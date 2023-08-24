import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

import { Category } from '../interfaces/category';
import { PageEvent } from '@angular/material/paginator';
import { pagination } from '../interfaces/Pagination';
@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.scss']
})
export class CategoriesListComponent implements OnInit {
  items: pagination;
  displayedColumns:string[]=['name']
  length :number =0;
  pageIndex:number=1;
  pageSize:number=5;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {
    this.refresh();
  }

  delete(id: number) {
    this.http.delete(this.baseUrl + 'category/delete/' + id).subscribe(result => {
      this.refresh();
    });
  }

  refresh() {
    this.http.get<pagination>(
    `${this.baseUrl}category/getPagination?pageIndex=${this.pageIndex}&pageSize=${this.pageSize}`)
    .subscribe(result => {
      this.length = result.length;
      this.items = result;
    });
  }
  handlePageEvent(event:PageEvent){
      this.pageIndex=event.pageIndex;
      this.pageSize=event.pageSize;
      this.refresh();
  }

}
