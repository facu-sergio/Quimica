import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabsComponent } from './components/tabs/tabs.component';
import { TabsRoutingModule } from './tabs-routing.module';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { MenuComponent } from './components/menu/menu.component';



@NgModule({
  declarations: [TabsComponent,MenuComponent],
  imports: [
    CommonModule,IonicModule, TabsRoutingModule,RouterModule
  ]
})
export class TabsModule { }
