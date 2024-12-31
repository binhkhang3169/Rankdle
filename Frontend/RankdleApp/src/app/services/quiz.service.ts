import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Daily, DailyWithQuizzes, Quiz } from '../interfaces/model';

@Injectable({
  providedIn: 'root',
})
export class QuizService {
  private dailyApiUrl = `${environment.apiUrl}/api/Daily`;
  private quizApiUrl = `${environment.apiUrl}/api/Quiz`;

  constructor(private http: HttpClient) {}

  /**
   * Lấy thông tin Daily từ API
   * @param typeId ID của loại quiz
   * @param date Ngày cần lấy dữ liệu
   * @returns Observable chứa dữ liệu Daily
   */
  getDaily(typeId: number, date: string): Observable<any> {
    const url = `${this.dailyApiUrl}/${typeId}/${date}`;
    return this.http.get(url);
  }

  /**
   * Lấy thông tin Quiz từ API
   * @param quizId ID của quiz
   * @returns Observable chứa dữ liệu Quiz
   */
  getQuiz(quizId: number): Observable<any> {
    const url = `${this.quizApiUrl}/${quizId}`;
    return this.http.get(url);
  }

  getDailyWithQuizDetails(typeId: number, date: string): Observable<DailyWithQuizzes> {
    return this.getDaily(typeId, date).pipe(
      switchMap((daily: Daily) => {
        const quizRequests = [daily.quiz1, daily.quiz2, daily.quiz3].map((quizId) =>
          this.getQuiz(quizId)
        );
        return forkJoin(quizRequests).pipe(
          map((quizzes: Quiz[]) => ({
            ...daily,
            quizzes,
          }))
        );
      })
    );
  }
}