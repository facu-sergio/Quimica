import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { ShipmentDto } from "../modules/core/models/shipmentDto";
import { ShipmentAddProductDTO } from "../modules/core/models/shipmentAddProductDTO";
import { Shipment } from "../modules/core/models/shipment";

@Injectable({
    providedIn: 'root'
  })
  export class shipmentService{
    constructor(private httpClient:HttpClient) { }


    getShipments(date: string): Observable<ShipmentDto[]> {
        let params = new HttpParams().set('date', date);
        return this.httpClient.get<ShipmentDto[]>(environment.shipments, { params: params });
      }

    insertShipments(shipment:ShipmentDto){
      return this.httpClient.post(environment.shipments,shipment)
    }

    deleteProductOfShipment(productId: number | undefined, shipmentId: number | undefined) {
      const url = `${environment.shipments}/DeleteProduct?idShipment=${shipmentId}&idProduct=${productId}`;
      return this.httpClient.delete(url);
    }
    
    DeleteShipment(shipmentId:number|undefined){
      const url = `${environment.shipments}?idShipment=${shipmentId}`;
      return this.httpClient.delete(url);
    }

    InsertProductToShipment (product:ShipmentAddProductDTO){
      const url = `${environment.shipments}/insertProduct`;
      return this.httpClient.post(url,product);
    }


    GetShipmentById(id:number):Observable<Shipment>{
      const url =`${environment.shipments}/GetShipment?id=${id}`;
      return this.httpClient.get<Shipment>(url);
    }

    updateShipment(shipment:Shipment){
      const url =`${environment.shipments}`
      return this.httpClient.put(url,shipment);
    }
  }