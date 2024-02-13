import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShipmentComponent } from './Components/shipment/shipment.component';
import { IonicModule } from '@ionic/angular';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProductModalComponent } from './Components/product-modal/product-modal.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [ShipmentComponent,ProductModalComponent],
  imports: [
    CommonModule,IonicModule,FontAwesomeModule,FormsModule,RouterModule,SharedModule
  ],
  exports:[ShipmentComponent]
})
export class ShipmentModule { }
