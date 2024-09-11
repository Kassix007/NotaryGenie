import React, { useState, useEffect } from 'react';
import axios from 'axios';

interface DocumentDto {
    documentID: number;
    documentName: string;
    uploadDate: string;
    filePath: string;
}

interface ClientDeedDto {
    deedID: number;
}

interface ClientDto {
    clientID: number;
    firstName: string;
    surname: string;
    email: string;
    phone: string;
    dateOfBirth: string;
    profession: string;
    documents: DocumentDto[];
    clientDeeds: ClientDeedDto[];
}

const ClientsList: React.FC = () => {
    const [clients, setClients] = useState<ClientDto[]>([]); 
    const [loading, setLoading] = useState<boolean>(true); 
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        fetchClients();
    }, []);

    const fetchClients = async () => {
        try {
            const response = await axios.get('api/Clients/Notary/2');           
            const clientsData = response.data.$values;
            setClients(clientsData);
        } catch (error) {
            setError("There was an error fetching the clients!");
            console.error(error);
        } finally {
            setLoading(false);
        }
    };

    const handleEdit = (clientId: number) => {
       
        console.log(`Edit client with ID: ${clientId}`);
    };

    const handleDelete = async (clientId: number) => {
        try {
            await axios.delete(`https://localhost:7072/api/Clients/${clientId}`);
            setClients(clients.filter(client => client.clientID !== clientId));
        } catch (error) {
            setError("There was an error deleting the client!");
            console.error(error);
        }
    };

    const handleCreate = () => {
        // Handle create logic
        console.log("Create a new client");
    };

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h1>Clients List</h1>
            <button onClick={handleCreate}>Create New Client</button>
            {clients.length > 0 ? (
                <table>
                    <thead>
                        <tr>
                            <th>First Name</th>
                            <th>Surname</th>
                            <th>Email</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {clients.map(client => (
                            <tr key={client.clientID}>
                                <td>{client.firstName}</td>
                                <td>{client.surname}</td>
                                <td>{client.email}</td>
                                <td>
                                    <button onClick={() => handleEdit(client.clientID)}>Edit</button>
                                    <button onClick={() => handleDelete(client.clientID)}>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            ) : (
                <div>No clients found.</div>
            )}
        </div>
    );
};

export default ClientsList;
