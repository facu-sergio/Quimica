import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShipmentSummaryComponent } from './components/shipment-summary/shipment-summary.component';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import {  } from '../producs-summary/components/products-grid/productsGrid.component';
import { AgGridModule } from 'ag-grid-angular';
@NgModule({
  declarations: [ShipmentSummaryComponent, ],
  imports: [
    CommonModule,IonicModule,RouterModule,AgGridModule
  ],
  exports:[ShipmentSummaryComponent,]
})
export class DashboardModule { }
