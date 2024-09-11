import React, { useState, useEffect, ChangeEvent } from 'react';
import axios from 'axios';

interface Suggestion {
  id: string;
  text: string;
}

interface SearchBarProps {
  onSuggestionSelect: (value: string) => void;
}

const SearchBarClient: React.FC<SearchBarProps> = ({ onSuggestionSelect }) => {
  const [query, setQuery] = useState<string>('');
  const [suggestions, setSuggestions] = useState<Suggestion[]>([]);

  useEffect(() => {
    const fetchSuggestions = async () => {
      if (query.length > 2) {
        try {
          const response = await axios.get<Suggestion[]>(`/api/search/suggestions?query=${query}`);
          setSuggestions(response.data);
        } catch (error) {
          console.error('Error fetching suggestions:', error);
        }
      } else {
        setSuggestions([]);
      }
    };

    const debounceTimer = setTimeout(fetchSuggestions, 300);

    return () => clearTimeout(debounceTimer);
  }, [query]);

  const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
    setQuery(e.target.value);
  };

  const handleSuggestionClick = (suggestion: Suggestion) => {
    setQuery(suggestion.text);
    setSuggestions([]);
    onSuggestionSelect(suggestion.text);
  };

  return (
    <div>
      <input
        type="text"
        value={query}
        onChange={handleInputChange}
        placeholder="Search..."
      />
      {suggestions.length > 0 && (
        <ul>
          {suggestions.map((suggestion) => (
            <li key={suggestion.id} onClick={() => handleSuggestionClick(suggestion)}>
              {suggestion.text}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default SearchBarClient;