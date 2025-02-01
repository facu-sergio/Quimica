import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { ProductsSummaryComponent } from './products-summary.component';
import { ProductsSummaryRoutingModule } from './products-summary-routing.module';
import { ProductSGridComponent } from '../../modules/producs-summary/components/products-grid/productsGrid.component';
import { SharedModule } from 'src/app/modules/shared/shared.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [ProductsSummaryComponent],
  imports: [
    CommonModule,
    ProductsSummaryRoutingModule,
    IonicModule,
    ProductSGridComponent,
    SharedModule,
    FormsModule
]
})
export class ProductsSummaryModule { }
