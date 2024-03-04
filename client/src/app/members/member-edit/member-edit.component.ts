import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { Member } from '../../_models/member';
import { User } from '../../_models/user';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  standalone: false,
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit {

  member: Member | undefined;
  user: User | null = null;

  @ViewChild('editForm') editForm: NgForm | undefined;
  @HostListener('window:beforeunload', ['$event']) windowUnloadEvent($event: any) {
    if (this.editForm?.dirty) {
      return false;
    }
    else {
      return true;
    }
  }

  constructor(private accountService: AccountService, private memberService: MembersService, private toastr: ToastrService) {
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.user = user;
        console.log(this.user);
      }
    })
  }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    if (!this.user) return;
    this.memberService.getMember(this.user.username).subscribe({
      next: member => {
        this.member = member;
        console.log(this.member);
      }
    })
  }

  updateMember() {
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: _ =>{
        this.toastr.success('Profile Updated Successfully');
        this.editForm?.reset(this.member);
      }
    })   
  }
}
