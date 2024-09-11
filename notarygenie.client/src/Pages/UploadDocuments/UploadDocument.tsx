import React from 'react';
import Header from '../../Components/Header/Header';
import { useNavigate } from 'react-router-dom';
import './uploadDocument.css';
const UploadDocuments: React.FC = () => {
    const navigate = useNavigate();
    const handleNavigate = (path: string) => {
        navigate(path);
    };

    return (
       <div>
            <Header />

            <div>  
                <h1>Upload Documents</h1>
                <p>Start upload doc... build profile.. existing or new...</p>
                <div className = "uploadDoc-container">
                    <div className="dashboard-card" onClick={() => handleNavigate('/upload-documents/clients')}>
                            <div className="dashboard-card-icon">
                                <i className="fas fa-users"></i>
                            </div>
                            <h3 className="dashboard-card-title">Existing Client</h3>
                    </div>
                    <br></br>
                    <div className="dashboard-card" onClick={() => handleNavigate('/upload-documents/new-client')}>
                            <div className="dashboard-card-icon">
                            <i className="fa fa-plus-square" aria-hidden="true"></i>
                            </div>
                            <h3 className="dashboard-card-title">New Client</h3>
                    </div>
                </div>
                
            </div>
        </div>
    );
};

export default UploadDocuments;
