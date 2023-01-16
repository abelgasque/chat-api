import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PageRedirectComponent } from '../shared/pages/page-redirect/page-redirect.component';
import { CoreComponent } from './core.component';

const routes: Routes = [
  {
    path: '',
    component: CoreComponent,
    children: [
      { path: '', loadChildren: () => import('src/app/modules/home/home.module').then(m => m.HomeModule) },
      { path: 'security', loadChildren: () => import('src/app/modules/security/security.module').then(m => m.SecurityModule) },
      { path: 'customer', loadChildren: () => import('src/app/modules/customer/customer.module').then(m => m.CustomerModule) },

      { path: 'page-not-found', component: PageRedirectComponent },
      { path: 'page-not-authorized', component: PageRedirectComponent },
      { path: '', redirectTo: '', pathMatch: 'full' },
      { path: '**', redirectTo: 'page-not-found' },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CoreRoutingModule { }
