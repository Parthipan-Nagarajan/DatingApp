import { Component, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-member-list',
  standalone: false,
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {

  members$: Observable<Member[]> |undefined;

  constructor(private memberservice: MembersService) {

  }
  ngOnInit(): void {
    this.members$ = this.memberservice.getMembers();
  }
}
