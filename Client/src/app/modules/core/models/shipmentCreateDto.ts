import { ProductShipmentDto } from './productShipmentDto';

export interface ShipmentCreateDto { 
    clientName: string;
    price: number;
    note: string;
    locationId: number;
    street: string;
    number: string;
    state: number;
    date: Date;
    products: Array<ProductShipmentDto>;
}