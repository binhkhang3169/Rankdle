import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizService } from '../../services/quiz.service';
import { DailyWithQuizzes } from '../../interfaces/model';

@Component({
  selector: 'app-test',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent {
  responseData: DailyWithQuizzes | null = null;
  errorMessage: string | null = null;

  constructor(private quizService: QuizService) {}

  // Test hàm getDailyWithQuizDetails
  testGetDailyWithQuizDetails(): void {
    this.clearResponse();

    const testTypeId = 1; // Thay bằng ID hợp lệ
    const testDate = '2024-12-30'; // Thay bằng ngày hợp lệ (YYYY-MM-DD)

    this.quizService.getDailyWithQuizDetails(testTypeId, testDate).subscribe({
      next: (data) => {
        console.log('Daily With Quiz Details:', data);
        this.responseData = data;
      },
      error: (err) => {
        console.error('Error:', err);
        this.errorMessage = 'Failed to fetch Daily with Quiz details.';
      }
    });
  }

  // Xóa dữ liệu trước đó
  private clearResponse(): void {
    this.responseData = null;
    this.errorMessage = null;
  }
}
