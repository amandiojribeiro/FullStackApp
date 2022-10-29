import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Employee, EmployeeCreateDto, EmployeeUpdateDto } from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getAllEmployees(): Observable<Employee[]>
  {
    return this.http.get<Employee[]>(this.baseApiUrl +'employees');
  }
  addEmployee(addEmployeeRequest: EmployeeCreateDto): Observable<Employee>{
    return this.http.post<Employee>(this.baseApiUrl +'employees', addEmployeeRequest);
  }
  getEmployee(id: string): Observable<Employee>
  {
    return this.http.get<Employee>(this.baseApiUrl +'employees/'+ id);
  }
  updateEmployee(id: string, updateEmployeeRequest : EmployeeUpdateDto): Observable<Employee>
  {
    return this.http.put<Employee>(this.baseApiUrl +'employees/'+ id, updateEmployeeRequest);
  }
  deleteEmployee(id: string): Observable<Employee>
  {
    return this.http.delete<Employee>(this.baseApiUrl +'employees/'+ id);
  }
}
