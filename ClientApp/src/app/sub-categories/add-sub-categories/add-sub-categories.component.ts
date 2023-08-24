import { Category } from 'src/app/categories/interfaces/category';

import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

import { SubCategory } from '../interfaces/subcategory';

@Component({
  selector: 'app-add-sub-categories',
  templateUrl: './add-sub-categories.component.html',
  styleUrls: ['./add-sub-categories.component.scss']
})
export class AddSubCategoriesComponent implements OnInit {
  items: Category[] = [];
  name: string = '';
  categoryId: number = 0;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {
    this.http.get<Category[]>(this.baseUrl + 'category/get').subscribe(result => {
      this.items = result;
    });
  }

  submit() {
    const body: SubCategory = {
      name: this.name,
      categoryId: this.categoryId
    };

    this.http.post(this.baseUrl + 'subcategory/post', body).subscribe(response => {
      console.log(response);
    });
  }
}
