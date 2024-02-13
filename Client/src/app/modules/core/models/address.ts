import { Location } from "./location";

export interface Address {
    id?: number|null;
    location: Location;
    street: string;
    number: string;
  }