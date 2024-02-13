import { ProductShipmentDto } from './productShipmentDto';

export interface ShipmentUpdateDTO { 
    id: number;
    clientName?: string;
    price?: number;
    note?: string;
    addressId?: number;
    locationId?: number;
    street?: string;
    number?: string;
    state?: number;
    date?: Date;
    products?: Array<ProductShipmentDto>;
}