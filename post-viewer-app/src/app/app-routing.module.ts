import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ErrorPageComponent } from './error-page/error-page.component';

const routes: Routes = [
  { path: 'dashboard', redirectTo: '/', pathMatch: 'full' },
  {
    path: '', 
    component: DashboardComponent, 
    loadChildren: () => 
      import('./dashboard/dashboard.module').then(m => m.DashboardModule)
  },
  { path: '**', component: ErrorPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
