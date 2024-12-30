import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ItemSelectServiceService {
  private jsonUrl = './data.json'; // Đường dẫn tới file JSON

  constructor(private http: HttpClient) {}

  // Hàm để lấy dữ liệu từ JSON
  getRanks(): Observable<any> {
    return this.http.get(this.jsonUrl);
  }
}
