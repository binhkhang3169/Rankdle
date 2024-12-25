import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // Để hỗ trợ *ngIf, *ngFor
import { FormsModule } from '@angular/forms'; // Để hỗ trợ [(ngModel)]

@Component({
  selector: 'app-menus',
  templateUrl: './menus.component.html',
  styleUrls: ['./menus.component.css'],
  standalone: true, // Standalone Component
  imports: [CommonModule, FormsModule] // Import CommonModule và FormsModule
})
export class MenusComponent {
  isSideMenuVisible = false; // Trạng thái hiển thị Side Menu
  searchQuery = ''; // Dữ liệu cho ô tìm kiếm
  notificationsCount = 3; // Số lượng thông báo
  messagesCount = 5; // Số lượng tin nhắn
  menuItems = [
    { label: 'Trang chủ', link: '/' },
    { label: 'Sản phẩm', link: '/products' },
    { label: 'Liên hệ', link: '/contact' }
  ];

  toggleSideMenu() {
    this.isSideMenuVisible = !this.isSideMenuVisible;
  }

  onSearch() {
    alert(`Đã tìm kiếm: ${this.searchQuery}`);
  }

  showNotifications() {
    alert('Xem thông báo!');
  }

  showMessages() {
    alert('Xem tin nhắn!');
  }

  viewProfile() {
    alert('Xem thông tin tài khoản!');
  }

  logout() {
    alert('Đăng xuất!');
  }
}
