/* eslint-disable @typescript-eslint/no-unused-vars */
import React from 'react';
import Header from '../../../Components/Header/Header';
import { useNavigate } from 'react-router-dom';

const ExistingClient: React.FC = () => {
    const navigate = useNavigate();
    const handleNavigate = (path: string) => {
        navigate(path);
    };

    return (
       <div>
            <Header />
            <h1>Existing client</h1>
        </div>
    );
};

export default ExistingClient;
