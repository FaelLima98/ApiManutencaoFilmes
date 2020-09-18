import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { Genero } from 'src/model/genero';
import { ApiService } from 'src/services/api.service';

@Component({
  selector: 'app-filme-novo',
  templateUrl: './filme-novo.component.html',
  styleUrls: ['./filme-novo.component.scss']
})
export class FilmeNovoComponent implements OnInit {
  filmeForm: FormGroup;
  titulo: string = '';
  diretor: string = '';
  generos: Genero[] = null;
  generoId: string;
  sinopse: string = '';
  ano: string = '';

  isLoadingResults = false;

  constructor(private router: Router, private api: ApiService, private formBuilder: FormBuilder) {
  
  }

  ngOnInit() {
    this.getGeneros();

    this.filmeForm = this.formBuilder.group({
      'titulo' : ['', Validators.required],
      'diretor' : ['', Validators.required],
      'generoId': ['', Validators.required] ,
      'sinopse' : [null],
      'ano' : [null],
    });  
  }

  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }

  getGeneros(){
    this.api.getGeneros().subscribe(data => {
      this.generos = data.filter(element => {
       return element !== null;
      });
      console.log(this.generos);
    });
  }

  addFilme(form: NgForm) {
    this.isLoadingResults = true;
    this.api.addFilme(form)
      .subscribe(res => {
          //const id = res['id'];
          this.isLoadingResults = false;
          this.router.navigate(['/filmes']);
        }, (err) => {
          console.log(err);
          this.isLoadingResults = false;
        });
  }

}
