
import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ApiService } from '../../core/services/api.service';
@Component({
  template: `
    <h2>Sales Report</h2>
    <form [formGroup]="form" (ngSubmit)="load()" class="row g-2 mb-3">
      <div class="col-md-3"><select class="form-select" formControlName="groupBy"><option value="week">By week</option><option value="month">By month</option><option value="custom">Custom</option></select></div>
      <div class="col-md-3"><input type="date" class="form-control" formControlName="from"></div>
      <div class="col-md-3"><input type="date" class="form-control" formControlName="to"></div>
      <div class="col-md-3"><button class="btn btn-primary w-100" type="submit">Load</button></div>
    </form>
    <pre class="bg-light p-3 rounded">{{data | json}}</pre>
    <style>
h2 {
  text-align: center;
  margin: 2rem 0 1.5rem;
  font-weight: bold;
  color: #2a7a2e;
}


form {
  background: #fff;
  padding: 1rem;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  margin-bottom: 2rem;
}


.form-control,
.form-select {
  border-radius: 8px;
  padding: 0.6rem;
  border: 1px solid #ccc;
  transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

.form-control:focus,
.form-select:focus {
  border-color: #2a7a2e;
  box-shadow: 0 0 6px rgba(42, 122, 46, 0.3);
}


.btn-primary {
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


pre {
  background: #f8f9fa;
  padding: 1rem;
  border-radius: 8px;
  font-size: 0.9rem;
  max-height: 400px;
  overflow-y: auto;
  border: 1px solid #ddd;
}
</style>`
})
export class SalesReportComponent {
  data: any;
  form = this.fb.group({ groupBy: ['month'], from: [''], to: [''] });
  constructor(private fb: FormBuilder, private api: ApiService) {}
  load() { this.api.salesReport(this.form.value as any).subscribe(res => this.data = res); }
}
