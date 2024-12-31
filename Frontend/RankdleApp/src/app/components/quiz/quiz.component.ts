import { ItemSelectServiceService } from './../../services/item-select-service.service';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { QuizService } from '../../services/quiz.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserAnswerService } from '../../services/user-answer.service';
import { MatDialog } from '@angular/material/dialog';
import { AnswerStatisticsComponent } from '../answer-statistics/answer-statistics.component';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css'],
  imports: [CommonModule, FormsModule],
})
export class QuizComponent implements OnInit {
  sanitizedVideoUrl: SafeResourceUrl;
  region = '';
  point = 0;
  chooseItem = '';
  ans = '';
  typeMap = new Map<string, number>([
    ['lol', 1],
    ['val', 2],
  ]);
  stars = [1, 2, 3, 4, 5, 6];
  items: { id: number; label: string; isSelected: boolean; iconUrl: string }[] =
    [];
  clips: {
    clipId: number;
    quizId: number;
    name: string;
    url: string;
    region: string;
    isLocked: boolean;
    isCurrent: boolean;
    isParticipated: boolean;
    isLast: boolean;
  }[] = [];
  currentClipIndex: number = 0;

  constructor(
    private sanitizer: DomSanitizer,
    private quizService: QuizService,
    private itemService: ItemSelectServiceService,
    private userAnswerService: UserAnswerService,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog
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
          const game = data.games.find(
            (g: any) => g.shortName.toLowerCase() === gameType.toLowerCase()
          );
          if (game) {
            this.items = game.ranks.map((rank: any, index: number) => ({
              id: index + 1,
              label: rank.rank,
              isSelected: false,
              iconUrl: rank.iconUrl,
            }));

            // Kiểm tra tất cả các clip đã hoàn thành chưa
            this.clips.forEach((clip) => {
              clip.isParticipated = this.isClipCompleted(clip.quizId);
            });
          } else {
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
      clipId: index + 1, // ID riêng cho clip
      quizId: quiz.quizId, // ID riêng cho quiz
      name: `Clip ${index + 1}`,
      url: quiz.url,
      region: quiz.region,
      isLocked: index !== 0, // Chỉ clip đầu tiên được mở khóa
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

  resetSelectItem(): void {
    this.items.forEach((i) => (i.isSelected = false));
    this.chooseItem = '';
  }

  onClick(): void {
    const currentClip = this.clips[this.currentClipIndex];
    if (currentClip && this.chooseItem) {
      this.quizService.getQuiz(currentClip.quizId).subscribe(
        // Sử dụng quizId
        (quizData) => {
          const correctValue = quizData.correctValue;
          this.ans = quizData.correctValue;
          const selectedItem = this.items.find(
            (item) => item.label === this.chooseItem
          );
          const correctItem = this.items.find(
            (item) => item.label === correctValue
          );

          if (selectedItem && correctItem) {
            const difference = Math.abs(selectedItem.id - correctItem.id);
            let stars = 0;

            if (difference === 0) {
              stars = 2;
            } else if (difference === 1) {
              stars = 1;
            }

            this.point += stars;
            currentClip.isParticipated = true;

            // Xử lý clip tiếp theo
            const nextClipIndex = this.currentClipIndex + 1;
            this.currentClipIndex = nextClipIndex;
            if (nextClipIndex < this.clips.length) {
              this.clips[nextClipIndex].isLocked = false; // Mở khóa clip tiếp theo
              this.loadClip(nextClipIndex);
            } else {
              currentClip.isCurrent = true;
              // Tất cả các clip đã hoàn thành
              alert('Bạn đã hoàn thành tất cả các clip!');
            }

            // Lưu câu trả lời vào session
            sessionStorage.setItem(
              `clip_${this.currentClipIndex - 1}_answer`, // Sử dụng quizId trong session
              this.chooseItem
            );
            sessionStorage.setItem(
              `clip_${this.currentClipIndex - 1}_score`, // Sử dụng quizId trong session
              this.point.toString()
            );

            // Gửi dữ liệu trả lời vào API UserAnswer
            const token = localStorage.getItem('token');
            this.userAnswerService
              .submitAnswer(this.chooseItem, currentClip.quizId, token) // Sử dụng quizId
              .subscribe(
                (response) => {
                  console.log('Câu trả lời đã được ghi nhận:', response);
                },
                (error) => {
                  console.error('Lỗi khi gửi câu trả lời:', error);
                }
              );

            // Hiển thị thống kê qua popup
            this.userAnswerService.getUserAnswers(currentClip.quizId).subscribe(
              // Sử dụng quizId
              (answers) => {
                this.dialog.open(AnswerStatisticsComponent, {
                  width: '600px',
                  data: {
                    items: this.items, // Danh sách các items
                    quizId: currentClip.quizId, // Sử dụng quizId
                    answer: this.ans, // ID của bài quiz
                  },
                });
              },
              (error) => {
                console.error('Error fetching answers:', error);
                alert('Không thể tải dữ liệu thống kê.');
              }
            );
            this.resetSelectItem();
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

  navigateClip(direction: string): void {
    let nextIndex = this.currentClipIndex;

    if (direction === 'next' && this.currentClipIndex < this.clips.length - 1) {
      nextIndex++;
    } else if (direction === 'prev' && this.currentClipIndex > 0) {
      nextIndex--;
    }

    if (nextIndex !== this.currentClipIndex) {
      this.loadClip(nextIndex);
    }
  }

  isClipCompleted(clipId: number): boolean {
    const answer = sessionStorage.getItem(`clip_${clipId}_answer`); // Sử dụng quizId trong session
    return answer !== null;
  }
}
