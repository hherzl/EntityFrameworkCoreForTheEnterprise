import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { TxnListComponent } from './components/txn-list/txn-list.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'txn', component: TxnListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
