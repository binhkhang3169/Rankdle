import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  CreateQuizDto,
  CreatequizService,
} from '../../services/createquiz.service';
import { ItemSelectServiceService } from '../../services/item-select-service.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Component({
  selector: 'app-submitquiz',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './submitquiz.component.html',
  styleUrls: ['./submitquiz.component.css'],
  providers: [JwtHelperService],
})
export class SubmitquizComponent implements OnInit {
  games: {
    name: string;
    shortName: string;
    ranks: { rank: string; iconUrl: string }[];
    isSelected: boolean;
  }[] = [];
  ranks: { rank: string; iconUrl: string; isSelected: boolean }[] = [];
  types: {
    typeId: number;
    nameType: string;
    image: string;
    url: string;
    isSelected: boolean;
  }[] = [];
  selectedRank: string = '';
  selectedType: string = '';
  selectedTypeId: number = 0;
  isSelectedGame: boolean = false;
  createQuizDto: CreateQuizDto = {
    Region: '',
    CorrectValue: '',
    TypeId: 0,
    TokenJWT: '',
    Url: '',
  };

  constructor(
    private createquizService: CreatequizService,
    private itemService: ItemSelectServiceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.fetchData();
  }
  async fetchData(): Promise<void> {
    try {
      // Lấy danh sách games và ranks từ API
      const gamesData = await this.itemService.getRanks().toPromise();
      console.log('Dữ liệu games:', gamesData); // Log dữ liệu games

      // Kiểm tra và xử lý games
      if (gamesData && Array.isArray(gamesData.games)) {
        this.games = gamesData.games.map((game: any) => ({
          ...game,
          isSelected: false, // Đặt trạng thái mặc định là chưa được chọn
        }));
      } else {
        console.error('Dữ liệu games không hợp lệ');
      }

      // Lấy danh sách TypeQuiz từ API
      const typesData = await this.createquizService.getTypeQuiz().toPromise();
      console.log('Dữ liệu TypeQuiz:', typesData); // Log dữ liệu TypeQuiz

      // Kiểm tra và xử lý TypeQuiz
      if (Array.isArray(typesData)) {
        this.types = typesData.map((type: any) => ({
          ...type,
          isSelected: false, // Đặt trạng thái mặc định là chưa được chọn
        }));
      } else {
        console.error('Dữ liệu TypeQuiz không hợp lệ');
      }

      // Lấy token từ localStorage
      this.createQuizDto.TokenJWT = localStorage.getItem('token') || '';
    } catch (error) {
      console.error('Lỗi khi lấy dữ liệu:', error);
    }
  }
  // Chọn game
  selectGame(game: any): void {
    this.games.forEach((g) => (g.isSelected = false)); // Hủy chọn tất cả games
    game.isSelected = true; // Đánh dấu game được chọn
    this.selectedRank = ''; // Reset rank đã chọn
  }

  selectRank(rank: any): void {
    // Hủy chọn tất cả các rank
    this.ranks.forEach((r) => (r.isSelected = false));

    // Đánh dấu rank được chọn
    rank.isSelected = true;

    // Lưu rank đã chọn vào selectedRank
    this.selectedRank = rank.rank;
  }

  // Chọn TypeQuiz
  selectType(type: any): void {
    this.types.forEach((t) => (t.isSelected = false)); // Hủy chọn tất cả types
    type.isSelected = true; // Đánh dấu type được chọn
    this.selectedType = type.nameType;
    this.isSelectedGame = true;
    // Đánh dấu game đã được chọn
    this.isSelectedGame = true;

    // Tìm game có tên trùng với selectedType và lấy ranks của game đó
    const selectedGame = this.games.find(
      (game) => game.name === this.selectedType
    );

    // Nếu tìm thấy game, cập nhật ranks
    if (selectedGame) {
      this.ranks = selectedGame.ranks.map((rank) => ({
        ...rank,
        isSelected: false, // Đặt trạng thái mặc định là chưa được chọn
      }));
    } else {
      console.error('Không tìm thấy game tương ứng với type');
    }
  }

  // Gửi dữ liệu quiz
  submit(): void {
    const selectedRank = this.ranks.find((ranks) => ranks.isSelected);
    const selectedType = this.types.find((type) => type.isSelected);

    if (!this.ranks || !selectedType) {
      alert('Vui lòng chọn game và loại quiz trước khi gửi!');
      return;
    }

    this.createQuizDto.TypeId = selectedType.typeId; // TypeId từ type đã chọn
    this.createQuizDto.CorrectValue = this.selectedRank;

    this.createquizService.addQuiz(this.createQuizDto).subscribe({
      next: (response) => {
        console.log('Quiz đã được tạo:', response);
        alert('Tạo quiz thành công!');
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.error('Lỗi khi gửi dữ liệu:', err);
        alert('Đã xảy ra lỗi!');
      },
    });
  }
}
