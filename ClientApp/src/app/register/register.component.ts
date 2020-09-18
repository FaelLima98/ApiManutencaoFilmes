import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Usuario } from 'src/model/usuario';
import { ApiService } from 'src/services/api.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  email: String = '';
  password: String = '';
  confirmPassword: String = '';
  dataSource: Usuario;

  isLoadingResults = false;
  constructor(private router: Router, private api: ApiService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      'email' : [null, Validators.required],
      'password': [null, Validators.required],
      'confirmPassword': [null, Validators.required]
    }, {validators: this.passwordConfirming})
  }

  addRegister(form: NgForm){
    this.isLoadingResults = true;
    this.api.Register(form).subscribe(res => {
      this.dataSource = res;
      localStorage.setItem("jwt", this.dataSource.token);
      this.router.navigate(['/filmes']);
    }, (err) => {
      console.log(err);
      this.isLoadingResults = false
    })
  }

  passwordConfirming(c: AbstractControl): { invalid: boolean } {
    if (c.get('password').value !== c.get('confirmPassword').value) {
        return {invalid: true};
    }
  }

}
