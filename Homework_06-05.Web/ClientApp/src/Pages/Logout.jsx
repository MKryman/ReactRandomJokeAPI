import React, {useEffect} from "react";
import axios from 'axios';
import { AuthorizeUser } from "../AuthorizeUserContext";

const Logout = () => {

    const {setUser} = AuthorizeUser();

    useEffect(() => {
        logoutUser();
    }, []);
    
    const logoutUser = async () => {
        await axios.post('/api/account/logout');
        setUser(null);
    }
}

export default Logout;