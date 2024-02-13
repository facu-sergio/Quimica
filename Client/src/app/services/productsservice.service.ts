import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../modules/core/models/Product';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private httpClient:HttpClient,) { }


  getProducts():Observable<Product[]>{
    return this.httpClient.get<Product[]>(environment.products);
  }

  createProduct(product:Product){
    return this.httpClient.post(environment.products,product);
  }

  deleteProduct(idProduct:number){
    const url:string = environment.products+'?productId='+ idProduct;
    return this.httpClient.delete(url);
  }
  
}
