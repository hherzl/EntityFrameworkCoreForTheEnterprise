import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SalesService } from '../sales/sales.service';
import { PagedResponse, OrderInfo } from '../models';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrls: ['./orders-list.component.css']
})
export class OrdersListComponent implements OnInit {
  private form: FormGroup;
  private loading: boolean;
  private response: PagedResponse<OrderInfo>;
  private columnsForOrders: string[];
  private isPreviousDisabled: boolean;
  private isNextDisabled: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private salesService: SalesService) {
  }

  ngOnInit() {
    this.response = new PagedResponse<OrderInfo>();

    this.form = new FormGroup({
      pageSize: new FormControl('10')
    });

    this.columnsForOrders = [
      'orderID',
      'orderStatusDescription',
      'orderDate',
      'customerCompanyName',
      'total',
      'detailsCount',
      'referenceOrderID'
    ];

    this.search();
  }

  public changePageSize(): void {
    this.response.pageNumber = 1;
    this.search();
  }

  public search(): void {
    this.loading = true;
    this.salesService.getOrders(this.form.get('pageSize').value, this.response.pageNumber).subscribe((data: PagedResponse<OrderInfo>) => {
      this.response = data;
      this.isPreviousDisabled = this.response.pageNumber === 1;
      this.isNextDisabled = this.response.pageNumber >= this.response.pageCount;
      this.loading = false;
    });
  }

  previous(): void {
    this.response.pageNumber -= 1;
    this.search();
  }

  next(): void {
    this.response.pageNumber += 1;
    this.search();
  }
}
