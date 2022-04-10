import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AutoIncrement } from './autoincrement.service';
import { Item } from './item.interface';
import { ItemService } from './item.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  filter: "all" | "active" | "complete" = "all";

  tasks: Array<Item> = [
    { id: AutoIncrement.getNext(), title: "First task", isDone: false },
    { id: AutoIncrement.getNext(), title: "Second task", isDone: false }
  ];
  completedTasks: Array<Item> = [];

  constructor(private itemService: ItemService) { }

  ngOnInit(): void {
    this.itemService.getItems().subscribe(x => this.tasks = x)
  }

  getTasks(): Array<Item> {
    if (this.filter === "all") {
      return this.tasks;
    }
    return this.tasks.filter(task =>
      this.filter === "active" ? !task.isDone : task.isDone)
  }

  addTask(taskForm: NgForm): void {
    if (!taskForm.value.title || taskForm.value.title.length === 0) {
      return;
    }

    this.itemService
      .createItem({
        id: AutoIncrement.getNext(),
        title: taskForm.value.title,
        isDone: false
      })
      .subscribe(
        x => this.tasks.push({
          id: x.id,
          title: x.title,
          isDone: x.isDone
        })
      )
  }

  onComplete(task: Item): void {
    task.isDone = true;
    this.itemService
      .completeItem(task.id)
      .subscribe(x => task = x)
  }

  onDelete(task: Item): void {
    this.tasks = this.tasks.filter(a => a.id != task.id);
    this.itemService
      .deleteItem(task.id)
      .subscribe(() => {
        console.log(`Item with id:${task.id} was deleted`);
      })
  }
}
