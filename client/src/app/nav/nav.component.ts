import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { response } from 'express';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-nav',
  standalone: false,
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit, OnChanges, OnDestroy {

  model: any = {};
  currentUser$: Observable<User | null> = of(null);

  strName: string = "";

  constructor(public accountService: AccountService,private router:Router,private toastr:ToastrService,private spinner:NgxSpinnerService) {
    console.log("Nav Component Constructor called " + this.strName);
    this.strName = " Assigned In Constructor";
  }
  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
  }
  ngOnDestroy(): void {
    console.log('Nav Component Destroy Called');
  }

  ngOnInit(): void {
    console.log('Nav Component init Called' + this.strName);
    this.strName = " Assigned In On Init"
    this.currentUser$ = this.accountService.currentUser$;
  }

  login() {
    console.log('Nav Component login Called' + this.strName);
    console.log(this.model);
    this.spinner.show();
    this.accountService.login(this.model).subscribe({
      next: response => {       
        this.currentUser$ = this.accountService.currentUser$;
        this.router.navigateByUrl('/members');
      }
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }
}
