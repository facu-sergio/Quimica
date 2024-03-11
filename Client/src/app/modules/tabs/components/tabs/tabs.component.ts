import { Component, OnInit } from '@angular/core';
import { MenuController } from '@ionic/angular';

@Component({
  selector: 'app-tabs',
  templateUrl: './tabs.component.html',
  styleUrls: ['./tabs.component.scss'],
})
export class TabsComponent  implements OnInit {

  constructor(private menucontroller:MenuController) { }

  ngOnInit() {}
  openMenu(){
    this.menucontroller.toggle();
  }
}
