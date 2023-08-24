import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AddCategoryComponent } from './categories/add-category/add-category.component';
import { CategoriesListComponent } from './categories/categories-list/categories-list.component';
import { DeleteCategoryComponent } from './categories/delete-category/delete-category.component';
import { CounterComponent } from './counter/counter.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import {
  AddSubCategoriesComponent
} from './sub-categories/add-sub-categories/add-sub-categories.component';
import {
  SubCategoriesListComponent
} from './sub-categories/sub-categories-list/sub-categories-list.component';
import {MatTableModule} from '@angular/material/table';
import { MatPaginatorModule} from '@angular/material/paginator';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    CategoriesListComponent,
    AddCategoryComponent,
    DeleteCategoryComponent,
    SubCategoriesListComponent,
    AddSubCategoriesComponent,
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'categories', component: CategoriesListComponent },
      { path: 'add-category', component: AddCategoryComponent },
      {path: 'edit-category/:id', component: AddCategoryComponent},
      { path: 'counter', component: CounterComponent },
      { path: 'subcategories', component: SubCategoriesListComponent },
      { path: 'add-subcategory', component: AddSubCategoriesComponent },
    ]),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
