import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { FilmesComponent } from './filmes/filmes.component';
import { FilmeDetalheComponent } from './filme-detalhe/filme-detalhe.component';
import { FilmeNovoComponent } from './filme-novo/filme-novo.component';
import { FilmeEditarComponent } from './filme-editar/filme-editar.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { RegisterComponent } from './register/register.component';


const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    data: {title:'Login'}
  },
  {
    path: 'logout',
    component: LogoutComponent,
    data: {title:'Logout'}
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {title:'Registrar'}
  },
  {
    path: 'filmes',
    component: FilmesComponent,
    data: {title:'Lista de Filmes'}
  },
  {
    path: 'filme-detalhe/:id',
    component: FilmeDetalheComponent,
    data: {title:'Detalhe do Filme'}
  },
  {
    path: 'filme-novo',
    component: FilmeNovoComponent,
    data: {title:'Adicionar Filme'}
  },
  {
    path: 'filme-editar/:id',
    component: FilmeEditarComponent,
    data: {title:'Editar Filme'}
  },
  {
    path: '',
    redirectTo: '/filmes',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
