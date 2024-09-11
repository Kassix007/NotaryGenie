import React from 'react';
import { FaUserCircle, FaSignOutAlt } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';
import './Header.css';
interface HeaderProps {

}

const username = localStorage.getItem('username');

const Header: React.FC<HeaderProps> = () => {
    const navigate = useNavigate(); 
    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.clear();
        navigate('/');
    };

    const handleTitleClick = () => {
        navigate('/dashboard'); 
    };

    return (
        <header className="header">
            <div className="header-left">
            <h1 className="header-title" onClick={handleTitleClick} style={{ cursor: 'pointer' }}>
                    Notary Genie
                </h1>
            </div>
            <div className="header-right">
                <div className="user-profile">
                    <FaUserCircle className="user-icon" />
                    <span className="username">{username}</span>
                </div>
                <button className="logout-button" onClick={handleLogout}>
                    <FaSignOutAlt className="logout-icon" />
                    Logout
                </button>
            </div>
        </header>
    );
};

export default Header;