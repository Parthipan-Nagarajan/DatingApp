import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../_models/user';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl: string = environment.apiUrl + "AppUsers/";

  constructor(private http: HttpClient) {

  }

  getMembers() {
   return this.http.get<Member[]>(this.baseUrl + "users")
  }

  getMember(username: string) {
   return this.http.get<Member>(this.baseUrl + "user/" + username)
  }
}
