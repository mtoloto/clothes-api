import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { RouterModule } from '@angular/router';
import { AdminAuthGuard } from '../../../../core/guards/admin.auth.guard';

const routes = [
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AdminAuthGuard]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ],
  declarations: [HomeComponent]
})
export class HomeModule { }
