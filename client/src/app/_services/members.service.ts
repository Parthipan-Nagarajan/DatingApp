import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../_models/user';
import { Member } from '../_models/member';
import { map, of } from 'rxjs';
import { subscribe } from 'diagnostics_channel';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl: string = environment.apiUrl + "AppUsers/";
  members: Member[] = [];


  constructor(private http: HttpClient) {

  }

  getMembers() {
    if (this.members.length > 0) {
      return of(this.members);
    }
    return this.http.get<Member[]>(this.baseUrl + "users").pipe(map(members =>{
      this.members = members;
      return members;
    }))
  }

  getMember(username: string) {
    const member = this.members.find(x => x.userName == username);
    if(member) return of(member);
    return this.http.get<Member>(this.baseUrl + "user/" + username)
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'user/1', member).pipe(map(() =>{
      const index = this.members.indexOf(member);
      this.members[index] = {...this.members[index],...member};
    }));
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'set-main-photo/' + photoId,{});
  }

  deleteMainPhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'delete-photo/' + photoId,{});
  }

}
