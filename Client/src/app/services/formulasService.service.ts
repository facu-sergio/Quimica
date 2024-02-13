import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FormulasService {

  constructor(private httpClient:HttpClient) { }


  getFormulas(){
    return this.httpClient.get(environment.formulas);
  }

  getFormulBase(id: string) {
    return this.httpClient.get(`${environment.formula}/${id}`);
  }
  
  calcular(id: string, cantidad: number) {
    console.log(id)
    console.log(cantidad)
    const params = {
      formula_id: id,
      cantidad: cantidad
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.httpClient.post(`${environment.calcular}`, params);
  }
}
