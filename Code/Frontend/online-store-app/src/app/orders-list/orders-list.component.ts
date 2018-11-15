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

  constructor(
    private formBuilder: FormBuilder,
    private salesService: SalesService) {
  }

  ngOnInit() {
    this.response = new PagedResponse<OrderInfo>();
    this.form = new FormGroup({
      pageSize: new FormControl('')
    });
    this.salesService.getOrders(50, 1).subscribe((data: PagedResponse<OrderInfo>) => {
      this.response = data;
    });
  }
}
