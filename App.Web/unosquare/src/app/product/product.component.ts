import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';

import { ProductService } from '../product.service';
import { NotificationService } from '../notification.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  public form = null;

  private productId: number = 0;
  public productTypes: any[] = [];
  public rewardPolicyTypes: any[] = [];

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _productService: ProductService,
    private notificationService: NotificationService) { }

  ngOnInit(): void {

    this.productId = +this._route.snapshot.paramMap.get("productId");

    this.createFormGroup();

    if (this.productId >= 1) {
      this.loadProduct(this.productId);
    }

  }

  public onClear(): void {
    this.form.reset();
    this.initializeForm();
  }

  private createFormGroup(): void {

    this.form = this._formBuilder.group({
      productId: new FormControl({ value: null, disabled: !this.isCreationMode() }, (!this.isCreationMode() ? [Validators.required] : null)),
      name: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      ageRestriction: new FormControl('', [Validators.required]),
      company: new FormControl('', [Validators.required]),
      price: new FormControl('', [Validators.required]),
    });

  }

  public isCreationMode() {

    let newEntity = true;

    if (this.productId >= 1) {
      newEntity = false;
    }

    return newEntity;

  }

  private initializeForm(): void {
    this.form.setValue({
      productId: '',
      name: '',
      description: '',
      ageRestriction: '',
      company: '',
      price: ''
    });

  }

  //TODO: Manage exceptions on suscribe
  private loadProduct(productId: number): void {
    this._productService.getProduct(this.productId).
      subscribe(
        product =>
          this.form.setValue({
            productId: product.productId,
            name: product.name,
            description: product.description,
            ageRestriction: product.ageRestriction,
            company: product.company,
            price: product.price
          }));
  }

  //TODO: Manage exceptions on suscribe
  //https://jasonwatmore.com/post/2019/11/21/angular-http-post-request-examples
  //TODO: Back to list
  public onSave() {

    this._productService.
      saveProduct(this.form.getRawValue()).
      subscribe(() => {
        this._router.navigate(['/products'])
        this.notificationService.success('Record ' + (this.isCreationMode() ? 'Created' : 'Updated'));
      });

  }

}
