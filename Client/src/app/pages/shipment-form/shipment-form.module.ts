import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShipmentFormComponent } from './shipment-form.component';
import { ShipmentFormRouitngModule } from './shipment-form-rouitng.module';
import {  ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { SharedModule } from 'src/app/modules/shared/shared.module';
import{MatStepperModule} from'@angular/material/stepper'


@NgModule({
  declarations: [ShipmentFormComponent],
  imports: [
    CommonModule,ShipmentFormRouitngModule,ReactiveFormsModule,IonicModule,SharedModule,MatStepperModule 
  ]
})
export class ShipmentFormModule { }
