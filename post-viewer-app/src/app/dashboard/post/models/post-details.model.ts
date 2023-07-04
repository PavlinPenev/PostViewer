import { PostComment } from "./post-comment.model";

export interface PostDetails {
    id: number,
    title: string,
    body: string,
    comments: PostComment[]
}