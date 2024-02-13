import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TabsComponent } from './components/tabs/tabs.component';

const routes: Routes = [
  {
    path:'tabs',
    component: TabsComponent,
    children:[
      {
        path:'shipments',
        loadChildren: () => import('../../pages/shipments/shipments.module').then(m => m.ShipmentsModule),
      },
      {
        path: 'editShipment/:id',
        loadChildren: () => import('../../pages/shipment-edit/shipment-edit.module').then(m => m.ShipmentEditModule)
      },
      {
        path:'Newshipment',
        loadChildren: () => import('../../pages/shipment-form/shipment-form.module').then(m => m.ShipmentFormModule)
      },
      {
       path: 'formulas',
       loadChildren: () => import ('../../pages/home/home.module').then(m => m.HomePageModule)
      },
      {
        path: 'products',
        loadChildren: () => import('../../pages/products/products.module').then(m => m.ProductsModule)
      }
  ]
  },
  {
    path: '',
    redirectTo:'/tabs/shipments',
    pathMatch: 'full'
  }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ]
})
export class TabsRoutingModule { }
