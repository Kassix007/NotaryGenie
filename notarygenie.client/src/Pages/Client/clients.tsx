import React, { FormEvent, useState } from 'react';

import Header from '../../Components/Header/Header';
import SearchBarClient from '../../Components/Header/SearchBarClient/SearchBarClient';
import Button from '@mui/material/Button';
import ClientsList from '../../Components/ClientList/ClientsList';

const Clients: React.FC = () => {


    
    const [selectedValue, setSelectedValue] = useState<string>('');

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
      e.preventDefault();
      console.log('Form submitted with value:', selectedValue);
      // Handle form submission here
    };



    return (
        <div className="clients-container">
            <Header  />
            <h1>Clients</h1>
            
            <form onSubmit={handleSubmit}>
                <SearchBarClient onSuggestionSelect={setSelectedValue} />
                <input type="hidden" name="selectedValue" value={selectedValue} /><br></br>
                <Button variant="outlined" type="submit">Submit</Button>
            </form>

            <ClientsList />

        </div>
       
    );
};

export default Clients;