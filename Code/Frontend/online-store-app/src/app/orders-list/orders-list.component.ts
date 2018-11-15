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
  private response: PagedResponse<OrderInfo>;
  private columnsForOrders: string[];

  constructor(
    private formBuilder: FormBuilder,
    private salesService: SalesService) {
  }

  ngOnInit() {
    this.response = new PagedResponse<OrderInfo>();

    this.form = new FormGroup({
      pageSize: new FormControl('50')
    });

    this.columnsForOrders = [
      'orderID',
      'orderStatusDescription',
      'orderDate',
      'customerCompanyName',
      'total'
    ];

    this.search();
  }

  public search(): void {
    this.salesService.getOrders(this.form.get('pageSize').value, 1).subscribe((data: PagedResponse<OrderInfo>) => {
      this.response = data;
    });
  }

  public changePageSize(): void {
    this.search();
  }
}
