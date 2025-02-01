import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardModule } from 'src/app/modules/dashboard/dashboard.module';
import { DashboardComponent } from './dashboard.component';
import { IonicModule } from '@ionic/angular';



@NgModule({
  declarations: [DashboardComponent],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    DashboardModule,
    IonicModule
  ]
})
export class DashboardPageModule { }
