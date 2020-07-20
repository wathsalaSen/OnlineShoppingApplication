import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {TopNavigationComponent} from 'src/app/top-navigation/top-navigation.component';
import {ProductsHomeComponent} from './products-home/products-home.component';
import {SignComponent} from 'src/app/sign/sign.component';
import { RegistrationComponent } from './registration/registration.component';

const routes: Routes = [
  {path:'',component:ProductsHomeComponent,pathMatch: 'full'},
  {path:'sign', component:SignComponent},
  {path:'registration',component:RegistrationComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
