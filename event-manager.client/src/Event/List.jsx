import { useState } from 'react';

/* eslint-disable react/prop-types */ // TODO: upgrade to latest eslint tooling
function List({ name, data, onCreate, error }) {
    const [formData, setFormData] = useState({ id: '', name: '' });

    const handleFormChange = (event) => {
        const { name, value } = event.target;
        setFormData(prevData => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        onCreate(formData);
        setFormData({ id: '', name: '' });
    };

    return (
        <div>
            <h2>New {name}</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    name="name"
                    placeholder="Name"
                    value={formData.name}
                    onChange={handleFormChange}
                />
                <button type="submit">{'Create'}</button>
            </form>
            {error && <div>{error.message}</div>}
            <h2>{name}s</h2>
            <ul>
                {data.map(item => (
                    <li key={item.id}>
                        <div>{item.name}</div>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default List;