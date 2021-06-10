import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component:  HomeComponent}, 
  { path: 'login', component: LoginComponent},
  { path: 'register', component:RegisterComponent },
  {path:'adminLogin',component:AdminLoginComponent},
  {path: 'dashboard' ,component:DashboardComponent},
  {
    path: "admin",
    loadChildren: () => import("./admin/admin.module").then(mod => mod.AdminModule)
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
