import React, { useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes, useNavigate } from 'react-router-dom';
import LoginPage from './Pages/login/LoginPage';
import Dashboard from './Pages/dashboard/Dashboard';
import Clients from './Pages/Client/clients';
import UploadDocuments from './Pages/UploadDocuments/UploadDocument';
import ExistingClient from './Pages/UploadDocuments/existingClient/existingClient';
import NewClient from './Pages/UploadDocuments/newClient/NewClient';
import ConfirmProfile from './Pages/UploadDocuments/newClient/ConfirmProfile';

const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<RequireAuth><LoginPage /></RequireAuth>} />
                <Route path="/dashboard" element={<RequireAuth><Dashboard /></RequireAuth>} />
                <Route path = "/chatbotai" element={null} />
                <Route path="/upload-documents" element={<UploadDocuments />} />
                <Route path="/clients" element={<Clients />} />
                <Route path="/upload-documents/clients" element={<ExistingClient />} />
                <Route path="/upload-documents/new-client" element={<NewClient />} />
                <Route path="/upload-documents/new-client/confirm" element={<ConfirmProfile />} />
            </Routes>
        </Router>
    );
};

const RequireAuth: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const navigate = useNavigate();
    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!token) {       
            navigate('/');
        }
    }, [navigate]);

    return <>{children}</>;
};

export default App;
