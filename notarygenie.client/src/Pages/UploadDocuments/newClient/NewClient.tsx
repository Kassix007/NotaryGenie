/* eslint-disable @typescript-eslint/no-unused-vars */
import React, { useState, ChangeEvent } from 'react';
import Header from '../../../Components/Header/Header';
import { useNavigate } from 'react-router-dom';
import styles from './NewClient.module.css';

const NewClient: React.FC = () => {
    const navigate = useNavigate();
    const handleNavigate = (path: string) => {
        navigate(path);
    };

    const [isMarried, setIsMarried] = useState<boolean>(false);
    const [proofOfAddressType, setProofOfAddressType] = useState<string>('');
    const [uploadedFiles, setUploadedFiles] = useState<{ [key: string]: boolean }>({});

    const handleFileUpload = (event: ChangeEvent<HTMLInputElement>, fieldName: string) => {
        if (event.target.files && event.target.files.length > 0) {
            console.log(`File uploaded for ${fieldName}:`, event.target.files[0]);
            setUploadedFiles(prev => ({ ...prev, [fieldName]: true }));
        } else {
            setUploadedFiles(prev => ({ ...prev, [fieldName]: false }));
        }
    };

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        try {
            for (const [key, value] of Object.entries(uploadedFiles)) {
                if (value) {
                    const input = document.querySelector(`#${key}`) as HTMLInputElement;
                    if (input && input.files && input.files.length > 0) {
                        const file = input.files[0];
                        const formData = new FormData();
                        formData.append("file", file);
                        formData.append("documentName", key);

                        console.log('FormData:', formData.get("file"), formData.get("documentName"), formData.get("keyword")); // Debugging line to verify form data

                        const res = await fetch('/api/TempUploadDocuments/upload', {
                            method: 'POST',
                            body: formData,
                        });

                        if (!res.ok) {
                            const errorMessage = await res.text();
                            throw new Error(`Failed to upload ${key}: ${errorMessage}`);
                        }

                        const data = await res.json();
                        console.log(`${key} uploaded successfully:`, data.Message);
                    }
                }
            }
            alert("All files uploaded successfully.");
            handleNavigate('/upload-documents/new-client/confirm');
        } catch (error) {
            console.error('Error uploading documents:', error);
            alert('Error uploading documents');
        }
    };

    return (
        <div className={styles.container}>
            <Header />
            <h1 className={styles.title}>New Client</h1>
            <form className={styles.form} onSubmit={handleSubmit}>
                <FileUploadField
                    id="nationalIdFront"
                    label="National ID Card Front"
                    onChange={(e) => handleFileUpload(e, 'nationalIdFront')}
                    isUploaded={uploadedFiles['nationalIdFront']}
                    required
                />

                <FileUploadField
                    id="nationalIdBack"
                    label="National ID Card Back"
                    onChange={(e) => handleFileUpload(e, 'nationalIdBack')}
                    isUploaded={uploadedFiles['nationalIdBack']}
                    required
                />

                <FileUploadField
                    id="birthCertificate"
                    label="Birth Certificate"
                    onChange={(e) => handleFileUpload(e, 'birthCertificate')}
                    isUploaded={uploadedFiles['birthCertificate']}
                    required
                />

                <div className={styles.formGroup}>
                    <label className={styles.label}>
                        <input 
                            type="checkbox" 
                            checked={isMarried}
                            onChange={(e) => setIsMarried(e.target.checked)} 
                            className={styles.checkbox}
                        />
                        Add Marriage Certificate?
                    </label>
                </div>

                {isMarried && (
                    <FileUploadField
                        id="marriageCertificate"
                        label="Marriage Certificate"
                        onChange={(e) => handleFileUpload(e, 'marriageCertificate')}
                        isUploaded={uploadedFiles['marriageCertificate']}
                    />
                )}

                <div className={styles.formGroup}>
                    <label className={styles.label}>Proof of Address Type:</label>
                    <select 
                        value={proofOfAddressType} 
                        onChange={(e) => setProofOfAddressType(e.target.value)}
                        required
                        className={styles.select}
                    >
                        <option value="">Select type</option>
                        <option value="CEB">CEB</option>
                        <option value="CWA">CWA</option>
                        <option value="other">Other</option>
                    </select>
                </div>

                {proofOfAddressType && (
                    <FileUploadField
                        id={`proofOfAddress-${proofOfAddressType}`} // Unique ID based on the selected type
                        label={`Proof of Address (${proofOfAddressType})`} // Update label dynamically
                        onChange={(e) => handleFileUpload(e, `proofOfAddress-${proofOfAddressType}`)} // Pass the type along with the field name
                        isUploaded={uploadedFiles[`proofOfAddress-${proofOfAddressType}`]}
                        required
                    />
                )}

                <FileUploadField
                    id="otherDocuments"
                    label="Other Documents"
                    onChange={(e) => handleFileUpload(e, 'otherDocuments')}
                    isUploaded={uploadedFiles['otherDocuments']}
                    multiple
                />

                <button type="submit" className={styles.submitButton}>Submit</button>
            </form>
        </div>
    );
};

interface FileUploadFieldProps {
    id: string;
    label: string;
    onChange: (event: ChangeEvent<HTMLInputElement>) => void;
    isUploaded: boolean;
    required?: boolean;
    multiple?: boolean;
}

const FileUploadField: React.FC<FileUploadFieldProps> = ({ id, label, onChange, isUploaded, required, multiple }) => (
    <div className={`${styles.formGroup} ${isUploaded ? styles.uploaded : ''}`}>
        <label htmlFor={id} className={styles.label}>{label}:</label>
        <input 
            type="file" 
            id={id} 
            onChange={onChange}
            required={required}
            multiple={multiple}
            className={styles.fileInput}
        />
    </div>
);

export default NewClient;
