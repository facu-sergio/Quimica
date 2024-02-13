import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastController } from '@ionic/angular';
import { Product } from 'src/app/modules/core/models/Product';
import { ProductsService } from 'src/app/services/productsservice.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent implements OnInit {
  productForm: FormGroup;

  constructor(
    private productService: ProductsService,
    private toastController: ToastController,
    private formBuilder: FormBuilder
  ) {
    this.productForm = this.createProductForm();
  }

  ngOnInit() {}

  onSubmit(): void {
    if (this.productForm.invalid) {
      this.markFormControlsAsTouched();
    } else {
      const formData = this.productForm.value;
      const product: Product = {
        name: formData.nombre,
      };
      this.productService.createProduct(product).subscribe({
        next: () => this.handleProductCreationSuccess(),
        error: () => this.handleProductCreationError(),
      });
    }
  }

  private createProductForm(): FormGroup {
    return this.formBuilder.group({
      nombre: ['', Validators.required],
    });
  }

  private markFormControlsAsTouched(): void {
    Object.values(this.productForm.controls).forEach((control) => {
      control.markAllAsTouched();
    });
  }

  private handleProductCreationSuccess(): void {
    this.presentToast('Producto guardado', true);
  }

  private handleProductCreationError(): void {
    this.presentToast('Ocurrió un error', false);
  }

  private async presentToast(message: string, isSuccess: boolean): Promise<void> {
    const toast = await this.toastController.create({
      message: message,
      duration: 2000,
      position: 'bottom',
      color: isSuccess ? 'success' : 'danger',
    });
    toast.present();
  }
}
