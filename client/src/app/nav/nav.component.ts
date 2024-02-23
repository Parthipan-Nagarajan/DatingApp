import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { response } from 'express';
import { Observable, of } from 'rxjs';

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

  constructor(public accountService: AccountService) {
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
    this.currentUser$.subscribe({
      next: response => console.log(response)
    })
  }

  login() {
    console.log('Nav Component login Called' + this.strName);
    console.log(this.model);
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => console.log(error)
    })
    this.currentUser$.subscribe({
      next: response => console.log(response)
    })
  }

  logout() {
    this.accountService.logout();
  }
}
