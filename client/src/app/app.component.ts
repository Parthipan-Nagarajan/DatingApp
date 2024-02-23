import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterOutlet } from '@angular/router';
import { AppModuleModule } from './app-module/app-module.module';
import { FormsModule } from '@angular/forms';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule, CommonModule, FormsModule, AppModuleModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title: string = 'Dating Application';
  users: any;
  constructor(private accountService: AccountService) {
    console.log("App Component Constructor Called");
  }
  ngOnInit(): void {
    console.log("App Component Ng Init Called");
    this.setCurrentUser();
  }

  

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if(!userString) return;
    
    const user:User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);  
    
  }

}
