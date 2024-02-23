import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  registerMode = false;
  users: any;

  constructor(private http: HttpClient) {

  }
  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getUsers() {
    this.http.get('http://localhost:5001/api/AppUsers').subscribe({
      next: (response) => { this.users = response },
      error: (error) => { console.log(error) },
      complete: () => { console.log('Request has Completed') }
    })
  }

  cancleRegisterMode(flag: boolean) {
    if (!flag) {
      this.registerMode = false;
    }
  }

}
