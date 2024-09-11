import React, { useState } from 'react';
import { MDBInput, MDBIcon } from 'mdb-react-ui-kit';

const PasswordInput: React.FC<{ value: string, onChange: (e: React.ChangeEvent<HTMLInputElement>) => void }> = ({ value, onChange }) => {
    const [showPassword, setShowPassword] = useState(false);

    const togglePasswordVisibility = () => {
        setShowPassword(!showPassword);
    };

    return (
        <div className="position-relative">
            <MDBInput
                wrapperClass='mb-4'
                label='Password'
                id='form2'
                type={showPassword ? 'text' : 'password'}  // Toggle between 'text' and 'password'
                value={value}
                onChange={onChange}
            />
            <MDBIcon
                fas
                icon={showPassword ? 'eye-slash' : 'eye'}
                onClick={togglePasswordVisibility}
                className="position-absolute"
                style={{
                    top: '50%',
                    right: '15px',
                    transform: 'translateY(-50%)',
                    cursor: 'pointer'
                }}
            />
        </div>
    );
};

export default PasswordInput;
