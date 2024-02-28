import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-error-handler',
  standalone: false,
  templateUrl: './error-handler.component.html',
  styleUrl: './error-handler.component.css'
})

export class ErrorHandlerComponent {
  baseUrl = "http://localhost:5001/api/";
  validationErrors: any[] = [];

  constructor(private http: HttpClient) {

  }

  get404Error() {
    this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
      next: response => console.log(response),
      error: error => console.log(error),
      complete: () => console.log("Request Completed for 404")
    })
  }

  get400Error() {
    this.http.get(this.baseUrl + 'buggy/bad-request').subscribe({
      next: response => console.log(response),
      error: error => console.log(error),
      complete: () => console.log("Request Completed for 404")
    })
  }

  get500Error() {
    this.http.get(this.baseUrl + 'buggy/server-error').subscribe({
      next: response => console.log(response),
      error: error => console.log(error),
      complete: () => console.log("Request Completed for 404")
    })
  }

  get401Error() {
    this.http.get(this.baseUrl + 'buggy/auth').subscribe({
      next: response => console.log(response),
      error: error => console.log(error),
      complete: () => console.log("Request Completed for 404")
    })
  }

  get400ValidationError() {
    this.http.post(this.baseUrl + 'account/register', { username: 'admin', password: '123' }).subscribe({
      next: response => console.log(response),
      error: error => { this.validationErrors = error; console.log(error) },
      complete: () => console.log("Request Completed for 404")
    })
  }

}
