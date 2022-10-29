import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Employee, EmployeeCreateDto } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {

  addEmployeeRequest: EmployeeCreateDto =
  {
    name:'',
    email:'',
    phone: '',
    salary: ''
  }
  constructor(private employeesService : EmployeesService, private router: Router) { }

  ngOnInit(): void {

  }

  addEmployee(){
    console.log(this.addEmployeeRequest);
    this.employeesService.addEmployee(this.addEmployeeRequest)
    .subscribe({
      next: (employeeResponse) =>{
        console.log(employeeResponse)
        this.router.navigate(['employees']);
      },
      error: (response) => {
        console.log(response);
      }
    })
  }

}
