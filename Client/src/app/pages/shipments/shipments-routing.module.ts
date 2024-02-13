import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ShipmentsComponent } from './shipments.component';

const routes: Routes = [
  {
    path: '',
    component: ShipmentsComponent,
  },
];
@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShipmentsRoutingModule {}
