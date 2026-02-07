import {Component, OnInit} from '@angular/core';
import {Post} from '../../interfaces/post';
import {TechnicalBlogService} from '../../services/technical-blog-service';
import {DatePipe} from '@angular/common';

@Component({
  selector: 'app-technical-blog',
  imports: [
    DatePipe
  ],
  templateUrl: './technical-blog.component.html',
  styleUrl: './technical-blog.component.css',
})
export class TechnicalBlogComponent implements OnInit {

  posts: Post[] = [];

  constructor(private service:  TechnicalBlogService) { }

  ngOnInit() {
    this.service.getPosts().subscribe({
      next: data => this.posts = data,
      error: error => console.error('Failed to fetch posts.', error)
    });
  }
}
