import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Item } from '../item.interface';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent {
  @Input() task!: Item
  @Output() onCompleteItem = new EventEmitter();
  @Output() onDeleteItem = new EventEmitter();

  complete(): void {
    this.onCompleteItem.emit(this.task);
  }

  delete(): void {
    this.onDeleteItem.emit(this.task);
  }
}
