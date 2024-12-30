import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MenusComponent } from './components/menus/menus.component';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { TestComponent } from './components/test/test.component';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet,MenusComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'RankdleApp';
}
