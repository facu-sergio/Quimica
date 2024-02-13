import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ShipmentsComponent } from './shipments.component';
import { ShipmentsRoutingModule } from './shipments-routing.module';
import { IonicModule } from '@ionic/angular';
import { ShipmentModule } from 'src/app/modules/shipment/shipment.module';
import { SharedModule } from 'src/app/modules/shared/shared.module';

@NgModule({
  declarations: [ShipmentsComponent],
  imports: [
    CommonModule,
    ShipmentsRoutingModule,
    IonicModule,
    ShipmentModule,
    SharedModule,
  ],
  providers: [DatePipe],
})
export class ShipmentsModule {}
