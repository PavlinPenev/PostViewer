import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostComponent } from './post/post.component';
import { DashboardPageComponent } from './dashboard-page/dashboard-page.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: DashboardPageComponent
      },
      {
        path: 'post/:postId',
        component: PostComponent,
        loadChildren: () => import('./post/post.module').then(m => m.PostModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
