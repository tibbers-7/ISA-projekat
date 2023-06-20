import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'BloodBankFront';
  map: mapboxgl.Map;
  style = '';
  lat = 13.0569951;
  lng = 80.20929129999999;
  message = 'Hello World!';
}
