import { ItemSelectServiceService } from './../../services/item-select-service.service';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { QuizService } from '../../services/quiz.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css'],
  imports:[CommonModule,FormsModule]
})
export class QuizComponent implements OnInit {
  sanitizedVideoUrl: SafeResourceUrl;
  region = '';
  point = 0;
  chooseItem = '';
  typeMap = new Map<string, number>([
    ['lol', 1],
    ['val', 2],
  ]);
  stars = [1, 2, 3, 4, 5, 6];
  items: { id: number; label: string; isSelected: boolean; iconUrl: string }[] = [];
  clips: {
    index: number;
    name: string;
    url: string;
    region:string;
    isLocked: boolean;
    isCurrent: boolean;
    isParticipated: boolean;
    isLast: boolean;
  }[] = [];
  currentClipIndex: number = 0;

  constructor(
    private sanitizer: DomSanitizer,
    private quizService: QuizService,
    private itemService:ItemSelectServiceService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.sanitizedVideoUrl = this.sanitizeUrl('');
  }

  ngOnInit(): void {
    const typeId = this.route.snapshot.queryParamMap.get('type');
    const today = new Date();
    const date = today.toISOString().split('T')[0]; // Lấy ngày ở định dạng 'yyyy-MM-dd'

    if (typeId) {
      const id = Number(this.typeMap.get(typeId));
      console.log(id);
      this.quizService.getDailyWithQuizDetails(id, date).subscribe(
        (dailyWithQuizzes) => {
          this.initializeClips(dailyWithQuizzes.quizzes);
          this.loadClip(0); // Hiển thị clip đầu tiên.
        },
        (error) => {
          console.error('Error loading Daily and Quizzes:', error);
          this.router.navigate(['/']);
        }
      );
    } else {
      console.warn('No type parameter found in the URL.');
      this.router.navigate(['/']);
    }

    const gameType = this.route.snapshot.queryParamMap.get('type');
    if (gameType) {
      this.itemService.getRanks().subscribe(
        (data) => {
          // Lọc game dựa trên tham số 'type'
          const game = data.games.find(
            (g: any) => g.shortName.toLowerCase() === gameType.toLowerCase()
          );
          if (game) {
            this.items = game.ranks.map((rank: any, index: number) => ({
              id: index + 1,
              label: rank.rank,
              isSelected: false,
              iconUrl: rank.iconUrl
            }));
          }
          else{
            this.router.navigate(['/']);      
          }
        },
        (error) => {
          console.error('Error loading ranks:', error);
        }
      );
    } else {
      this.router.navigate(['/']);
      console.warn('No type parameter found in the URL.');
    }
  }

  sanitizeUrl(url: string): SafeResourceUrl {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }

  initializeClips(quizzes: any[]): void {
    this.clips = quizzes.map((quiz, index) => ({
      index: index + 1,
      name: `Clip ${index + 1}`,
      url: quiz.url,
      region: quiz.region,
      isLocked: index !== 0, // Chỉ clip đầu tiên được mở khóa.
      isCurrent: index === 0,
      isParticipated: false,
      isLast: index === quizzes.length - 1,
    }));
  }

  loadClip(index: number): void {
    if (index >= 0 && index < this.clips.length) {
      this.currentClipIndex = index;
      const clip = this.clips[index];
      this.sanitizedVideoUrl = this.sanitizeUrl(clip.url);
      this.region = clip.region;
      this.clips.forEach((c) => (c.isCurrent = false));
      clip.isCurrent = true;
    }
  }

  selectItem(item: any): void {
    this.items.forEach((i) => (i.isSelected = false));
    item.isSelected = true;
    this.chooseItem = item.label;
  }

  onClick(): void {
    // Đánh dấu clip đã tham gia.
    const currentClip = this.clips[this.currentClipIndex];
    if (currentClip && this.chooseItem) {
      // Gọi API để lấy thông tin quiz
      this.quizService.getQuiz(currentClip.index).subscribe(
        (quizData) => {
          const correctValue = quizData.correctValue; // Giá trị đúng từ API
          const selectedItem = this.items.find((item) => item.label === this.chooseItem);
          const correctItem = this.items.find((item) => item.label === correctValue);
  
          if (selectedItem && correctItem) {
            // Tính toán độ chênh lệch giữa ID lựa chọn và ID đáp án đúng
            const difference = Math.abs(selectedItem.id - correctItem.id);
            let stars = 0;
  
            if (difference === 0) {
              stars = 2; // Đúng chính xác
            } else if (difference === 1) {
              stars = 1; // Sai lệch 1 đơn vị
            }
  
            this.point += stars; // Cộng điểm
            // alert(`Bạn đã được ${stars} sao. Tổng điểm hiện tại: ${this.point}`);
  
            // Cập nhật trạng thái clip
            currentClip.isParticipated = true;
  
            // Mở khóa clip tiếp theo nếu có
            const nextClipIndex = this.currentClipIndex + 1;
            if (nextClipIndex < this.clips.length) {
              this.clips[nextClipIndex].isLocked = false;
              this.loadClip(nextClipIndex);
            } else {
              alert('Bạn đã hoàn thành tất cả các clip!');
            }
          } else {
            alert('Đã xảy ra lỗi trong quá trình kiểm tra đáp án.');
          }
        },
        (error) => {
          console.error('Error fetching quiz details:', error);
          alert('Đã xảy ra lỗi khi kiểm tra đáp án.');
        }
      );
    } else {
      alert('Vui lòng chọn một hạng.');
    }
  }
  
}
