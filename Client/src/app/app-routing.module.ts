import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { LandingComponent } from './landing/landing.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NotAuthenticatedGuard } from './service/not_authenticated.guard';
import { AuthenticatedGuard } from './service/authenticated.guard';
import { CategoryPageComponent } from './category-page/category-page.component';
import { ProductPageComponent } from './product-page/product-page.component';
import { AdminPageComponent } from './admin-page/admin-page.component';
import { AdminOverviewComponent } from './admin-overview/admin-overview.component';
import { AdminProductComponent } from './admin-product/admin-product.component';
import { AdminReportComponent } from './admin-report/admin-report.component';
import { AdminTransactionComponent } from './admin-transaction/admin-transaction.component';
import { AdminGuard } from './service/admin.guard';
import { SignupComponent } from './signup/signup.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, canActivate: [AuthenticatedGuard] },
  { path: 'landing', component: LandingComponent }, // Add AuthGuard if needed
  { path: 'signup', component: SignupComponent, canActivate: [AuthenticatedGuard]}, // Add AuthGuard if needed
  { path: 'category', component: CategoryPageComponent },
  { path: 'product/:selected', component: ProductPageComponent },
  { path: 'category/:selected', component: CategoryPageComponent },

  // Admin Page Route with Child Routes
  {
    path: 'admin',
    component: AdminPageComponent,
    children: [
      { path: 'overview', component: AdminOverviewComponent },
      { path: 'product', component: AdminProductComponent },
      { path: 'transaction', component: AdminTransactionComponent },
      { path: 'report', component: AdminReportComponent },
      { path: '', redirectTo: 'overview', pathMatch: 'full' }, // Default to Overviewre
    ],
    canActivate: [AdminGuard]
  },

  // Default Route
  { path: '', redirectTo: '/landing', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
