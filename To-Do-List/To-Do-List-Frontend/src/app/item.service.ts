import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Item } from "./item.interface";


@Injectable()
export class ItemService {
    constructor(private http: HttpClient) { }

    getItems(): Observable<Item[]> {
        return this.http.get<Item[]>("http://localhost:5277/rest/Todo/get-all");
    }

    getItem(itemId: number): Observable<Item> {
        return this.http.get<Item>(`http://localhost:5277/rest/Todo/${itemId}`);
    }

    createItem(item: Item): Observable<Item> {
        return this.http.post<Item>("http://localhost:5277/rest/Todo/create", item);
    }

    completeItem(itemId: number): Observable<Item> {
        return this.http.put<Item>(`http://localhost:5277/rest/Todo/${itemId}/complete`, null);
    }

    deleteItem(itemId: number): Observable<any> {
        return this.http.delete(`http://localhost:5277/rest/Todo/${itemId}/delete`)
    }
}