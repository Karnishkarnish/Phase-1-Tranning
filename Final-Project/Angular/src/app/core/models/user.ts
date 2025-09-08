export type Role = 'Customer' | 'Store' | 'Admin'; 
export interface User 
{ id: number; 
    email: string; 
    name: string; 
    role: Role; 
    token?: string; 
    storeId?: number; 
}