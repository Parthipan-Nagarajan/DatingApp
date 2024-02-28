import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl: String = "http://localhost:5001/api/";

  private currentUserSource = new BehaviorSubject<User | null>(null);

  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {

  }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + "Account/login", model).pipe(
      map((response: User) => {
        const user = response;        
        if (user) {
          this.currentUserSource.next(user);
          localStorage.setItem('user', JSON.stringify(user));          
        }
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + "Account/register", model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.currentUserSource.next(user);
          localStorage.setItem('user', JSON.stringify(user));          
        }

        return user;
      })
    );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
