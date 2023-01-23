//src\app\app.component.ts
import { Component } from '@angular/core';
import { WebsocketService } from 'app/services/websocket.service';
import { Message } from 'app/services/websocket.service';

@Component({
  selector: "testproba-root",
  templateUrl: "./testproba.component.html",
  styleUrls: ["./testproba.component.css"],
  providers: [WebsocketService]
})

export class TestprobaComponent {
  title = 'socketrv';
  content = '';
  received:Message[] = [];
  sent:Message[] = [];

  constructor(private webSocket:  WebsocketService) {
    webSocket.messages.subscribe(msg => {
      this.received.push(msg);
      console.log("Response from websocket: " + msg);
    });

    
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
}