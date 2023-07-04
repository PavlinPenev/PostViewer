const webApiBaseUrl = 'https://localhost:7258';

const compose =
  (...fns: any) =>
  (x: any) =>
    fns.reduceRight((v: any, f: any) => f(v), x);
const join = (separator: string) => (left: string) => (right: string) =>
  `${left}${separator}${right}`;

const joinWithSlash = join('/');
const prependWithBaseUrl = joinWithSlash(webApiBaseUrl);

//Posts Controller
const prependPostsControllerRoute = joinWithSlash('api/posts');
const prependBaseUrlAndPostsControllerRoute = compose(
    prependWithBaseUrl,
    prependPostsControllerRoute
);

export const GET_POSTS_ENDPOINT = prependBaseUrlAndPostsControllerRoute('get');
export const GET_POST_DETAILS_ENDPOINT = prependBaseUrlAndPostsControllerRoute('get');
export const DELETE_POST_ENDPOINT = prependBaseUrlAndPostsControllerRoute('delete');
export const REFRESH_DATA_ENDPOINT = prependBaseUrlAndPostsControllerRoute('refresh');

//Comments Controller
const prependCommentsControllerRoute = joinWithSlash('api/comments');
const prependBaseUrlAndCommentsControllerRoute = compose(
    prependWithBaseUrl,
    prependCommentsControllerRoute
);

export const DELETE_COMMENT_ENDPOINT = prependBaseUrlAndCommentsControllerRoute('delete');
export const ADD_COMMENT_ENDPOINT = prependBaseUrlAndCommentsControllerRoute('add');