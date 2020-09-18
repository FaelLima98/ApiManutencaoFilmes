import { Component, OnInit } from '@angular/core';
import { Filme } from 'src/model/filme';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/services/api.service';
import { Genero } from 'src/model/genero';

@Component({
  selector: 'app-filme-detalhe',
  templateUrl: './filme-detalhe.component.html',
  styleUrls: ['./filme-detalhe.component.scss']
})
export class FilmeDetalheComponent implements OnInit {
  filme: Filme = {filmeId: '', titulo:'', diretor:'', generoId:'', sinopse:'', ano:''};
  genero: Genero = {generoId:'', nome:''};
  generoId: number;
  
  isLoadingResults = true;
  constructor(private router: Router, private route: ActivatedRoute, private api: ApiService) { }

  ngOnInit() {
    this.getFilme(this.route.snapshot.params['id']);
  }

  getGenero(id){
    this.api.getGenero(+id)
      .subscribe(data => {
        this.genero = data;
        console.log(this.genero);
          this.isLoadingResults = false;  
    });
  }

  getFilme(id) {
    this.api.getFilme(id)
      .subscribe(data => {
        this.filme = data;
        this.getGenero(data.generoId);
           console.log(this.filme);
             this.isLoadingResults = false;
    });
  }
}