import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShipmentEditComponent } from './shipment-edit.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: ShipmentEditComponent,
  }
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShipmentEditRoutingModule {}
