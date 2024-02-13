import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ShipmentFormComponent } from './shipment-form.component';

const routes: Routes = [
  {
    path: '',
    component: ShipmentFormComponent,
  },
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShipmentFormRouitngModule {}
