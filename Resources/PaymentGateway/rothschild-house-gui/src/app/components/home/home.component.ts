import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  private hubConnection!: signalR.HubConnection;

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7251/txnhub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build();
    
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started!');
      })
      .catch(err => {
        console.log(`Error while starting connection: ${err}`);
      })
  }

  public addListener = () => {
    this.hubConnection.on('receiveTxn', (clientApplication, amount, currency) => {
      console.log(`'${clientApplication}': ${amount} ${currency}`);
    });
  }

  public ngOnInit(): void {
    this.startConnection();
    this.addListener();
  }
}
