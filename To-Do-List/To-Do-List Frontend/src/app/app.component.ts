import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AutoIncrement } from './autoincrement.service';
import { Item } from './item.interface';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  filter: "all" | "active" | "complete" = "all";

  tasks: Array<Item> = [
    { id: AutoIncrement.getNext(), title: "First task", isDone: false },
    { id: AutoIncrement.getNext(), title: "Second task", isDone: false }
  ];
  completedTasks: Array<Item> = [];

  getTasks(): Array<Item> {
    if (this.filter === "all") {
      return this.tasks;
    }
    return this.tasks.filter(task => 
      this.filter === "active" ? !task.isDone : task.isDone)
  }

  addTask(taskForm: NgForm): void {
    if (!taskForm.value.title || taskForm.value.title.length === 0)
      return;
    
    this.tasks.push({
      id: AutoIncrement.getNext(),
      title: taskForm.value.title,
      isDone: false
    });
  }

  onComplete(task: Item): void {
    task.isDone = true;
  }

  onDelete(task: Item): void {
    this.tasks = this.tasks.filter(a => a.id != task.id);
  }
}
