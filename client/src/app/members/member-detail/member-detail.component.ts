import { Component, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css',
  imports: [
    CommonModule, TabsModule, GalleryModule
  ]
})
export class MemberDetailComponent implements OnInit {

  member: Member | undefined;
  images: GalleryItem[] = [];

  constructor(private memberService: MembersService, private router: ActivatedRoute) {

  }
  ngOnInit(): void {
    this.getMember();
  }

  getMember() {
    var username = this.router.snapshot.paramMap.get('username');
    if (!username) return;
    this.memberService.getMember(username).subscribe({
      next: member => {
        this.member = member;
        this.getImages();
      }
    });
  }

  getImages() {
    if (!this.member) return;
    this.member.photos.forEach(photo => {
      this.images.push(new ImageItem({ src: photo.url, thumb: photo.url }));
    });
  }
}
