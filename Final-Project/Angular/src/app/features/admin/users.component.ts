import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../core/services/api.service';

@Component({
  template: `
    <style>
      h2 {
        text-align: center;
        margin: 1.5rem 0;
        font-weight: bold;
        color: #2a7a2e;
      }

      .table {
        width: 100%;
        background: #fff;
        border-radius: 12px;
        overflow: hidden;
        box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        margin-top: 1rem;
      }

      .table thead {
        background-color: #2a7a2e;
        color: #fff;
        text-align: center;
      }

      .table th, 
      .table td {
        padding: 0.9rem;
        text-align: center;
        vertical-align: middle;
        font-size: 0.95rem;
      }

      .table tbody tr:nth-child(even) {
        background-color: #f9f9f9;
      }

   
      .table tbody tr:hover {
        background-color: #eef6ee;
        transition: background 0.2s ease;
      }
    </style>

    <h2>Users</h2>
    <table class='table'>
      <thead>
        <tr>
          <th>ID</th>
          <th>Name</th>
          <th>Email</th>
          <th>Role</th>
        </tr>
      </thead>
      <tbody>
        <tr *ngFor='let u of users'>
          <td>{{u.id}}</td>
          <td>{{u.name}}</td>
          <td>{{u.email}}</td>
          <td>{{u.role}}</td>
        </tr>
      </tbody>
    </table>
  `
})
export class UsersComponent implements OnInit {
  users: any[] = [];
  constructor(private api: ApiService) {}
  ngOnInit() { 
    this.api.getUsers().subscribe((res: any) => this.users = res); 
  }
}
