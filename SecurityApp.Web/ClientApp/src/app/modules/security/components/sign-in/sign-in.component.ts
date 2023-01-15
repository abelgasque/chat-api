import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CoreService } from 'src/app/core/core.service';

import { TokenService } from 'src/app/shared/services/token.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private coreService: CoreService,
    private tokenService: TokenService,

  ) {
    this.form = this.fb.group({
      username: ['', [Validators.required, Validators.email, Validators.maxLength(250)]],
      password: ['', [Validators.required, Validators.maxLength(50)]]
    });
  }

  ngOnInit(): void {
  }

  ngSubmit() {
    this.tokenService.signIn(this.form.value).subscribe({
      next: (response: any) => {
        this.coreService.setTokenLocalStorage(response.accessToken);
        this.coreService.setCustomerLocalStorage(response.customer);
        this.router.navigate(['/']);
      },
      error: (e) => {
        console.error(e);
        this.form.reset();
      }
    });
  }
}
