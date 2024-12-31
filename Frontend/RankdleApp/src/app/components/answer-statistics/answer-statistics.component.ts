import {
  Component,
  Inject,
  OnInit,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Chart } from 'chart.js/auto';
import { UserAnswerService } from '../../services/user-answer.service';
import { ItemSelectServiceService } from '../../services/item-select-service.service';

@Component({
  selector: 'app-answer-statistics',
  templateUrl: './answer-statistics.component.html',
  styleUrls: ['./answer-statistics.component.css'],
})
export class AnswerStatisticsComponent implements OnInit {
  @ViewChild('answerChart') answerChart!: ElementRef<HTMLCanvasElement>; // Truy xuất phần tử canvas
  chart: any;
  itemStats: { label: string; correctCount: number; totalCount: number }[] = [];
  iconUrls: { [key: string]: string } = {};
  constructor(
    public dialogRef: MatDialogRef<AnswerStatisticsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { answer:string; quizId:number; items: any[] }, // Nhận cả danh sách 'answers' và 'items',
    private userAnswerService: UserAnswerService,
    private itemService: ItemSelectServiceService
  ) {}

  ngOnInit(): void {
    this.fetchAnswers();
    this.loadRankData();
  }
  ngAfterViewInit(): void {
    // Chỉ render biểu đồ sau khi View đã được khởi tạo
    console.log(this.data.answer);
    this.renderChart();
  }
  
  loadRankData(): void {
    this.itemService.getRanks().subscribe((ranksData) => {
      ranksData.games.forEach((game: any) => {
        game.ranks.forEach((rank: any) => {
          this.iconUrls[rank.rank] = rank.iconUrl;
        });
      });
      console.log(this.iconUrls); // Kiểm tra dữ liệu sau khi tải
    });
  }
  fetchAnswers(): void {
    this.userAnswerService.getUserAnswers(this.data.quizId).subscribe(
      (response) => {
        console.log('Fetched Answers:', response);
        const answers = response.map((item) => item.answer); // Lấy danh sách câu trả lời từ API
        this.calculateItemStats(answers); // Tính toán thống kê
        this.renderChart(); // Render biểu đồ
      },
      (error) => {
        console.error('Failed to fetch answers:', error);
      }
    );
  }
  // Tính toán số lần trả lời đúng và tổng số lần mỗi item xuất hiện
  calculateItemStats(answers: string[]): void {
    console.log('Items:', this.data.items);
    console.log('Answers:', answers);

    this.itemStats = this.data.items.map((item) => {
      const correctCount = answers.filter((answer) => answer === item.label).length;

      return {
        label: item.label,
        correctCount,
        totalCount: answers.length,
      };
    });

    console.log('Calculated Item Stats:', this.itemStats);
  }
  renderChart(): void {
    const canvasElement = this.answerChart?.nativeElement;
    if (!canvasElement) {
      console.error('Canvas element not found.');
      return;
    }

    const ctx = canvasElement.getContext('2d');
    if (!ctx) {
      console.error('Failed to get 2D context from canvas.');
      return;
    }

    if (this.chart) {
      this.chart.destroy(); // Hủy biểu đồ cũ nếu đã tồn tại
    }

    const labels = this.itemStats.map((stat) => stat.label);
    const correctData = this.itemStats.map((stat) => stat.correctCount);

    this.chart = new Chart(ctx, {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [
          {
            label: 'Câu trả lời đúng',
            data: correctData,
            backgroundColor: 'rgba(75, 192, 192, 0.5)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1,
          },
        ],
      },
      options: {
        responsive: true,
        plugins: {
          legend: { display: true },
          title: {
            display: true,
            text: 'Thống kê câu trả lời',
          },
        },
        scales: {
          y: {
            beginAtZero: true,
          },
        },
      },
    });
  }
}
