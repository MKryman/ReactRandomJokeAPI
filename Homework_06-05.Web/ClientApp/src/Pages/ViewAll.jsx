import React, {useState, useEffect} from "react";
import axios from "axios";

const ViewAll = () => {
   
    const [allJokes, setAllJokes] = useState([]);

    useEffect (() => {
        const loadJokes = async () => {
            const {data} = await axios.get('/api/jokes/alljokes');
            setAllJokes(data);
        }
        loadJokes();
    },[]);

    return (
        <div className="container" style={{marginTop: 60}}>
            <div className="row">
                <div className="col-md-6 offset-md-3">
                  {  allJokes.map(j => 
                        <div key={j.id} className="card card-body bg-light mb-3">
                            <h5>{j.setup}</h5>
                            <h5>{j.punchline}</h5>
                            <br/>
                            <span>Likes: {j.userLikedJokes.filter(j => j.liked).length}</span>
                            <span>Dislikes: {j.userLikedJokes.filter(j => !j.liked).length}</span>
                        </div>
                        )}
                </div>
            </div>
        </div>
    )
}

export default ViewAll;