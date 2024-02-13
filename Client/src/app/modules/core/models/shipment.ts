import { Address } from "./address";

export interface Shipment {
    id: number;
    clientName: string;
    price: number;
    note: string;
    state?: number;
    date: string; // Cambié el tipo a string para representar fechas en formato de cadena
    address: Address;
  }