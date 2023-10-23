import React, { useContext, useState, useEffect, createContext } from "react";
import axios from "axios";

const Authorize = createContext();

const AuthorizeUserComponent = ({ children }) => {

    const [user, setUser] = useState(null);

    useEffect(() => {
        const loadUser = async () => {
            const { data } = await axios.get('/api/account/getcurrentuser');
            setUser(data);
        }

        loadUser();
    }, []);

    return (
        <Authorize.Provider value={{user, setUser}}>
            {children}
        </Authorize.Provider>
    )
}

const AuthorizeUser = () => useContext(Authorize);

export{AuthorizeUserComponent, AuthorizeUser};