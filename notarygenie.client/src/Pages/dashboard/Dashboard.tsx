import React from 'react';
import { useNavigate } from 'react-router-dom';
import './Dashboard.css';
import Header from '../../Components/Header/Header';

const Dashboard: React.FC = () => {
    const navigate = useNavigate();
    const handleNavigate = (path: string) => {
        navigate(path);
    };

    return (
        <div className="dashboard-container">
            <Header  />
            <div className="dashboard-content">
                <div className="dashboard-card-container">
                    {/* Clients */}
                    <div className="dashboard-card" onClick={() => handleNavigate('/clients')}>
                        <div className="dashboard-card-icon">
                            <i className="fas fa-users"></i>
                        </div>
                        <h3 className="dashboard-card-title">Clients</h3>
                    </div>
                    {/* Upload Documents */}
                    <div className="dashboard-card" onClick={() => handleNavigate('/upload-documents')}>
                        <div className="dashboard-card-icon">
                            <i className="fas fa-upload"></i>
                        </div>
                        <h3 className="dashboard-card-title">Upload Documents</h3>
                    </div>
                    {/* Deeds */}
                    <div className="dashboard-card" onClick={() => handleNavigate('/deeds')}>
                        <div className="dashboard-card-icon">
                            <i className="fas fa-file-signature"></i>
                        </div>
                        <h3 className="dashboard-card-title">Deeds</h3>
                    </div>

                    <div className="dashboard-card" onClick={() => handleNavigate('/chatbotai')}>
                        <div className="dashboard-card-icon">
                            <i className="fa-regular fa-comments"></i>
                        </div>
                        <h3 className="dashboard-card-title">AI ChatBot</h3>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Dashboard;