import { Component, OnInit } from '@angular/core';
import { FormulasService } from 'src/app/services/formulasService.service';
import { ToastController } from '@ionic/angular';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { register } from 'swiper/element/bundle';

register();
@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage implements OnInit {
  formulas:any[]=[];
  constructor(private formulasService: FormulasService,private toastController: ToastController) { }

  ngOnInit() {
    this.formulasService.getFormulas()
      .pipe(
        catchError((error) => {
          // Obtener el mensaje de error del objeto error
          const errorMessage = this.getErrorMessage(error);
          this.presentErrorToast(`Error: ${errorMessage}`);
          return throwError(error); // Reenviar el error
        })
      )
      .subscribe((res: any) => {
        this.formulas.push(...res);
      });
  }

  getErrorMessage(error: any): string {
    if (error instanceof Error) {
      // Si es un error de tipo Error, obtener el mensaje
      return error.message;
    } else if (typeof error === 'string') {
      // Si es una cadena, usarla como mensaje de error
      return error;
    } else {
      // En otros casos, convertir el objeto error a cadena
      return JSON.stringify(error);
    }
  }

  async presentErrorToast(message: string) {
    const toast = await this.toastController.create({
      message,
      duration: 5000000, // Duración del mensaje en milisegundos
      position: 'bottom', // Posición del mensaje en la pantalla
      color: 'danger' // Color del mensaje de error
    });
    toast.present();
  }

}
