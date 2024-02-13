import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { FormulaDetailPageRoutingModule } from './formula-detail-routing.module';

import { FormulaDetailPage } from './formula-detail.page';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    FormulaDetailPageRoutingModule,
  ],
  declarations: [FormulaDetailPage]
})
export class FormulaDetailPageModule {}
