import {Component, OnInit} from '@angular/core';
import {Post} from '../../interfaces/post';
import {TechnicalBlogService} from '../../services/technical-blog-service';
import {CommonModule, DatePipe} from '@angular/common';

@Component({
  selector: 'app-technical-blog',
  standalone: true,
  imports: [
    CommonModule,
    DatePipe
  ],
  templateUrl: './technical-blog.component.html',
  styleUrl: './technical-blog.component.css',
})
export class TechnicalBlogComponent implements OnInit {

  posts: Post[] = [];

  constructor(private service:  TechnicalBlogService) { }

  ngOnInit() {

    console.log('[TechnicalBlog] ngOnInit fired');

    this.service.getPosts().subscribe({
      next: data => {
        console.log('[TechnicalBlog] posts received:', data);
        this.posts = data;
        console.log('[TechnicalBlog] posts assigned length:', this.posts.length);

      },
      error: error => console.error('Failed to fetch posts.', error)
    });
  }
}
