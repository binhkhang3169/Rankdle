import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';


export interface CreateQuizDto {
  Region: string;
  CorrectValue: string;
  TypeId: number;
  TokenJWT: string;
  Url: string;
}

export interface TypeQuiz {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class CreatequizService {

  private baseUrl = `${environment.apiUrl}/api`;
  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  constructor(private http: HttpClient) { }

  // Lấy danh sách TypeQuiz
  getTypeQuiz(): Observable<TypeQuiz[]> {
    return this.http.get<TypeQuiz[]>(`${this.baseUrl}/typequiz`);
  }

  // Gửi CreateQuizDto để tạo Quiz mới
  addQuiz(quizDto: CreateQuizDto): Observable<any> {
    return this.http.post(`${this.baseUrl}/quiz`, quizDto, { headers: this.headers });
  }
}
