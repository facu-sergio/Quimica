import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShipmentEditRoutingModule } from './shipment-edit-routing.module';
import { ShipmentEditComponent } from './shipment-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/modules/shared/shared.module';
import { IonicModule } from '@ionic/angular';



@NgModule({
  declarations: [ShipmentEditComponent],
  imports: [
    CommonModule,IonicModule, ShipmentEditRoutingModule,ReactiveFormsModule,SharedModule
  ]
})
export class ShipmentEditModule { }
