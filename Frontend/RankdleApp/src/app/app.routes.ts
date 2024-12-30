import { LoginComponent } from './components/login/login.component';
import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { QuizComponent } from './components/quiz/quiz.component';
import { RegisterComponent } from './components/register/register.component';
import { MenusComponent } from './components/menus/menus.component';
import { SubmitquizComponent } from './components/submitquiz/submitquiz.component';
import { TestComponent } from './components/test/test.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent,
  },

  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'quiz',
    component: QuizComponent,
  },
  {
    path: 'submitquiz',
    component: SubmitquizComponent,
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path:'test',
    component:TestComponent
  }
];
