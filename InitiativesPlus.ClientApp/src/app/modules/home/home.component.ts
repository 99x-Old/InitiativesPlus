import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserForLogin } from "../../_models/UserForLogin";
import { UserForRegister } from "../../_models/UserForRegister";
import { AuthService } from "../../_services/auth.service";
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  loginForm: FormGroup;
  registerForm: FormGroup;
  user: UserForLogin;
  userForRegister: UserForRegister;
  loginRegisterToggle: boolean = true;

  constructor(private fb: FormBuilder, private authService: AuthService) { }

  ngOnInit(): void {
    this.createLoginForm();
    this.createRegisterForm();
  }

  createLoginForm() {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
    });
  }

  create

  login() {
    if (this.loginForm.valid) {
      this.user = Object.assign({}, this.loginForm.value);
      console.log(this.user);
      this.authService.login(this.user).subscribe(data => {
        //this.alertify.success('logged in');
        console.log(data);
      }, error => {
        //this.alertify.error(error);
      }, () => {
        //this.router.navigate(['/home']);
      });
      // this.authService.register(this.user).subscribe(() => {
      //   this.alertify.success('Registration Succesful');
      // }, error => {
      //   this.alertify.error(error);
      // }, () => {
      //   this.authService.login(this.user).subscribe(() => {
      //     this.router.navigate(['/members']);
      //   });
      // });
    }
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required],
    }, {validator: this.passwordMatchValidator});
  }
  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch': true};
  }
  register(){
    if (this.registerForm.valid) {
      this.userForRegister = Object.assign({}, this.registerForm.value);
      this.userForRegister.roleId = 1;
      console.log(this.userForRegister);

      // this.authService.register(this.user).subscribe(() => {
      //   this.alertify.success('Registration Succesful');
      // }, error => {
      //   this.alertify.error(error);
      // }, () => {
      //   this.authService.login(this.user).subscribe(() => {
      //     this.router.navigate(['/members']);
      //   });
      // });
    }
  }

  registerToggle(){
    this.loginRegisterToggle = !this.loginRegisterToggle;
  }
}
