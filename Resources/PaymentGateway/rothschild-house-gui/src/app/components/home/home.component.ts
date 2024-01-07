import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ChartConfiguration, ChartType } from 'chart.js';

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

  chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    scales: {
      y: {
        min: 0
      }
    }
  };

  dataSet!: ChartModel[];
  chartLabels: string[] = ['Real time data for the chart'];
  chartType: ChartType = 'bar';
  chartLegend: boolean = true;

  public ngOnInit(): void {
    this.startConnection();
    this.addListener();
    this.dataSet = new Array<ChartModel>();
    this.dataSet.push({ data: [5, 7, 9], label: 'Foo', backgroundColor: '#5491DA' });
    this.dataSet.push({ data: [10, 12, 14], label: 'Bar', backgroundColor: '#E74C3C' });
    this.dataSet.push({ data: [15, 18, 21], label: 'Baz', backgroundColor: '82E0AA' });
  }
}

export interface ChartModel {
  data: number[],
  label: string
  backgroundColor: string
}
