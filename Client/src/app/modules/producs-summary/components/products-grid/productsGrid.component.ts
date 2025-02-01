import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { ColDef } from 'ag-grid-community';
import { AgGridModule } from 'ag-grid-angular';

@Component({
  selector: 'app-productsGrid',
  standalone: true,
  imports: [CommonModule, IonicModule,AgGridModule],
  templateUrl: './productsGrid.component.html',
  styleUrls: ['./productsGrid.component.scss']
})
export class ProductSGridComponent implements OnInit {
  rowData = [
    { nombre: "Skip", cantidad:5 },
    { nombre: "Vivere", cantidad: 10 },
    { nombre: "Detergente", cantidad: 3 },
  ];
 
  // Column Definitions: Defines the columns to be displayed.
  colDefs: ColDef[] = [
    { field: "nombre" },
    { field: "cantidad" },
  ];



  public chart: any;

  constructor() { }

  ngOnInit(): void {
    
  }


}
