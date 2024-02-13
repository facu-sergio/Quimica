import { ProductShipmentDto } from './productShipmentDto';
export interface ShipmentDto { 
    id?: number;
    clientName?: string;
    location?: string;
    street?: string;
    number?: string;
    price?: number;
    note?: string;
    state?: number;
    date?: Date;
    products?: Array<ProductShipmentDto>;
}