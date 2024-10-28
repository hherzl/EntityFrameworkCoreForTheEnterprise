import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ListResponse } from './common';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PaymentGatewayClientService {

  private endpoint: string;

  constructor(private http: HttpClient) {
    this.endpoint = 'https://localhost:7250/api/v1';
  }

  public getTxn(): Observable<ListResponse<TxnItemModel>> {
    const url = `${this.endpoint}/transaction`;
    return this.http.get<ListResponse<TxnItemModel>>(url);
  }
}

export class TxnItemModel {
  public id!: number;
  public transactionDateTime!: Date;
  public transactionTypeId!: number;
  public transactionType!: string;
  public transactionStatusId!: number;
  public transactionStatus!: string;
  public clientApplicationId!: number;
  public clientApplication!: string;
  public customerId!: string;
  public cardId!: string;
  public issuingNetwork!: string;
  public cardNumber!: string;
  public orderTotal!: number;
  public currency!: string;
  public creationDateTime!: Date;
}
