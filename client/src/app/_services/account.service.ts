import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  
  baseUrl: string = environment.apiUrl;

  private currentUserSource = new BehaviorSubject<User | null>(null);

  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {

  }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + "Account/login", model).pipe(
      map((response: User) => {
        if (response) {
          this.setCurrentUser(response);   
        }
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + "Account/register", model).pipe(
      map((response: User) => {
        if (response) {
          this.setCurrentUser(response);         
        }
        return response;
      })
    );
  }

  setCurrentUser(user: User) {
    console.log(user);
    this.currentUserSource.next(user);
    localStorage.setItem('user', JSON.stringify(user));
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
