import { Component, OnInit, ViewChild } from '@angular/core';
import { MenuController } from '@ionic/angular';

@Component({
  selector: 'app-tabs',
  templateUrl: './tabs.component.html',
  styleUrls: ['./tabs.component.scss'],
})
export class TabsComponent implements OnInit {


  constructor(private menuCtrl: MenuController) {}

  ngOnInit() {}

  openMenu() {
 
    this.menuCtrl.open('first-menu'); // Abre el menú
  }
}