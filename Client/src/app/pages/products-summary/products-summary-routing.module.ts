import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes } from '@angular/router';
import { ProductsSummaryComponent } from './products-summary.component';
import { RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: ProductsSummaryComponent
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,RouterModule.forChild(routes)
  ]
})
export class ProductsSummaryRoutingModule { }
