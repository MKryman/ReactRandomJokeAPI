import React, { useState, useEffect } from "react";
import { HandThumbsUpFill, HandThumbsDownFill } from 'react-bootstrap-icons';
import axios from "axios";
import { AuthorizeUser } from "../AuthorizeUserContext";

const Home = () => {

    const [currentJoke, setCurrentJoke] = useState({
        id: 0,
        setup: '',
        punchline: '',
        likes: 0,
        dislikes: 0
    });
    const [isLoading, setIsLoading] = useState(true);
    const { user } = AuthorizeUser();

    useEffect(() => {
        const loadJoke = async () => {
            const { data } = await axios.get('/api/jokes/getjoke');
            setCurrentJoke(data);
            setIsLoading(false);
        }

        loadJoke();
    }, []);


    const updateCounts = async () => {
        const { data } = await axios.get(`/api/jokes/getlikes?id=${currentJoke.id}`);
        setCurrentJoke({ ...currentJoke, likes: data.likes, dislikes: data.dislikes });
    }

    //setInterval(updateCounts, 3000);

    const reactToJoke = async (reaction) => {
        await axios.post('/api/jokes/jokereaction', { jokeId: currentJoke.id, userId: user.userId, status: reaction });
        updateCounts();
    }

    return (
        <div className="container" style={{ marginTop: 60 }}>
            <div className="row" style={{ minHeight: '80vh', display: 'flex', alignItems: 'center' }}>
                <div className="col-md-6 offset-md-3 bg-light p-4 rounded shadow">
                    {!!isLoading && <h1>Loading...</h1>}
                    <div >
                        <h4>{currentJoke.setup}</h4>
                        <h4>{currentJoke.punchline}</h4>

                        <br />
                        <div>
                            <div>{!user && <a href="/login">Login to your account to like/dislike this joke</a>}</div>
                            {!!user && <button className="btn btn-success" style={{ marginRight: 2, alignContent: 'center' }} onClick={() => reactToJoke(true)}>Like</button>}
                            {!!user && <button className="btn btn-danger" style={{ alignContent: 'center' }} onClick={() => reactToJoke(false)}>Dislike</button>}
                        </div>
                        <HandThumbsUpFill></HandThumbsUpFill> <span>{currentJoke.likes}      </span>
                        <HandThumbsDownFill></HandThumbsDownFill> <span>{currentJoke.dislikes}</span>

                        <br />
                        <button className="btn btn-link" onClick={() => window.location.reload()}>Refresh</button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Home;