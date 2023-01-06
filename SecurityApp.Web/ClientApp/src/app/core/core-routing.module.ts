import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoreComponent } from './core.component';

const routes: Routes = [
  {
    path: '',
    component: CoreComponent,
    children: [
      { path: '', loadChildren: () => import('src/app/modules/home/home.module').then(m => m.HomeModule) },
      { path: 'security', loadChildren: () => import('src/app/modules/security/security.module').then(m => m.SecurityModule) },
      { path: 'customer', loadChildren: () => import('src/app/modules/customer/customer.module').then(m => m.CustomerModule) },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CoreRoutingModule { }
