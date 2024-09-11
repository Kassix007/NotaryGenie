import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
    MDBBtn,
    MDBContainer,
    MDBRow,
    MDBCol,
    MDBInput,

} from 'mdb-react-ui-kit';
import 'mdb-react-ui-kit/dist/css/mdb.min.css';
import "@fortawesome/fontawesome-free/css/all.min.css";
import PasswordInput from './passwordInput';

const LoginPage: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate(); 

    const handleLogin = async () => {
        try {
           
            if (!email || !password) {
                alert('Please enter both email and password.');
                return;
            }

            const response = await fetch('api/Auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ email, password }),
            });

            if (!response.ok) {
                throw new Error('Login failed');
            }

            const data = await response.json();

            localStorage.setItem('token', data.token);
            localStorage.setItem('username', data.username);
            localStorage.setItem('userID', data.userID);

            navigate('/dashboard');
        } catch (error) {

            console.log('Login failed', error);
            alert('Login failed. Please check your credentials and try again.');
        }
    };

    return (
        <MDBContainer className="my-5 gradient-form">
            <MDBRow>
                <MDBCol col='6' className="mb-5">
                    <div className="d-flex flex-column ms-5">
                        <div className="text-center">
                            <img
                                src="logo.png"
                                style={{ width: '80%' }}
                                alt="logo"
                            />
                            <h4 className="mt-1 mb-5 pb-1">Notary Genie</h4>
                        </div>
                        <p>Please login to your account</p>
                        <MDBInput
                            wrapperClass='mb-4'
                            label='Email address'
                            id='form1'
                            type='email'
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                        <PasswordInput value={password} onChange={(e) => setPassword(e.target.value)} />
                        <div className="text-center pt-1 mb-5 pb-1">
                            <MDBBtn className="mb-4 w-100 gradient-custom-2" onClick={handleLogin}>Sign in</MDBBtn>
                            <a className="text-muted" href="#!">Forgot password?</a>
                        </div>
                    </div>
                </MDBCol>
            </MDBRow>
        </MDBContainer>
    );
}

export default LoginPage;
