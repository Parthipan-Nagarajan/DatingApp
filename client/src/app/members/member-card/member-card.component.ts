import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Member } from '../../_models/member';

@Component({
  selector: 'app-member-card',
  standalone: false,
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css',
  encapsulation: ViewEncapsulation.Emulated
})
export class MemberCardComponent implements OnInit {

  @Input() member: Member | undefined;

  constructor() {

  }
  ngOnInit(): void {

  }
}
