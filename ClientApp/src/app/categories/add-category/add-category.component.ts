import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../interfaces/category';
import { EMPTY, catchError, map } from 'rxjs';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.scss']
})
export class AddCategoryComponent implements OnInit {
  id  :number =0;
  name: string = '';

  constructor(private http: HttpClient,
    private router:Router,
    route:ActivatedRoute,
     @Inject('BASE_URL') private baseUrl: string) 
    {
       this.id= +route.snapshot.params['id'];
      
    }
  ngOnInit(): void {
    if(this.id > 0)
    {
      this.http.get<Category>(this.baseUrl + 'category/get/' + this.id)
      .pipe(
        map(result =>{
          return result.name;
          // result.name = result.name.toUpperCase()
          // return result;
        }),
        catchError((error) => {
          console.error(error);
          return EMPTY;
        })
        
      )
      
      .subscribe(result =>{
        this.name = result;
    });
  }
}

  addoreditCategory() 
  {
    const body = {
      name: this.name
    };
    let observable =null;
    if(this.id > 0)
    {
     observable = this.http.put(this.baseUrl + 'category/edit/'+ this.id, body)
    }else{
      observable = this.http.post(this.baseUrl + 'category/post', body)
    }
    observable
    .pipe(
      catchError((error) => {
      console.error(error);
      return EMPTY;
    }))
    .subscribe(()=>{
      this.router.navigate(['/categories'])
    });
    // if(this.id > 0)
    // {
    //   this.http.put(this.baseUrl + 'category/edit/'+ this.id, body)
    //   .pipe(
    //     catchError((error) => {
    //     console.error(error);
    //     return EMPTY;
    //   }))
    //   .subscribe(()=>{
    //     this.router.navigate(['/categories'])
    //   });

    // }else{
    //   this.http.post(this.baseUrl + 'category/post', body)
    //   .pipe(
    //     catchError((error) => {
    //     console.error(error);
    //     return EMPTY;
    //   }))
    //   .subscribe(()=>{
    //     this.router.navigate(['/categories'])
    //   });

    // }

  }
}

