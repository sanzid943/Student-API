import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student, CreateStudent } from './student';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private apiUrl = 'https://localhost:7206/api/Student';

  constructor(private http: HttpClient) {}

  getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.apiUrl);
  }

  addStudent(student: CreateStudent): Observable<Student> {
    return this.http.post<Student>(this.apiUrl, student);
  }

  updateStudent(
    id: number,
    student: CreateStudent
  ): Observable<Student> {
    return this.http.put<Student>(
      `${this.apiUrl}/${id}`,
      student
    );
  }

  deleteStudent(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${id}`
    );
  }
}
