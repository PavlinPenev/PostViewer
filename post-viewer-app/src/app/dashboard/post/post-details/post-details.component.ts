import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostsService } from 'src/app/services/posts.service';
import { constants } from 'src/app/shared/constants';
import { PostDetails } from '../models/post-details.model';
import { catchError, filter, first, throwError } from 'rxjs';
import { CommentsService } from 'src/app/services/comments.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from '@angular/common';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { AddCommentRequest } from '../models/add-comment-request.model';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.scss']
})
export class PostDetailsComponent implements OnInit {
  textConstants = constants;
  postId!: number;
  post!: PostDetails;

  form = new UntypedFormGroup({
    name: new UntypedFormControl(''),
    email: new UntypedFormControl(''),
    body: new UntypedFormControl(''),
  });

  constructor(
    private postsService: PostsService,
    private commentsService: CommentsService, 
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
    private location : Location) {
      this.postId = Number(this.route.snapshot.paramMap.get('postId'));
  }

  ngOnInit(): void {
    this.getPost();
  }

  deleteComment(id: number) {
    this.commentsService
      .delete(id)
      .pipe(
        filter(x => !!x),
        first(),
        catchError((x: HttpErrorResponse) => throwError(() => 'something went wrong')))
      .subscribe(x => {
        confirm('comment deleted successfully');
        this.getPost();
      });
  }

  getPost() {
    this.postsService
    .getSingle(this.postId)
    .pipe(
      filter(x => !!x),
      first()
    ).subscribe(x => {
      this.cdr.markForCheck();
      this.post = x;
    });
  }

  onAddComment() {
    const request: AddCommentRequest = {
      ...this.form.value,
      postId: this.postId
    };

    this.commentsService
      .add(request)
      .pipe(
        filter(x => !!x),
        first(),
        catchError((x: HttpErrorResponse) => throwError(() => 'something went wrong'))
      ).subscribe(() => {
        confirm('comment added');
        this.getPost();
      });
  }

  goBack() {
    this.location.back();
  }
}
