import { Component, OnInit } from '@angular/core';
import { SalesService } from '../sales.service';
import { PagedResponse, OrderInfo } from '../models';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrls: ['./orders-list.component.css']
})
export class OrdersListComponent implements OnInit {
  private response: PagedResponse<OrderInfo>;

  constructor(private salesService: SalesService) {
  }

  ngOnInit() {
    this.salesService.getOrders(50, 1).subscribe((data: PagedResponse<OrderInfo>) => {
      this.response = data;
    });
  }

}
