import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { ShipmentDto } from 'src/app/modules/core/models/shipmentDto';
import { DashBoardService } from 'src/app/services/dash-board.service';

@Component({
  selector: 'app-shipment-summary',
  templateUrl: './shipment-summary.component.html',
  styleUrls: ['./shipment-summary.component.scss'],
})
export class ShipmentSummaryComponent implements OnInit {
  @ViewChild('barCanvas', { static: true }) public barCanvas!: ElementRef;
  barChart: any;
  shipmentsList: ShipmentDto[] = [];
  
  // Nueva propiedad para contar los pedidos del mes actual
  currentMonthCount: number = 0;

  monthLabels: string[] = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];
  monthData: number[] = Array(12).fill(0);

  constructor(private dashBoardServices: DashBoardService) {}

  ngOnInit() {
    // Registrar los componentes de Chart.js
    Chart.register(...registerables);

    // Obtener el año actual
    const currentYear = new Date().getFullYear();
    
    // Establecer el rango de fechas para todo el año actual
    const dateFrom = new Date(`${currentYear}-01-01`);
    const dateTo = new Date(`${currentYear}-12-31`);

    // Obtener los envíos por rango de fechas
    this.dashBoardServices.getShipmenstByDateRange(dateFrom, dateTo).subscribe(data => {
      this.shipmentsList = data;
      this.groupShipmentsByMonth();
      this.countCurrentMonthShipments(); // Llamar a la función para contar los pedidos del mes actual
      this.loadChart();
    });
  }

  groupShipmentsByMonth() {
    this.shipmentsList.forEach(shipment => {
      if (shipment.date) {
        const shipmentDate = new Date(shipment.date);
        const month = shipmentDate.getMonth(); // Obtener el mes (0-11)
        this.monthData[month]++; // Incrementar el contador del mes correspondiente
      }
    });
  }

  // Nueva función para contar los pedidos del mes actual
  countCurrentMonthShipments() {
    const currentMonth = new Date().getMonth(); // Obtener el mes actual (0-11)
    this.currentMonthCount = this.monthData[currentMonth]; // Asignar el conteo del mes actual
  }
  loadChart() {
    const ctx = this.barCanvas.nativeElement;
    this.barChart = new Chart(ctx, {
      type: 'bar', // Gráfico de barras
      data: {
        labels: this.monthLabels,
        datasets: [{
          label: 'Total de Pedidos por Mes',
          data: this.monthData,
          backgroundColor: 'rgba(75, 192, 192, 0.6)',
          borderColor: 'rgba(75, 192, 192, 1)',
          borderWidth: 1
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'Cantidad de Pedidos' // Título para el eje Y
            }
          },
          x: {
            title: {
              display: true,
              text: 'Meses' // Título para el eje X
            }
          }
        },
        plugins: {
          legend: {
            display: true,
            position: 'top'
          }
        }
      }
    });
  }
  
  
  
  
}
