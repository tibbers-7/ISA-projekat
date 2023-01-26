//src\app\app.component.ts
import { Component } from '@angular/core';
import { MapsTooltipService, MarkerService } from '@syncfusion/ej2-angular-maps';
import { WebsocketService } from 'app/services/websocket.service';
import { Message } from 'app/services/websocket.service';
import { Location } from 'app/model/location.model';
import { DeliveryService } from 'app/services/delivery.service';

@Component({
  selector: "testproba-root",
  templateUrl: "./testproba.component.html",
  styleUrls: ["./testproba.component.css"],
  providers: [WebsocketService,MarkerService,MapsTooltipService]
})

export class TestprobaComponent {
  //socket config
  title = 'socketrv';
  content = '';
  received:Message[] = [];
  sent:Message[] = [];

  //maps
  public shapeData: object;
  public dataSource: object;
  public shapeSettings: object;
  public markerdataSource: object[];

  urlTemplate:string;
  public zoomSettings: object;
  public centerPosition:object;

  public location:Location;

  private stop:boolean=false;
  

  constructor(private webSocket:  WebsocketService, private deliveryService:DeliveryService) {
    
    webSocket.messages.subscribe(msg => {
      this.received.push(msg);
      //this.location=JSON.parse(msg);
      
       let intermediary=msg as unknown;
       this.location=intermediary as Location;

      this.markerdataSource = [
        { latitude:this.location.Latitude,longitude:this.location.Longitude, name: 'Novi Sad' }];
        //console.log(this.markerdataSource);
      //console.log("Response from websocket: " + msg);      
    }); 
  }

  ngOnInit(){
    
    this.zoomSettings = {
      enable: true,
      toolbars: ["Zoom", "ZoomIn", "ZoomOut", "Pan", "Reset"],
      shouldZoomInitially:true,
      //zoomFactor:13,
      
   }
   this.centerPosition={
    latitude:45.252259252152676, 
    longitude:19.837422262872497
   }
   this.urlTemplate = 'https://tile.openstreetmap.org/level/tileX/tileY.png';
//    this.shapeData = world_Map;
//     this.dataSource = colorMapping;
//  
    this.shapeSettings = { colorValuePath: 'color', };
    this.markerdataSource = [
      { latitude:45.252259252152676,longitude:19.837422262872497, name: 'Novi Sad' }
  ];

    this.sendMsg();
  }

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

  stopReceiving(){
    let message = {
      source: 'localhost',
      content: 'STOP'
    };

    this.sent.push(message);
    this.webSocket.messages.next(message);
  }

  
}