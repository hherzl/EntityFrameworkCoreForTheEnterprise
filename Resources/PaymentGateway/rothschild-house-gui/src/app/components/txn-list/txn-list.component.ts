import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { TxnListDataSource } from './txn-list-datasource';
import { PaymentGatewayClientService, TxnItemModel } from 'src/app/services/payment-gateway-client.service';
import { ListResponse } from 'src/app/services/common';

@Component({
  selector: 'app-txn-list',
  templateUrl: './txn-list.component.html',
  styleUrls: ['./txn-list.component.css']
})
export class TxnListComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<TxnItemModel>;
  dataSource!: TxnListDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = [
    'id',
    'transactionDateTime',
    'transactionType',
    'transactionStatus',
    'issuingNetwork',
    'cardNumber',
    'orderTotal',
    'currency'
  ];

  response!: ListResponse<TxnItemModel>;

  constructor(private paymentGatewayClient: PaymentGatewayClientService) {
  }

  ngAfterViewInit(): void {
    this.paymentGatewayClient.getTxn().subscribe(result => {
      this.response = result;
      this.dataSource = new TxnListDataSource(this.response.model);

      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      this.table.dataSource = this.dataSource;
    });
  }
}
