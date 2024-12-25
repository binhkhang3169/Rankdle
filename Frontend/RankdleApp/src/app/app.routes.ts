import { LoginComponent } from './components/login/login.component';
import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { QuizComponent } from './components/quiz/quiz.component';
import { RegisterComponent } from './components/register/register.component';
import { MenusComponent } from './components/menus/menus.component';

export const routes: Routes = [
    {
        path:'',
        redirectTo:'login',
        pathMatch:'full'
    },
    {
        path:'login',
        component:LoginComponent
    },
    {
        path:'',
        component:MenusComponent,
        children:[
            {
                path:'quiz',
                component:QuizComponent
            }
        ]
    }

];
