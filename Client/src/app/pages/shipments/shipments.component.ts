// En tu componente ShipmentsComponent
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ShipmentDto } from 'src/app/modules/core/models/shipmentDto';
import { shipmentService } from 'src/app/services/shipmentService.service';

@Component({
  selector: 'app-shipments',
  templateUrl: './shipments.component.html',
  styleUrls: ['./shipments.component.scss'],
})
export class ShipmentsComponent implements OnInit, OnDestroy {
  private shipmentsUpdateSubscription!: Subscription;

  obtenerFechaActual = (): string => {
    const fechaActual: Date = new Date();
    const año = fechaActual.getFullYear();
    const mes = (fechaActual.getMonth() + 1).toString().padStart(2, '0');
    const dia = fechaActual.getDate().toString().padStart(2, '0');
    return `${año}-${mes}-${dia}`;
  };

  formatDate(): string {
    let date: Date = new Date();
    let day: string = '';
    let dayNumber: number = date.getDate();
    let month: string = this.months[date.getMonth()];
    let year: number = date.getFullYear();

    day = this.days[date.getDay()];

    return `${day} ${dayNumber} de ${month} del ${year}`;
  }

  months: string[] = ['enero', 'febrero', 'marzo', 'abril', 'mayo', 'junio', 'julio', 'agosto', 'septiembre', 'octubre', 'noviembre', 'diciembre'];
  days: string[] = ['lunes', 'martes', 'miércoles', 'jueves', 'viernes', 'sábado', 'domingo'];

  fecha: string = this.formatDate();
  shipments: ShipmentDto[] | undefined;

  constructor(private shipmenService: shipmentService) {}

  ngOnInit() {
    this.getShipments();
  }

  ngOnDestroy() {
    this.shipmentsUpdateSubscription.unsubscribe();
  }

  ionViewWillEnter() {
    this.getShipments();
  }

  getShipments() {
    const fechaActual = this.obtenerFechaActual();
    this.shipmenService.getShipments(fechaActual).subscribe((shipmentsList: ShipmentDto[]) => {
      this.shipments = shipmentsList;
    });
  }
  DeleteShipment({ shipmentId }: { shipmentId: number | undefined }) {
    this.shipmenService.DeleteShipment(shipmentId).subscribe(() => {
      this.getShipments();
    });
  }


  deleteProduct({ productId, shipmentId }: { productId: number | undefined, shipmentId: number | undefined }) {
    this.shipmenService.deleteProductOfShipment(productId, shipmentId).subscribe(() => {
      this.getShipments();
    });
  }
}
