import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthServiceService } from '../auth-service.service';
import { Emitters } from '../emitter';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formGroup : FormGroup;

  constructor(private authService: AuthServiceService ,
    private router : Router) { }

  ngOnInit(): void {
    this.initForm();

  }

  initForm()
  {
    this.formGroup = new FormGroup(
      {
        Username : new FormControl('',[Validators.required]),
        Password : new FormControl('',[Validators.required])
      }
    );

  }
  get Username()
   {
     return this.formGroup.get('Username')
    };

    get Password()
   {
     return this.formGroup.get('Password')
    };


  loginProcess()
  {

    if(this.formGroup.valid)
    {
      this.authService.login(this.formGroup.value).subscribe(result=>{
        if(result.status=="Success"){
          console.log(result);
          alert(result.message);
          this.router.navigate(['home']); 
          Emitters.authEmitter.emit(true);
          
        }
      else
      {
        alert(result.message);
        Emitters.authEmitter.emit(false);
      }

       

      });
    }
  }
  
}
