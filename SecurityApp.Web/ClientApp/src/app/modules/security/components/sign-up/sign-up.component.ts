import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { CustomerService } from 'src/app/shared/services/customer.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private customerService: CustomerService
  ) {
    this.form = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(50)]],
      mail: ['', [Validators.required, Validators.email, Validators.maxLength(250)]],
      password: ['', [Validators.required, Validators.maxLength(50)]]
    });
  }

  ngOnInit(): void { }

  ngSubmit() {
    this.customerService.createLeadAsync(this.form.value).subscribe({
      next: (response: any) => {
        this.router.navigate(['/security', 'sign-in']);
      },
      error: (e) => {
        console.error(e);
        this.form.reset();
      }
    })
  }
}
