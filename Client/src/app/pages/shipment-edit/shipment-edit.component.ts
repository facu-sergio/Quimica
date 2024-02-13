import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Shipment } from 'src/app/modules/core/models/shipment';
import { shipmentService } from 'src/app/services/shipmentService.service';

@Component({
  selector: 'app-shipment-edit',
  templateUrl: './shipment-edit.component.html',
  styleUrls: ['./shipment-edit.component.scss'],
})
export class ShipmentEditComponent  implements OnInit {

  shipmentForm!:FormGroup;
  shipment!:Shipment;
  idShipment:number= 0;
  selectError: boolean|undefined = false; 

  constructor(private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private shipmentService:shipmentService,
              private router: Router) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params =>{
      this.idShipment = Number(params.get('id'));
    }) 
    this.getShipmentById();
    this.shipmentForm = this.createForm();
  }

  private getShipmentById(){
    if(this.idShipment)this.shipmentService.GetShipmentById(this.idShipment).subscribe((response:Shipment) =>{
      this.shipment = response;
      this.shipmentForm = this.createFormWithValues();
    })
    
  }

  private createFormWithValues(): FormGroup {
    return this.formBuilder.group({
      clientName: [this.shipment.clientName, Validators.required],
      location_id: [String(this.shipment.address.location.id), Validators.required],
      street: [this.shipment.address.street, Validators.required],
      number: [this.shipment.address.number, Validators.required],
      price: [this.shipment.price, Validators.required],
      note: [this.shipment.note, Validators.required],
      state: [String(this.shipment.state), Validators.required],
      date: [this.formatDate(this.shipment.date), Validators.required],
    });
  }

  // Formatea la fecha al formato "YYYY-MM-DD"
  private formatDate(date: string): string {
    const formattedDate = new Date(date).toISOString().split('T')[0];
    return formattedDate;
  }
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
     });
   }

   validateOnTouched(campo:string){
    return this.shipmentForm.get(campo)?.invalid && this.shipmentForm.get(campo)?.touched
   }
   onsubmit(){
    if(this.shipmentForm.valid){
      const updatedShipment: Shipment = {
        
        clientName: this.shipmentForm.value.clientName,
        date: this.shipmentForm.value.date,
        id: this.idShipment,
        address: {
          id:this.shipment.address.id,
          location: {
            id: +this.shipmentForm.value.location_id
          },
          street: this.shipmentForm.value.street,
          number: this.shipmentForm.value.number
        },
        price: +this.shipmentForm.value.price,
        note: this.shipmentForm.value.note,
        state: +this.shipmentForm.value.state,
      };
      console.log(updatedShipment)
      this.shipmentService.updateShipment(updatedShipment).subscribe(response=>{
        this.router.navigate(['/tabs/shipments']) ; 
      })
    }
   }
}
