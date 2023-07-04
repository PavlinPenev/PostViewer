import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DELETE_POST_ENDPOINT, GET_POSTS_ENDPOINT, GET_POST_DETAILS_ENDPOINT, REFRESH_DATA_ENDPOINT } from '../shared/api-endpoints';
import { Post } from '../dashboard/models/post.model';
import { PostDetails } from '../dashboard/post/models/post-details.model';

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  constructor(private http: HttpClient) { }

  get(): Observable<Post[]> {
    return this.http.get<Post[]>(GET_POSTS_ENDPOINT);
  }

  getSingle(id: number): Observable<PostDetails> {
    return this.http.get<PostDetails>(GET_POST_DETAILS_ENDPOINT + `/${id}`);
  }

  delete(postId: number): Observable<boolean> {
    return this.http.delete<boolean>(DELETE_POST_ENDPOINT + `/${postId}`,);
  }

  refresh(): Observable<boolean> {
    return this.http.get<boolean>(REFRESH_DATA_ENDPOINT);
  }
}
