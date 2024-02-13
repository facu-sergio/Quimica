import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalController, Platform } from '@ionic/angular';
import { ShipmentDto } from '../../../core/models/shipmentDto';
import {faPerson,faHouseUser,faBottleWater,faDeleteLeft,faCirclePlus,faTrash,faPenToSquare,
        faLocationDot, faWindowMaximize,faWindowMinimize} from '@fortawesome/free-solid-svg-icons';
import { shipmentService } from 'src/app/services/shipmentService.service';
import { ProductModalComponent } from '../product-modal/product-modal.component';
import { Router } from '@angular/router';
import { ConfirmationModalComponent } from 'src/app/modules/shared/components/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'app-shipment',
  templateUrl: './shipment.component.html',
  styleUrls: ['./shipment.component.scss'],
})

export class ShipmentComponent  implements OnInit {
  faWindowMaximize=faWindowMaximize;
  faWindowMinimize=faWindowMinimize;
  faLocationDot=faLocationDot;
  faPenToSquare=faPenToSquare;
  faTrash=faTrash;
  faCirclePlus = faCirclePlus;
  faDeleteLeft = faDeleteLeft;
  faBottleWater = faBottleWater;
  faPerson = faPerson;
  faHouseUser = faHouseUser;

  showDetails = false;

  @Input() shipment:ShipmentDto;
  @Output() deleteProductRequest = new EventEmitter<{ productId: number|undefined, shipmentId: number|undefined }>();
  @Output() deleteShipmentRequest = new EventEmitter<{shipmentId:number|undefined}>();
  @Output() refreshShipmentsRequest = new  EventEmitter<{}>();
  direccion: string = 'barrio del plata, groenlandia 164';

  shipmentId:number|undefined = 0;
  isConfirmationModalOpen = false;
  productIdToDelete: number | undefined;

  constructor(private platform: Platform,
              private shipmentService:shipmentService,
              private modalController: ModalController,
              private router: Router) {
    this.shipment = {} as ShipmentDto;
   }

  ngOnInit() {
    this.shipmentId = this.shipment.id;
  }

  abrirGoogleMaps() {
    const direccionConProvincia = `Provincia de Buenos Aires, ${this.direccion}`;
    const direccionEncoded = encodeURIComponent(direccionConProvincia);
    let url: string;

    if (this.platform.is('ios')) {
      // URL para dispositivos iOS
      url = `maps://maps.apple.com/?q=${direccionEncoded}`;
    } else {
      // URL para otros dispositivos (Android, etc.)
      url = `https://www.google.com/maps/search/?api=1&query=${direccionEncoded}`;
    }

    window.open(url, '_system');
  }

  deleteProduct(productId: number|undefined) {
    this.deleteProductRequest.emit({ productId, shipmentId: this.shipment.id });
  }

  DeleteShipment(){
    this.deleteShipmentRequest.emit({shipmentId: this.shipment.id})
  }


  openProductModal(shipmentId: number|undefined) {
    this.presentProductModal(shipmentId);
  }

  async presentProductModal(shipmentId: number|undefined) {
    const modal = await this.modalController.create({
      component: ProductModalComponent,
      componentProps: {   shipmentId: shipmentId
      },
      cssClass: 'custom-modal' 
    });
    modal.onDidDismiss().then((data) => {
      this.refreshShipmentsRequest.emit();
    });
    return await modal.present();
  }

  navigateToEditShipment(shipmentId: number|undefined): void {
    this.router.navigate(['/tabs/editShipment', shipmentId]);
  }

  toggleDetails() {
    this.showDetails = !this.showDetails;
  }


 async openConfirmationModal(producId:number|undefined){
    const modal = await this.modalController.create({
      component:ConfirmationModalComponent,
      componentProps:{
        message: '¿Desea eliminar este producto?',
      },
      cssClass: 'confirmation-modal' 
    });
    modal.onDidDismiss().then((data) => {
      if (data.data && data.data.confirmed) {
        this.deleteProduct(producId)
        this.refreshShipmentsRequest.emit();
      }
    });

    return await modal.present();
  }

  async confirmationDeleteShipment(){
    console.log("hi")
    const modal = await this.modalController.create({
      component:ConfirmationModalComponent,
      componentProps:{
        message:'¿Desea eliminar este Pedido?'
      },
      cssClass:'confirmation-modal' 
    });

     modal.onDidDismiss().then((data)=>{
      if (data.data && data.data.confirmed){
        this.DeleteShipment();
        this.refreshShipmentsRequest.emit();
      }
    })

    return await modal.present();
  }
  
}
