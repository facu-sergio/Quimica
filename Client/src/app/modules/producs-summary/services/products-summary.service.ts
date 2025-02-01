import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor() { }

  // Método simulado para obtener productos basados en mes y año
  getProductsByMonthAndYear(month: number, year: number): Observable<any[]> {
    // Simulación de datos
    const simulatedProducts = [
      { name: 'Producto A', sales: 100 },
      { name: 'Producto B', sales: 150 },
      { name: 'Producto C', sales: 200 },
      { name: 'Producto D', sales: 250 },
      { name: 'Producto E', sales: 300 },
      { name: 'Producto F', sales: 120 },
    ];



    return of(simulatedProducts); // Devuelve los productos filtrados como un Observable
  }
}