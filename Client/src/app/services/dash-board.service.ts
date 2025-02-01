import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Shipment } from '../modules/core/models/shipment';
import { ShipmentDto } from '../modules/core/models/shipmentDto';

@Injectable({
  providedIn: 'root'
})
export class DashBoardService {

  constructor(private httpClient:HttpClient) { }

  getShipmenstByDateRange(dateFrom: Date, dateTo: Date): Observable<ShipmentDto[]>{
    let params = new HttpParams()
        .set('dateFrom', dateFrom.toISOString()) // Convertir la fecha a formato ISO
        .set('dateTo', dateTo.toISOString());    // Hacer lo mismo con dateTo

    return this.httpClient.get<ShipmentDto[]>(environment.dashBoard, { params });
}

}
