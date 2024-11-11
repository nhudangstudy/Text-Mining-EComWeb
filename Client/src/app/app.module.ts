import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { AuthService } from './service/auth.service';
import { NotAuthenticatedGuard } from './service/not_authenticated.guard';
import { AuthInterceptor } from './service/http-interceptor';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AppRoutingModule } from './app-routing.module';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { LandingComponent } from './landing/landing.component';
import { ProductCardComponent } from './product-card/product-card.component';
import { ReviewCardComponent } from './review-card/review-card.component';
import { CategoryPageComponent } from './category-page/category-page.component';
import { NgxSliderModule } from '@angular-slider/ngx-slider';
import { NouisliderModule } from 'ng2-nouislider';
import { ProductPageComponent } from './product-page/product-page.component';
import { MessageBoxComponent } from './message-box/message-box.component';
import { LoadingComponent } from './loading/loading.component';
import { CustomDropdownComponent } from './custom-dropdown/custom-dropdown.component';
import { ClickOutsideDirective } from './click-outside.directive';
import { CommentsListComponent } from './comments-list/comments-list.component';
import { HeaderAdminComponent } from './header-admin/header-admin.component';
import { AdminPageComponent } from './admin-page/admin-page.component';
import { AdminOverviewComponent } from './admin-overview/admin-overview.component';
import { AdminProductComponent } from './admin-product/admin-product.component';
import { AdminTransactionComponent } from './admin-transaction/admin-transaction.component';
import { UserIconComponent } from './user-icon/user-icon.component';
import { CommonModule } from '@angular/common';
import { NgxChartsModule } from '@swimlane/ngx-charts'; // Import NgxChartsModule
import { AdminReportComponent } from './admin-report/admin-report.component';
import { SignupComponent } from './signup/signup.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    HeaderComponent,
    FooterComponent,
    LandingComponent,
    ProductCardComponent,
    ReviewCardComponent,
    CategoryPageComponent,
    ProductPageComponent,
    MessageBoxComponent,
    LoadingComponent,
    CustomDropdownComponent,
    ClickOutsideDirective,
    CommentsListComponent,
    HeaderAdminComponent,
    AdminPageComponent,
    AdminOverviewComponent,
    AdminProductComponent,
    AdminTransactionComponent,
    UserIconComponent,
    AdminReportComponent,
    SignupComponent,

  ],
  imports: [BrowserModule, HttpClientModule, AppRoutingModule, FormsModule, ReactiveFormsModule, NgxSliderModule, NouisliderModule, CommonModule, NgxChartsModule  ],
  exports: [AdminReportComponent],
  providers: [
    AuthService,
    NotAuthenticatedGuard,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
