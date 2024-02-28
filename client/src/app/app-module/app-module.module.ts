import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavComponent } from '../nav/nav.component';
import { HomeComponent } from '../home/home.component';
import { RegisterComponent } from '../register/register.component';
import { MemberListComponent } from '../members/member-list/member-list.component';
import { MemberDetailComponent } from '../members/member-detail/member-detail.component';
import { RouterModule } from '@angular/router';
import { routes } from '../app.routes';
import { ToastrModule } from 'ngx-toastr';
import {  HttpClientModule} from '@angular/common/http';
import { SharedModule } from '../_modules/shared.module';
import { ErrorHandlerComponent } from '../errors/error-handler.component';
import { NotFoundComponent } from '../errors/not-found/not-found.component';
import { ServerErrorComponent } from '../errors/server-error/server-error.component';

@NgModule({
  declarations: [
    NavComponent, 
    HomeComponent, 
    RegisterComponent, 
    MemberListComponent, 
    MemberDetailComponent,
    ErrorHandlerComponent,
    NotFoundComponent,
    ServerErrorComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    ErrorHandlerComponent,
    NotFoundComponent,
    ServerErrorComponent,
    RouterModule,
    ToastrModule
  ]
})
export class AppModuleModule { }
