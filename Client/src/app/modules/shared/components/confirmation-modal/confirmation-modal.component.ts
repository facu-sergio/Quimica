import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrls: ['./confirmation-modal.component.scss'],
})
export class ConfirmationModalComponent  implements OnInit {
  @Input() message!: string;
  
  constructor(private modalController:ModalController) { }

  ngOnInit() {}

  confirmAction() {
    this.modalController.dismiss({ confirmed: true });
  }

  cancelAction() {
    this.modalController.dismiss({ confirmed: false });
  }
}
