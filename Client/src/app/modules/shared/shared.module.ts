import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { ConfirmationModalComponent } from './components/confirmation-modal/confirmation-modal.component';



@NgModule({
  declarations: [HeaderComponent,ConfirmationModalComponent],
  imports: [
    CommonModule,IonicModule,RouterModule 
  ],
  exports:[
    HeaderComponent,ConfirmationModalComponent
  ]
})
export class SharedModule { }
