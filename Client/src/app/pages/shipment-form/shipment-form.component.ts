import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastController } from '@ionic/angular';
import { Product } from 'src/app/modules/core/models/Product';
import { ProductShipmentDto } from 'src/app/modules/core/models/productShipmentDto';
import { ShipmentCreateDto } from 'src/app/modules/core/models/shipmentCreateDto';
import { ProductsService } from 'src/app/services/productsservice.service';
import { shipmentService } from 'src/app/services/shipmentService.service';

@Component({
  selector: 'app-shipment-form',
  templateUrl: './shipment-form.component.html',
  styleUrls: ['./shipment-form.component.scss'],
})
export class ShipmentFormComponent implements OnInit {

  shipmentForm!:FormGroup;
  startDate = new Date().toISOString().slice(0, 10)
  Products:Product[] = [];
  selectError: boolean|undefined = false; 
  step:number = 1;

  constructor(
     private formBuilder: FormBuilder,
     private shipmentService:shipmentService,
     private productService:ProductsService,
     private toastController: ToastController) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  ionViewWillEnter(){
    this.initializeForm();
    this.step=1;
  }
  
  private initializeForm(): void {
    this.getListOfProducts();
    this.shipmentForm = this.createForm();
  }
 

  validarSelect() {
    this.selectError = this.shipmentForm.get('location_id')?.hasError('required');
  }


  validateOnTouched(campo:string){
   return this.shipmentForm.get(campo)?.invalid && this.shipmentForm.get(campo)?.touched
  }

  validateOnToucchedProduct(position:number,campo:string){
    const array:FormArray = this.shipmentForm.get('products') as FormArray;
    const formgroup:FormGroup = array.at(position) as FormGroup;
    return formgroup.get(campo)?.invalid && formgroup.get(campo)?.touched;
  }

  removeProduct(index: number) {
    this.products.removeAt(index);
  }

  onsubmit() {
    this.validarSelect();
    if(this.shipmentForm.invalid){
      return Object.values(this.shipmentForm.controls).forEach(control =>{
        control.markAllAsTouched();
      })
    }else{
      this.submitShipment();
    }
  }

  private submitShipment(): void {
    this.shipmentService.insertShipments(this.generateShipment())
      .subscribe({
        next: (response) => {
          this.presentSuccessToast('Pedido guardado', true);
        },
        error: (error) => {
          this.presentSuccessToast('Ocurrió un error', false);
        }
      });
  }
  


  private async presentSuccessToast(message:string,isSuccess:boolean): Promise<void> {
    const toast = await this.toastController.create({
      message: message,
      duration: 2000,
      position: 'bottom',
      color: isSuccess ? 'success': 'danger'
    });
    toast.present();
  }
  getListOfProducts(){
    this.productService.getProducts().subscribe((data:Product[])=>{
      this.Products = data;
    })
  }


  private generateShipment(): ShipmentCreateDto {
    const formData = this.shipmentForm.value;
    const formDataProducts = formData.products as ProductShipmentDto[];
    const productsList: ProductShipmentDto[] = formDataProducts || [];
  
    const shipmentData: ShipmentCreateDto = {
      clientName: formData.clientName || '',
      price: formData.price || 0,
      note: formData.note || '',
      locationId: formData.location_id || 0,
      street: formData.street || '',
      number: formData.number || '',
      state: formData.state || 0,
      date: formData.date || new Date(),
      products: productsList,
    };
    return shipmentData;
  }
  

  //Armado de formulario
  private createForm(): FormGroup{
    return  this.formBuilder.group({
       clientName: ['',Validators.required],
       location_id: [null, Validators.required],
       street: ['', Validators.required],
       number: ['', Validators.required],
       price: [null, Validators.required],
       note: ['', Validators.required],
       state: [null, Validators.required],
       date: [null, Validators.required],
       products: this.formBuilder.array([]),
     });
   }

   addProduct() {
    const newProduct = this.formBuilder.group({
      id: ['', Validators.required],
      amount: ['', Validators.required],
    });
    this.products.push(newProduct);
  }

  get products() {
    return this.shipmentForm.controls['products'] as FormArray;
  }

  nextStep(){
    if(this.step === 1){
      if(this.validateStepOne()){
        this.step+=1;
      }
    }

    if(this.step===2){
      if(this.validateStepTwo())
      {
        this.step+=1;
      }
    }

    if(this.step===3){
      this.validateStepThree();
    }
  }

  validateStepOne():boolean{
    if (this.shipmentForm.controls['clientName'].invalid ||
      this.shipmentForm.controls['location_id'].invalid ||
      this.shipmentForm.controls['street'].invalid ||
      this.shipmentForm.controls['number'].invalid)
      {
      this.shipmentForm.controls['clientName'].markAsTouched();
      this.shipmentForm.controls['location_id'].markAsTouched();
      this.shipmentForm.controls['street'].markAsTouched();
      this.shipmentForm.controls['number'].markAsTouched();
      return false;

      }else{
        return true;
      }
  }

  validateStepTwo():boolean {

    if(this.shipmentForm.controls['price'].invalid || this.shipmentForm.controls['date'].invalid
    || this.shipmentForm.controls['note'].invalid || this.shipmentForm.controls['state'].invalid)
    {
      this.shipmentForm.controls['price'].markAsTouched();
      this.shipmentForm.controls['date'].markAsTouched();
      this.shipmentForm.controls['note'].markAsTouched();
      this.shipmentForm.controls['state'].markAsTouched();
      return false;
    }else{
      return true;
    }
  }
  

  validateStepThree() {

    const productsArray = this.shipmentForm.controls['products'] as FormArray;
    let isValid = true;
  
    // Verificar si al menos se ha agregado un producto
    if (productsArray.length === 0) {
      isValid = false;
      // Mostrar un mensaje de error o realizar alguna acción en caso de que no haya productos agregados
    } else {
      // Verificar la validez de cada producto
      productsArray.controls.forEach(product => {
        if (product.invalid) {
          // Marcar el producto como tocado si es inválido
          product.markAsTouched();
          isValid = false;
        }
      });
    }
  
    // Si todos los productos son válidos, avanzar al siguiente paso
    if (isValid) {
      this.step += 1;
    }
  }

  
  prevStep(){
    this.step -=1;
  }


  goToStep(step: number) {
    this.step = step;
  }
}
