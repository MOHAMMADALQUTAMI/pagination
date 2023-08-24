import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-delete-category',
  templateUrl: './delete-category.component.html',
  styleUrls: ['./delete-category.component.scss']
})
export class DeleteCategoryComponent {
  @Input() id: number = 0;
  @Output() onDelete: EventEmitter<number> = new EventEmitter<number>();


  delete(id: number) {
    this.onDelete.emit(id);
  }
}
