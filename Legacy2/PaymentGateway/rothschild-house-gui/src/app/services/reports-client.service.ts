import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportsClientService {
  private endpoint: string;

  constructor(private http: HttpClient) {
    this.endpoint = 'https://localhost:7252/api/v1';
  }

  public getYearlySales(year: number): Observable<ChartResponse> {
    const url = `${this.endpoint}/yearly-sale/${year}`;
    return this.http.get<ChartResponse>(url);
  }
}

export class ChartResponse {
  public labels!: string[];
  public datasets!: DatasetModel[];
  public backgroundColors!: string[];
}

export class DatasetModel {
  public data!: number[];
  public label!: string;
  public backgroundColor!: string;
}
