import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, NgForm, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Genero } from 'src/model/genero';
import { ApiService } from 'src/services/api.service';

@Component({
  selector: 'app-filme-editar',
  templateUrl: './filme-editar.component.html',
  styleUrls: ['./filme-editar.component.scss']
})
export class FilmeEditarComponent implements OnInit {
  filmeId: String = '';
  filmeForm: FormGroup;
  titulo: String = '';
  diretor: String = '';
  generos: Genero[] = null;
  generoId: String = '';
  sinopse: String = '';
  ano: String = '';

  isLoadingResults = false;
  constructor(private router: Router, private route: ActivatedRoute,
    private api: ApiService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.getFilme(this.route.snapshot.params['id']);

    this.getGeneros();
    
    this.filmeForm = this.formBuilder.group({
      'filmeId' : [''],  
      'titulo' : ['', Validators.required],
      'diretor' : ['', Validators.required],
      'generoId': ['', Validators.required],
      'sinopse' : [''],
      'ano' : ['']
   });
 }

 getGeneros(){
  this.api.getGeneros().subscribe(data => {
    this.generos = data.filter(element => {
     return element !== null;
    });
    console.log(this.generos);
  });
 }

  getFilme(id) {
    this.api.getFilme(id).subscribe(data => {
      this.filmeId = data.filmeId;
      this.filmeForm.setValue({
        filmeId: data.filmeId,
        titulo: data.titulo,
        diretor : data.diretor,
        generoId: data.generoId,
        sinopse: data.sinopse,
        ano: data.ano
      });
    });
  }

  updateFilme(form: NgForm) {
    this.isLoadingResults = true;
    this.api.updateFilme(this.filmeId, form)
      .subscribe(res => {
          this.isLoadingResults = false;
          this.router.navigate(['/filme-detalhe/' + this.filmeId]);
        }, (err) => {
          console.log(err);
          this.isLoadingResults = false;
        });
  }
  
}



