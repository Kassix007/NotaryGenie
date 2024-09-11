import React, { useState, useEffect } from 'react';
import Header from '../../../Components/Header/Header';
import styles from './NewClient.module.css';

interface UserInfo {
  childOtherNames: string;
  surname: string;
  dateOfBirth: string;
  birthCertificateNumber: string;
  idIssueDate: string;
  idNumber: string;
  profession: string;
  address: string;
}

const ConfirmProfile: React.FC = () => {
  const [userInfo, setUserInfo] = useState<UserInfo | null>(null);
  const [editingField, setEditingField] = useState<keyof UserInfo | null>(null);

  useEffect(() => {
    fetchUserInfo();
  }, []);

  const fetchUserInfo = async () => {
    try {
      // Replace with your actual API endpoint
      const response = await fetch('/api/File/InfoFromFiles');
      const data = await response.json();
      setUserInfo(data);
    } catch (error) {
      console.error('Error fetching user info:', error);
    }
  };

  const handleEdit = (field: keyof UserInfo) => {
    setEditingField(field);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>, field: keyof UserInfo) => {
    if (userInfo) {
      setUserInfo({ ...userInfo, [field]: e.target.value });
    }
  };

  const handleUpdate = async () => {
    if (!userInfo) return;

    try {
      // Replace with your actual API endpoint
      const response = await fetch('/api/update-user-info', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(userInfo),
      });

      if (response.ok) {
        setEditingField(null);
        alert('User information updated successfully!');
      } else {
        throw new Error('Failed to update user information');
      }
    } catch (error) {
      console.error('Error updating user info:', error);
      alert('Failed to update user information. Please try again.');
    }
  };

  if (!userInfo) {
          return (<><Header /><div>Loading...</div></>);
  }

  const renderField = (label: string, field: keyof UserInfo) => (
    <div className={styles.infoBox}>
      <h3>{label}</h3>
      {editingField === field ? (
        <input
          value={userInfo[field]}
          onChange={(e) => handleChange(e, field)}
          onBlur={() => setEditingField(null)}
          autoFocus
        />
      ) : (
        <p>{userInfo[field]}</p>
      )}
      <button onClick={() => handleEdit(field)}>Edit</button>
    </div>
  );

  return (
    <div className={styles.container}>
      <Header />
      <div className={styles.profileInfo}>
        {renderField('First Name', 'childOtherNames')}
        {renderField('Surname', 'surname')}
        {renderField('Date of Birth', 'dateOfBirth')}
        {renderField('Birth Certificate Number', 'birthCertificateNumber')}
        {renderField('ID Issue Date', 'idIssueDate')}
        {renderField('ID Number', 'idNumber')}
        {renderField('Profession', 'profession')}
        {renderField('Address', 'address')}
      </div>
      <button className={styles.updateButton} onClick={handleUpdate}>Update</button>
    </div>
  );
};

export default ConfirmProfile;