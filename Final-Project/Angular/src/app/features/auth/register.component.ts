
import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
@Component({
  template: `
    <h2>Register</h2>
    <form [formGroup]="form" (ngSubmit)="submit()" class="col-md-6">
      <div class="mb-3"><label class="form-label">Name</label><input class="form-control" formControlName="name"></div>
      <div class="mb-3"><label class="form-label">Email</label><input class="form-control" formControlName="email"></div>
      <div class="mb-3"><label class="form-label">Password</label><input type="password" class="form-control" formControlName="password"></div>
      <div class="mb-3"><label class="form-label">Role</label>
        <select class="form-select" formControlName="role"><option value="Customer">Customer</option><option value="Admin">Admin</option><option value="Store">Store</option></select>
      </div>
      <button class="btn btn-primary" [disabled]="form.invalid">Create account</button>
    </form>
    <style>
form.col-md-6 {
  background: rgba(255, 255, 255, 0.9);
  padding: 2rem;
  margin: 2rem auto;
  border-radius: 12px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
  max-width: 450px;
  width: 100%;
}


h2 {
  text-align: center;
  margin-bottom: 1.5rem;
  font-weight: bold;
  color: #2a7a2e; 
}


.form-label {
  font-weight: 500;
  color: #333;
}


.form-control,
.form-select {
  border-radius: 8px;
  padding: 0.6rem 0.9rem;
  border: 1px solid #ccc;
  transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

.form-control:focus,
.form-select:focus {
  border-color: #2a7a2e;
  box-shadow: 0 0 6px rgba(42, 122, 46, 0.4);
}


.btn-primary {
  width: 100%;
  border-radius: 8px;
  background-color: #2a7a2e;
  border: none;
  padding: 0.6rem;
  font-weight: 500;
  transition: background 0.3s ease;
}

.btn-primary:hover {
  background-color: #1e5721;
}
</style>`
})
export class RegisterComponent {
  form = this.fb.group({ name: ['', Validators.required], email: ['', [Validators.required, Validators.email]], password: ['', Validators.required], role: ['Customer', Validators.required] });
  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {}
  submit() { if (this.form.invalid) return; this.auth.register(this.form.value as any).subscribe(() => this.router.navigateByUrl('/auth/login')); }
}
