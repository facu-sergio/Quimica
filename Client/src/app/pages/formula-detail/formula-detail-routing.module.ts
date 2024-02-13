import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { FormulaDetailPage } from './formula-detail.page';

const routes: Routes = [
  {
    path: '',
    component: FormulaDetailPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class FormulaDetailPageRoutingModule {}
