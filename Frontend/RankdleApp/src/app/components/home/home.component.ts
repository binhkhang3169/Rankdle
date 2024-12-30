import { provideRouter, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { CreatequizService } from './../../services/createquiz.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'], // Đúng cú pháp
  imports:[CommonModule]
})
export class HomeComponent implements OnInit {
  cards: {
    typeId: number;
    nameType: string;
    image: string;
    url: string;
  }[] = [];

  responseData: any; // Thêm khai báo responseData để xử lý lỗi

  constructor(private createquizService: CreatequizService,private router:Router) {}

  ngOnInit(): void { // Đúng tên phương thức vòng đời
    this.createquizService.getTypeQuiz().subscribe({
      next: (data) => {
        console.log('TypeQuiz API Response:', data);
        this.cards = data.map((card: any) => ({
          ...card,
        })); // Map dữ liệu nếu cần
      },
      error: (err) => {
        console.error('Error calling TypeQuiz API:', err);
        this.responseData = err; // Ghi nhận lỗi
      },
    });
  }
  navigateTo(url: string): void {
    this.router.navigateByUrl(url);
  }
}
