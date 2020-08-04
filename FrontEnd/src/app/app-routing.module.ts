import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {TopNavigationComponent} from 'src/app/top-navigation/top-navigation.component';
import {ProductsHomeComponent} from './products-home/products-home.component';
import {SignInComponent} from 'src/app/signIn/signIn.component';

const routes: Routes = [
  {path:'',component:ProductsHomeComponent,pathMatch: 'full'},
  {path:'sign', component:SignInComponent},

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
