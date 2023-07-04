import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ADD_COMMENT_ENDPOINT, DELETE_COMMENT_ENDPOINT } from '../shared/api-endpoints';
import { AddCommentRequest } from '../dashboard/post/models/add-comment-request.model';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  constructor(private http: HttpClient) { }

  delete(id: number): Observable<boolean> {
    return this.http.delete<boolean>(DELETE_COMMENT_ENDPOINT + `/${id}`);
  }

  add(request: AddCommentRequest): Observable<boolean> {
    return this.http.post<boolean>(ADD_COMMENT_ENDPOINT, request);
  }
}
