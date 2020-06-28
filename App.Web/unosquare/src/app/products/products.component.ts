import { AfterViewInit } from '@angular/core';
import { ElementRef } from '@angular/core';
import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { ViewChild } from '@angular/core';

import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

import { tap } from 'rxjs/operators';
import { distinctUntilChanged } from 'rxjs/operators';
import { debounceTime } from 'rxjs/operators';
import { fromEvent } from 'rxjs';
import { merge } from 'rxjs';

import { ProductService } from '../product.service';
import { DialogService } from '../dialog.service';
import { NotificationService } from '../notification.service';

import { ProductsDataSource } from './products-datasource';
import { IProduct } from '../iproduct';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements AfterViewInit, OnInit {

  public pageSizeOptions: number[] = [25, 50, 100, 250];
  public defaultPageSize: number = this.pageSizeOptions[0];
  public dataSource: ProductsDataSource;

  public displayedColumns: string[] = [
    'productId', 'name', 'description', 'ageRestriction', 'company', 'price', 'actions'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('filter') filter: ElementRef;

  constructor(
    private _productService: ProductService,
    private _dialogService: DialogService,
    private _notificationService: NotificationService) { }

  ngOnInit() {
    this.dataSource = new ProductsDataSource(this._productService);
    this.dataSource.loadProducts('', 0, this.defaultPageSize);
  }

  ngAfterViewInit() {

    fromEvent(this.filter.nativeElement, 'keyup').
      pipe(
        debounceTime(800),
        distinctUntilChanged(),
        tap(() => {
          this.paginator.pageIndex = 0;
          this.loadProductsPage();
        })
      ).subscribe();

    // reset the paginator after sorting
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    // on sort or paginate events, load a new page
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.loadProductsPage())
      ).subscribe();

  }

  private loadProductsPage(): void {
    this.dataSource.loadProducts(
      this.filter.nativeElement.value,
      this.paginator.pageIndex,
      this.paginator.pageSize,
      this.sort.active,
      this.sort.direction);
  }

  public onRemove(productId: number): void {

    this._dialogService.
      showConfirmationDialog('Are you sure?').
      afterClosed().
      subscribe(userResponse => {

        if (userResponse) {

          this._productService.
            removeProduct(productId).
            subscribe(() => {

              this.dataSource.loadProducts(
                this.filter.nativeElement.value,
                this.paginator.pageIndex,
                this.paginator.pageSize,
                this.sort.active,
                this.sort.direction);

              this._notificationService.warn('Record Removed');

            });

        }

      });

  }

  //TODO: Create a translation Service
  getProductTypeDescription(productType: string): string {

    let productTypes: any[] = [
      { type: 'Enterprise', description: 'Empresa' },
      { type: 'Commerce', description: 'Comercio' }
    ];

    return (productTypes.find(x => x.type === productType)).description;

  }

  //TODO: Create a translation Service
  getRewardPolicyTypeDescription(rewardPolicyType: string): string {

    let rewardPolicyTypes: any[] = [
      { type: 'PurchaseAmountToPhasePercent', description: 'Porcentaje' },
      { type: 'PurchaseAmountToPhasePoints', description: 'Puntos Tabulados' },
      { type: 'PurchaseAmountToSimplePoints', description: 'Puntos Simples' }
    ];

    return (rewardPolicyTypes.find(x => x.type === rewardPolicyType)).description;

  }

}
