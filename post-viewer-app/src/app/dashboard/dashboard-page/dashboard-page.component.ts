import { Component, OnInit } from '@angular/core';
import { constants } from '../../shared/constants';
import { Post } from '../models/post.model';
import { MatTableDataSource } from '@angular/material/table';
import { PostsService } from '../../services/posts.service';
import { catchError, filter, first, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit {
  textConstants = constants;
  arePostsLoading: boolean = true;
  posts: Post[] = []; 

  dataSource: MatTableDataSource<Post> = new MatTableDataSource<Post>();

  displayedColumns: string[] = [
    'favorite',
    'title',
    'author',
    'numberOfComments',
    'actions'
  ]

  constructor(private postsService: PostsService, private router: Router, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.getPosts();
  }

  checkFavoriteStatus(post: Post):string {
    const localStoragePostIds = localStorage.getItem('pIds');

    if(localStoragePostIds?.includes(post.id.toString())) {
      return 'star';
    }

    return 'star_outlined';
  }

  toggleFavoriteStatus(id: number) {
    const localStoragePostIds = localStorage.getItem('pIds');
    let stringValue = localStoragePostIds;
    const stringId = id.toString();

    if(localStoragePostIds?.includes(stringId)){
      const finalResult = stringValue?.replace(stringId, "");
      localStorage.setItem('pIds', finalResult!);
    } else {
      localStorage.setItem('pIds', localStoragePostIds + stringId)
    }
  }

  deletePost(id: number) {
    this.postsService.delete(id)
      .pipe(
        filter(x => !!x), 
        first(),
        catchError((x: HttpErrorResponse) => 
          throwError(() => 'something went wrong'))
      ).subscribe(x => { 
        confirm('Post deleted successfully!');
        this.getPosts();
      });
  }

  resetData() {
    this.postsService
      .refresh()
      .pipe(filter(x => !!x), first())
      .subscribe(x => this.getPosts());
  }

  goToPostDetails(postId: number) {
    this.router.navigate(['post', postId], {relativeTo: this.activatedRoute});
  }

  private getPosts() {
    this.postsService
      .get()
      .pipe(filter(x => !!x), first())
      .subscribe(x => {
        this.posts = x;
        this.dataSource.data = this.posts;
        this.arePostsLoading = false;
      });
  }
}
