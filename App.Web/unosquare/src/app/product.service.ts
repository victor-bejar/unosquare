import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { IProduct } from './iproduct';
import { IProducts } from './iproducts';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private productEndpointUrl = 'https://localhost:5001/api/v1/product';

  constructor(private _http: HttpClient) { }

  public getProducts(filter: string, pageIndex: number, pageSize: number): Observable<IProducts> {
    return this._http.
      get<IProducts>
      (
        this.productEndpointUrl,
        {
          params: new HttpParams().
            set('filter', filter).
            set('pageIndex', pageIndex.toString()).
            set('pageSize', pageSize.toString())
        }
      );
  }

  public getProduct(productId: number): Observable<IProduct> {
    return this._http.get<IProduct>(this.productEndpointUrl + '/' + productId);
  }

  saveProduct(product: IProduct): Observable<IProduct> {

    let observable: Observable<IProduct> = null;

    if (!product.productId) {
      observable = this._http.post<IProduct>(this.productEndpointUrl, product);
    } else {
      observable = this._http.put<IProduct>(this.productEndpointUrl + '/' + product.productId, product);
    }

    return observable;

  }

  removeProduct(productId: number): Observable<IProduct> {
    return this._http.delete<IProduct>(this.productEndpointUrl + '/' + productId);
  }

}
