//src\app\app.component.ts
import { Component } from '@angular/core';
import { WebsocketService } from 'app/services/websocket.service';
import { Message } from 'app/services/websocket.service';
import { Location } from 'app/model/location.model';
import { DeliveryService } from 'app/services/delivery.service';
import * as mapboxgl from 'mapbox-gl';
import { environment } from 'environments/environment';

@Component({
  selector: "delivery-map-root",
  templateUrl: "./delivery-map.component.html",
  styleUrls: ["./delivery-map.component.css","ol.css"],
  providers: [WebsocketService]
})

export class DeliveryMapComponent {
  //socket config
  title = 'socketrv';
  content = '';
  received:string[]=[];
  sent:Message[] = [];


  public location:Location;

  private stop:boolean=false;
  

  map: mapboxgl.Map;
  style = 'mapbox://styles/tibbers/clit7e8t300w001pfdvwq4yxs';
  lat = 45.2462275072543;
  lng = 19.84240494925378;

  currentMarker:mapboxgl.Marker;


  // uspostavljanje web socketa sa bekom
  constructor(private webSocket:  WebsocketService, private deliveryService:DeliveryService) {
    
    webSocket.messages.subscribe(msg => {
      this.received.push(JSON.stringify(msg));
      console.log(JSON.stringify(msg));
       let intermediary=msg as unknown;
       this.location=intermediary as Location;

       const marker = new mapboxgl.Marker()
      .setLngLat([this.location.Longitude,this.location.Latitude]);
      
      this.currentMarker=marker;
      this.currentMarker.addTo(this.map);
    
    }); 
  }

  

  // inicijalizacija mape
  ngOnInit(){
    (mapboxgl as any).accessToken = environment.mapbox.accessToken;
    this.map = new mapboxgl.Map({
      container: 'map',
      style: this.style,
      zoom: 13,
      center: [this.lng, this.lat]
  });    // Add map controls
  this.map.addControl(new mapboxgl.NavigationControl());
   }


  // zapocinjanje streaminga lokacija
  sendMsg() {
    let message = {
      source: '',
      content: ''
    };
    message.source = 'localhost';
    message.content = this.content;

    this.sent.push(message);
    this.webSocket.messages.next(message);
  }

  removeMarkers(){
    this.currentMarker.remove();
  }
  
}