import React from 'react';
import './App.css';
import { Route, Routes } from 'react-router-dom';
import { AuthorizeUserComponent } from './AuthorizeUserContext';
import Layout from './Components/Layout';
import Private from './Components/Private';
import Home from './Pages/Home';
import Login from './Pages/Login';
import Signup from './Pages/Signup';
import ViewAll from './Pages/ViewAll';
import Logout from './Pages/Logout';


const App = () => {
    return (
        <AuthorizeUserComponent>
            <Layout>
                <Routes>
                    <Route exact path='/' element={<Home />} />
                    <Route exact path='/signup' element={<Signup />} />
                    <Route exact path='/login' element={<Login />} />
                    <Route exact path='/viewAll' element={<ViewAll />} />
                    <Route exact path='/logout' element={
                        <Private>
                            <Logout />
                        </Private>
                    } />
                </Routes>
            </Layout>
        </AuthorizeUserComponent>
    )
}

export default App;