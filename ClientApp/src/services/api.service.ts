import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { Filme } from 'src/model/filme';
import { Usuario } from 'src/model/usuario';
import { Genero } from 'src/model/genero';

const apiFilmeUrl = 'http://localhost:56243/api/Filmes';
const apiGeneroUrl = 'http://localhost:56243/api/Generos'
const apiLoginUrl = 'http://localhost:56243/api/autoriza/login';
const apiRegisterUrl = 'http://localhost:56243/api/autoriza/register';
var token ='';
var httpOptions = {headers: new HttpHeaders({"Content-Type": "application/json"})};

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  montaHeaderToken() {
    token = localStorage.getItem("jwt");
    console.log('jwt header token ' + token);
    httpOptions = {headers: new HttpHeaders({"Authorization": "Bearer " + token,"Content-Type": "application/json"})};
  }

  Login (Usuario): Observable<Usuario> {
    return this.http.post<Usuario>(apiLoginUrl, Usuario).pipe(
      tap((Usuario: Usuario) => console.log(`Login usuario com email =${Usuario.email}`)),
      catchError(this.handleError<Usuario>('Login'))
    );
  }

  Register (Usuario): Observable<Usuario> {
    return this.http.post<Usuario>(apiRegisterUrl, Usuario).pipe(
      tap((Usuario: Usuario) => console.log(`Registrando usuario com email =${Usuario.email}`)),
      catchError(this.handleError<Usuario>('Register'))
    );
  }

  getFilmes (): Observable<Filme[]> {
    this.montaHeaderToken();
    console.log(httpOptions.headers);
    return this.http.get<Filme[]>(apiFilmeUrl, httpOptions)
      .pipe(
        tap(Filmes => console.log('leu os Filmes')),
        catchError(this.handleError('getFilmes', []))
      );
  }

  getGeneros (): Observable<Genero[]> {
    this.montaHeaderToken();
    console.log(httpOptions.headers);
    return this.http.get<Genero[]>(apiGeneroUrl, httpOptions)
      .pipe(
        tap(Generos => console.log('leu os Generos')),
        catchError(this.handleError('getGeneros', []))
      );
  }

  getFilme(id: number): Observable<Filme> {
    const url = `${apiFilmeUrl}/${id}`;
    return this.http.get<Filme>(url, httpOptions).pipe(
      tap(_ => console.log(`leu a Filme id=${id}`)),
      catchError(this.handleError<Filme>(`getFilme id=${id}`))
    );
  }

  getGenero(id: number): Observable<Genero> {
    const url = `${apiGeneroUrl}/${id}`;
    return this.http.get<Genero>(url, httpOptions).pipe(
      tap(_ => console.log(`leu o Genero id=${id}`)),
      catchError(this.handleError<Genero>(`getGenero id=${id}`))
    );
  }

  addFilme (Filme): Observable<Filme> {
    this.montaHeaderToken();
    return this.http.post<Filme>(apiFilmeUrl, Filme, httpOptions).pipe(
      tap((Filme: Filme) => console.log(`adicionou a Filme com w/ id=${Filme.filmeId}`)),
      catchError(this.handleError<Filme>('addFilme'))
    );
  }

  updateFilme(id, Filme): Observable<any> {
    const url = `${apiFilmeUrl}/${id}`;
    return this.http.put(url, Filme, httpOptions).pipe(
      tap(_ => console.log(`atualiza o produco com id=${id}`)),
      catchError(this.handleError<any>('updateFilme'))
    );
  }

  deleteFilme (id): Observable<Filme> {
    const url = `${apiFilmeUrl}/${id}`;
    return this.http.delete<Filme>(url, httpOptions).pipe(
      tap(_ => console.log(`remove o Filme com id=${id}`)),
      catchError(this.handleError<Filme>('deleteFilme'))
    );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}