import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { Router } from '@angular/router';
import { Filme } from 'src/model/filme';
import { Genero } from 'src/model/genero';
import { ApiService } from 'src/services/api.service';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-filmes',
  templateUrl: './filmes.component.html',
  styleUrls: ['./filmes.component.scss']
})
export class FilmesComponent implements OnInit {
  displayedColumns: string[] = ['titulo', 'diretor', 'acao1', 'acao2', 'acao3'];
  dataSource: Filme[];
  cacheDataSource: Filme[];
  generos: Genero[] = null;
  isLoadingResults = true;

  constructor(private router: Router, private api: ApiService, public dialog: MatDialog) { }

  ngOnInit() {
    this.getGeneros();

    this.api.getFilmes().subscribe(res => {
      this.dataSource = res;
      this.cacheDataSource = res;
      console.log(this.dataSource);
      this.isLoadingResults = false;
    }, (err) => {
      console.log(err);
      this.isLoadingResults = false;
    })    
  }

  filterPorGenero(filterVal: any) {
    if (filterVal == "")
        this.dataSource = this.cacheDataSource;
    else
        this.dataSource = this.cacheDataSource.filter((item) => item.generoId == filterVal);
  }

  filterPorTitulo(filterVal: any) {
    if (filterVal == "")
        this.dataSource = this.cacheDataSource;
    else
        this.dataSource = this.cacheDataSource.filter((item) => item.filmeId == filterVal);
  }

  openDialog(id): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: "Deseja excluir este filme?"
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        console.log('Sim clicked');
        this.deleteFilme(id);
      }
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

  deleteFilme(id) {
    this.isLoadingResults = true;
    this.api.deleteFilme(id)
      .subscribe(res => {
          this.isLoadingResults = false;
          this.ngOnInit();
          this.router.navigate(['/filmes']);
        }, (err) => {
          console.log(err);
          this.isLoadingResults = false;
        }
      );
  }
  
}
