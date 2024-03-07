import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Member } from '../_models/member';
import { Router } from '@angular/router';
import { getDate } from 'ngx-bootstrap/chronos/utils/date-getters';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})

export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();

  model: Member | undefined;

  registerForm: FormGroup = new FormGroup({});

  maxDate: Date = new Date();

  validationErrors: string[] = [];

  constructor(private accountService: AccountService, private toastr: ToastrService, private fb: FormBuilder, private router: Router) {

  }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', [Validators.required, Validators.minLength(5)]],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues("password")]]
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value == control.parent?.get(matchTo)?.value ? null : { notmatching: true };
    }
  }

  register() {
    
    const dateOnly = this.getDateOnly(this.registerForm.controls['dateOfBirth'].value);
    console.log(dateOnly);
    const model = { ...this.registerForm.value, dateOfBirth: dateOnly };
    this.accountService.register(model).subscribe({
      next: response => {
        this.router.navigateByUrl('/members');
      },
      error: error =>{
        this.validationErrors = error;
      }
    })
  }

  private getDateOnly(dob: string | undefined) {
    if (!dob) return;
    let onlyDate = new Date(dob);
    var result= new Date(onlyDate.setMinutes(onlyDate.getMinutes() - onlyDate.getTimezoneOffset())).toISOString().slice(0, 10);
    return result;
  }

  cancle() {
    console.log('Cancelled');
    this.cancelRegister.emit(false);
  }
}
