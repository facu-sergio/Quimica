import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { Product } from 'src/app/modules/core/models/Product';
import { ShipmentAddProductDTO } from 'src/app/modules/core/models/shipmentAddProductDTO';
import { ProductsService } from 'src/app/services/productsservice.service';
import { shipmentService } from 'src/app/services/shipmentService.service';

@Component({
  selector: 'app-product-modal',
  templateUrl: './product-modal.component.html',
  styleUrls: ['./product-modal.component.scss'],
})

export class ProductModalComponent  implements OnInit {
  selectedProductId:number = 0;
  amount:string="";

  @Input() shipmentId!: number;
  Products:Product[] = [];

  constructor(private productsService:ProductsService,
              private shipmentService:shipmentService,
              private modalController: ModalController 
              ){}
              
  ngOnInit() {
    this.GetListOfProducts();
  }

  GetListOfProducts(){
    this.productsService.getProducts().subscribe((data:Product[])=>{
      this.Products = data;
    })
  }

  onSubmit(){
    console.log(this.generateProduct());
    this.shipmentService.InsertProductToShipment(this.generateProduct()).subscribe((data)=>{
      console.log(data);
      this.modalController.dismiss();
    })
  }

  generateProduct(){
    const producto:ShipmentAddProductDTO = {
      idProduct : this.selectedProductId,
      idShipment: this.shipmentId,
      amount: this.amount,
    }
    return producto;
  }

  cancelAction(){
    this.modalController.dismiss();
  }
}
