import { useEffect, useState } from 'react';
import List from './List'
import '../App.css';

const term = "Event";
const API_URL = "/events";
const headers = {
    'Content-Type': 'application/json',
};

function Event() {
    const [data, setData] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchEventData();
    }, []);

    const fetchEventData = () => {
        fetch(API_URL)
            .then(response => response.json())
            .then(data => setData(data))
            .catch(error => setError(error));
    };

    const handleCreate = (item) => {
        
        console.log(`add item: ${JSON.stringify(item)}`)

        fetch(API_URL, {
            method: 'POST',
            headers,
            body: JSON.stringify({ name: item.name }),
        })
            .then(response => response.json())
            .then(returnedItem => setData([...data, returnedItem]))
            .catch(error => setError(error));
    }

    return (
        <div>
            <List
                name={term}
                data={data}
                error={error}
                onCreate={handleCreate}
            />
        </div>
    );
}

export default Event;