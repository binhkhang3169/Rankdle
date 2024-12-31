import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserAnswerService {
  private baseUrl = `${environment.apiUrl}/api/UserAnswer`;

  constructor(private http: HttpClient) {}

  getUserAnswers(quizId: number): Observable<any[]> {
    const url = `${this.baseUrl}/${quizId}`;
    return this.http.get<any[]>(url);
  }
  submitAnswer(answer: string, quizId: number, token: string | null): Observable<any> {
    const body = {
      answer: answer,
      token: token,
      quizId: quizId
    };

    return this.http.post(this.baseUrl, body);
  }
}
