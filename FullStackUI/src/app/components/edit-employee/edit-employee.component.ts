import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee, EmployeeUpdateDto } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

  editEmployee: Employee =
  {
    id:'',
    name:'',
    email:'',
    phone: '',
    salary: ''
  }

  updatedEmployee: EmployeeUpdateDto =
  {
      name:'',
      email:'',
      phone: '',
      salary: ''
  }
  constructor(private route: ActivatedRoute, private employeesService : EmployeesService, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params) =>{
        const id = params.get('id')
        if(id){
          this.employeesService.getEmployee(id)
          .subscribe({
            next: (employee) =>{
              this.editEmployee=employee;
            },
            error: (response) => {
              console.log(response);
            }
          })
        }
      }
    })
  }

  updateEmployee(){
    console.log(this.editEmployee);
    
    this.updatedEmployee.name = this.editEmployee.name;
    this.updatedEmployee.email = this.editEmployee.email;
    this.updatedEmployee.phone = this.editEmployee.phone;
    this.updatedEmployee.salary = this.editEmployee.salary;

    this.employeesService.updateEmployee(this.editEmployee.id, this.updatedEmployee)
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

  deleteEmployee(id: string){
    console.log(id);

    this.employeesService.deleteEmployee(id)
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
