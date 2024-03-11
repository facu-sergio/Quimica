import { Component } from '@angular/core';
import{faFolder}  from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  faFolder=faFolder;
  constructor() {}
}
