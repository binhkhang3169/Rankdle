import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MenusComponent } from './components/menus/menus.component';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet,MenusComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'RankdleApp';
}
