import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../modules/producs-summary/services/products-summary.service';

@Component({
  selector: 'app-products-summary',
  templateUrl: './products-summary.component.html',
  styleUrls: ['./products-summary.component.scss'],
})
export class ProductsSummaryComponent implements OnInit {
  selectedMonth!: string; // Propiedad para almacenar el mes seleccionado

  constructor(private productService: ProductService) { }

  ngOnInit() {}

  onMonthChange(event: any) {
    const [year, month] = this.selectedMonth.split('-');
    const yearNumber = parseInt(year, 10); 
    const monthNumber = parseInt(month, 10); 

   
    this.productService.getProductsByMonthAndYear(monthNumber, yearNumber).subscribe((data) => {
      console.log(data);
    });
  }
}