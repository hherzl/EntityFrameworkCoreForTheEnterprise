import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalesService {
  private baseUrl: string;

  constructor(private httpClient: HttpClient) {
    this.baseUrl = 'http://localhost:7000/api/v1/Sales';
  }

  public getOrders(pageSize: number, pageNumber: number): Observable<Object> {
    const parameters = [
      'pageSize=' + (pageSize == null ? '10' : pageSize.toString()),
      'pageNumber=' + (pageNumber == null ? '1' : pageNumber.toString())
    ];
    
    const url: string = [this.baseUrl, 'Order'].join('/') + '?' + parameters.join('&');

    console.log(url);

    return this.httpClient.get(url);
  }
}
