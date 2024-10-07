import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { LandingComponent } from './landing/landing.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NotAuthenticatedGuard } from './service/not_authenticated.guard';
import { AuthenticatedGuard } from './service/authenticated.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent, canActivate: [AuthenticatedGuard]},
  { path: 'landing', component: LandingComponent}, // Add AuthGuard to protect the route
  { path: '', redirectTo: '/landing', pathMatch: 'full' }, // Redirect to login by default
  { path: '**', redirectTo: '/login' }, // Handle any unknown routes
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
