import { DataSource } from '@angular/cdk/collections';
import { CollectionViewer } from '@angular/cdk/collections';

import { catchError } from 'rxjs/operators';
import { finalize } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { BehaviorSubject } from 'rxjs';

import { IProduct } from '../iproduct';

import { ProductService } from '../product.service';
import { IProducts } from '../iproducts';

export class ProductsDataSource extends DataSource<IProduct> {

  private productsSubject = new BehaviorSubject<IProduct[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  public loading$ = this.loadingSubject.asObservable();
  public totalRows = 0;

  constructor(private _productService: ProductService) {
    super();
  }

  connect(collectionViewer: CollectionViewer): Observable<IProduct[]> {
    return this.productsSubject.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.productsSubject.complete();
    this.loadingSubject.complete();
  }

  public loadProducts(
    filter: string,
    pageIndex: number = 0,
    pageSize: number = 25,
    sortField: string = 'productId',
    sortDirection: string = 'asc'): void {

    this.loadingSubject.next(true);

    const emptyProductList: IProducts = {
      totalItemsCount: 0,
      renderedItemsCount: 0,
      items: []
    };

    this._productService.
      getProducts(filter, pageIndex, pageSize).
      pipe(
        catchError(() => of(emptyProductList)), finalize(() => this.loadingSubject.next(false)),
      ).
      subscribe(products => {
        this.totalRows = products.totalItemsCount;
        this.productsSubject.next(products.items.sort(this.compareValues(sortField, sortDirection)));
        this.loadingSubject.next(false);
      });

  }

  //TODO: Create an util library and put it there
  private compareValues(key: string, order: string = 'asc'): any {

    return function innerSort(a: any, b: any): number {

      if (!a.hasOwnProperty(key) || !b.hasOwnProperty(key)) {
        return 0;
      }

      const varA = (typeof a[key] === 'string') ? a[key].toUpperCase() : a[key];
      const varB = (typeof b[key] === 'string') ? b[key].toUpperCase() : b[key];

      let comparison = 0;

      if (varA > varB) {
        comparison = 1;
      } else if (varA < varB) {
        comparison = -1;
      }
      return (
        (order === 'desc') ? (comparison * -1) : comparison
      );

    };

  }

}
