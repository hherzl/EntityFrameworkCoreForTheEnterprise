import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ChartConfiguration, ChartType } from 'chart.js';
import { ChartResponse, DatasetModel, ReportsClientService } from 'src/app/services/reports-client.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private reportsClient: ReportsClientService) {
  }

  private hubConnection!: signalR.HubConnection;

  public dataset!: DatasetModel[];
  public chartLabels!: string[];
  public chartType!: ChartType;
  public chartLegend!: boolean;
  chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    scales: {
      y: {
        min: 0
      }
    }
  };

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

  public loadChart(): void {
    this.chartType = 'bar';
    this.chartLegend = true;
    this.reportsClient.getYearlySales(2024).subscribe(result => {
      this.chartLabels = result.labels;
      this.dataset = result.datasets;
    });
  }

  public addListener = () => {
    this.hubConnection.on('receiveTxn', (clientApplication, amount, currency) => {
      console.log(`'${clientApplication}': ${amount} ${currency}`);
      this.loadChart();
    });
  }

  public ngOnInit(): void {
    this.startConnection();
    this.loadChart();
    this.addListener();
  }
}
