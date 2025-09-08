import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  template: `
    <h2>Login</h2>
    <form [formGroup]="form" (ngSubmit)="submit()" class="col-md-6">
      <div class="mb-3">
        <label class="form-label">Email</label>
        <input class="form-control" formControlName="email">
      </div>

      <div class="mb-3">
        <label class="form-label">Password</label>
        <input type="password" class="form-control" formControlName="password">
      </div>

      <button class="btn btn-primary" [disabled]="form.invalid">Login</button>
      <a class="btn btn-link" routerLink="/auth/register">Create an account</a>
    </form>
    <style>
  body {
   background-image: url("assets/background.jpg") 
              no-repeat center center fixed;
  background-size: cover;
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
}

/* Form container */
form.col-md-6 {
  background: rgba(255, 255, 255, 0.9); /* semi-transparent */
  padding: 2rem;
  margin: 2rem auto;
  border-radius: 12px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
  max-width: 400px;
  width: 100%;
}

/* Heading */
h2 {
  text-align: center;
  margin-bottom: 1.5rem;
  font-weight: bold;
  color: #2a7a2e; /* Organic green */
}

/* Labels */
.form-label {
  font-weight: 500;
  color: #333;
}

/* Inputs */
.form-control {
  border-radius: 8px;
  padding: 0.6rem 0.9rem;
  border: 1px solid #ccc;
  transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

.form-control:focus {
  border-color: #2a7a2e;
  box-shadow: 0 0 6px rgba(42, 122, 46, 0.4);
}

/* Primary button */
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


.btn-link {
  display: block;
  text-align: center;
  margin-top: 1rem;
  color: #2a7a2e;
  font-weight: 500;
  text-decoration: none;
}

.btn-link:hover {
  text-decoration: underline;
}


</style>
  `
})
export class LoginComponent {
  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]]
  });

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {}

  submit() {
    if (this.form.invalid) return;

    const { email, password } = this.form.value;

    this.auth.login(email!, password!).subscribe({
      next: () => {
        const user = this.auth.currentUser;

        if (!user) {
          alert('Login failed.');
          return;
        }

        // ✅ Redirect based on role
        switch (user.role) {
          case 'Customer':
            this.router.navigate(['/shop']);
            break;
          case 'Store':
            this.router.navigate(['/store']);
            break;
          case 'Admin':
            this.router.navigate(['/admin']);
            break;
          default:
            this.router.navigate(['/auth/login']);
        }
      },
      error: err => {
        alert(err.error?.message || 'Login failed.');
      }
    });
  }
}
