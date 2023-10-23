import React, { useState } from "react";
import { Link, useNavigate } from 'react-router-dom';
import axios from "axios";
import { AuthorizeUser} from "../AuthorizeUserContext";

const Login = () => {

    const [formData, setFormData] = useState({ email: '', password: '' });
    const [validLogin, setValidLogin] = useState(true);
    const { setUser } = AuthorizeUser();
    const navigate = useNavigate();

    const textChange = e => {
        const copy = { ...formData };
        copy[e.target.name] = e.target.value;
        setFormData(copy);
    }

    const submitForm = async e => {
        e.preventDefault();
        const { data } = await axios.post('/api/account/login', formData);
        const isValidLogin = !!data;
        setValidLogin(false);
        if (isValidLogin) {
            setUser(data);
            navigate('/');
        }
    }

    return (
        <div className="row" style={{ minHeight: '80vh', display: 'flex', alignItems: 'center' }}>
            <div className="col-md-6 offset-md-3 bg-light p-4 rounded shadow">
                <h3>Log in to your account</h3>
                {!validLogin && <span className="text-danger">Invalid Login information. Please try again</span>}
                <form onSubmit={submitForm}>
                    <input onChange={textChange} value={formData.email} type="text" name="email" placeholder="Email" className="form-control" />
                    <br />
                    <input onChange={textChange} value={formData.password} type="password" name="password" placeholder="Password" className="form-control" />
                    <br />
                    <button className="btn btn-primary">Login</button>
                </form>
                <Link to='/signup'>Don't have an account? Sign up here!</Link>
            </div>
        </div>
    )

}

export default Login;