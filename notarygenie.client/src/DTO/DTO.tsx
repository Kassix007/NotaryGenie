interface DocumentDto {
    documentID: number;
    documentName: string;
    uploadDate: string;
    filePath: string;
}

interface ClientDeedDto {
    deedID: number;
    // Add other necessary properties if needed
}

// eslint-disable-next-line @typescript-eslint/no-unused-vars
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
