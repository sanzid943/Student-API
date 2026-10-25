import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StudentService } from './student.service';
import { Student, CreateStudent } from './student';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App implements OnInit {

  students: Student[] = [];

  editingId: number | null = null;

  form: CreateStudent = {
    name: '',
    email: '',
    age: 18,
    password: ''
  };

  message = '';

  constructor(private studentService: StudentService) {}

  ngOnInit(): void {
    this.loadStudents();
  }

  loadStudents(): void {
    this.studentService.getStudents().subscribe({
      next: (data) => {
        this.students = data;
      },
      error: (error) => {
        console.error(error);
        this.message =
          'Student Data could not Load. Check Backend/API URL';
      }
    });
  }

  editStudent(student: Student): void {
  this.editingId = student.id;

  this.form = {
    name: student.name,
    email: student.email,
    age: student.age,
    password: ''
  };
}

addStudent(): void {
  const request$ = this.editingId === null
    ? this.studentService.addStudent(this.form)
    : this.studentService.updateStudent(
        this.editingId,
        this.form
      );

  request$.subscribe({
    next: () => {
      this.message = this.editingId === null
        ? 'Student added!'
        : 'Student updated!';

      this.editingId = null;

      this.form = {
        name: '',
        email: '',
        age: 18,
        password: ''
      };

      this.loadStudents();
    },
    error: (error) => {
      console.error(error);
      this.message = 'Operation failed';
    }
  });
}

  deleteStudent(id: number): void {
    if (!confirm('Do you want to delete this student?')) {
      return;
    }

    this.studentService.deleteStudent(id).subscribe({
      next: () => {
        this.message = 'Student deleted!';
        this.loadStudents();
      },
      error: (error) => {
        console.error(error);
        this.message = 'Delete failed';
      }
    });
  }
}