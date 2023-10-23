import {Navigate} from 'react-router-dom';
import {AuthorizeUser} from '../AuthorizeUserContext';

const Private = (props) => {
    const {user} = AuthorizeUser();

    return user ? props.children : <Navigate to="/login" replace />;
};

export default Private;