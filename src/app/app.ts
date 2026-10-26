
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

  message = '';
  messageType: 'success' | 'error' | 'info' = 'info';

  isDarkMode = false;
  isLoading = false;

  searchText = '';
  ageFilter = 'all';
  sortBy = 'idAsc';

  form: CreateStudent = this.emptyForm();

  constructor(private studentService: StudentService) {}

  ngOnInit(): void {
    this.loadStudents();
  }

  emptyForm(): CreateStudent {
    return {
      name: '',
      email: '',
      age: 18,
      password: ''
    };
  }

  get totalStudents(): number {
    return this.students.length;
  }

  get averageAge(): number {
    if (this.students.length === 0) return 0;

    const total = this.students.reduce(
      (sum, student) => sum + Number(student.age),
      0
    );

    return Math.round(total / this.students.length);
  }

  get filteredStudents(): Student[] {
    const search = this.searchText.trim().toLowerCase();

    let result = this.students.filter(student => {
      const matchesSearch =
        student.name.toLowerCase().includes(search) ||
        student.email.toLowerCase().includes(search) ||
        String(student.id).includes(search);

      const age = Number(student.age);

      const matchesAge =
        this.ageFilter === 'all' ||
        (this.ageFilter === 'under18' && age < 18) ||
        (this.ageFilter === '18plus' && age >= 18);

      return matchesSearch && matchesAge;
    });

    result = [...result];

    switch (this.sortBy) {
      case 'nameAsc':
        result.sort((a, b) => a.name.localeCompare(b.name));
        break;
      case 'nameDesc':
        result.sort((a, b) => b.name.localeCompare(a.name));
        break;
      case 'ageAsc':
        result.sort((a, b) => a.age - b.age);
        break;
      case 'ageDesc':
        result.sort((a, b) => b.age - a.age);
        break;
      case 'idDesc':
        result.sort((a, b) => b.id - a.id);
        break;
      default:
        result.sort((a, b) => a.id - b.id);
    }

    return result;
  }

  loadStudents(): void {
    this.isLoading = true;

    this.studentService.getStudents().subscribe({
      next: data => {
        this.students = data;
        this.isLoading = false;
      },
      error: error => {
        console.error(error);
        this.isLoading = false;
        this.showMessage(
          'Failed to load student data. Please check the backend/API URL.',
          'error'
        );
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

    this.showMessage(
      'Edit student information. Please enter your password before updating.',
      'info'
    );
  }

  cancelEdit(): void {
    this.editingId = null;
    this.form = this.emptyForm();
    this.showMessage('Edit cancelled.', 'info');
  }

  addStudent(): void {
    const name = this.form.name.trim();
    const email = this.form.email.trim();
    const password = this.form.password;

    if (name.length < 2) {
      this.showMessage('Name must be at least 2 characters.', 'error');
      return;
    }

    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
      this.showMessage('Please enter a valid email address.', 'error');
      return;
    }

    const age = Number(this.form.age);

    if (!Number.isInteger(age) || age < 10 || age > 100) {
      this.showMessage('Age must be an integer between 10 and 100.', 'error');
      return;
    }

    if (password.length < 6) {
      this.showMessage('Password must be at least 6 characters long.', 'error');
      return;
    }

    const duplicateEmail = this.students.some(student =>
      student.email.toLowerCase() === email.toLowerCase() &&
      student.id !== this.editingId
    );

    if (duplicateEmail) {
      this.showMessage('A student with this email already exists.', 'error');
      return;
    }

    const requestData: CreateStudent = {
      name,
      email,
      age,
      password
    };

    const wasEditing = this.editingId !== null;
    const request$ = wasEditing
      ? this.studentService.updateStudent(this.editingId!, requestData)
      : this.studentService.addStudent(requestData);

    request$.subscribe({
      next: () => {
        this.showMessage(
          wasEditing ? 'Student updated successfully!' : 'Student added successfully!',
          'success'
        );

        this.editingId = null;
        this.form = this.emptyForm();
        this.loadStudents();
      },
      error: error => {
        console.error(error);

        const serverMessage =
          typeof error?.error === 'string'
            ? error.error
            : error?.error?.message;

        this.showMessage(
          serverMessage || 'Operation failed. Please check the backend/API.',
          'error'
        );
      }
    });
  }

  deleteStudent(id: number): void {
    if (!confirm('Do you want to delete this student?')) return;

    this.studentService.deleteStudent(id).subscribe({
      next: () => {
        this.students = this.students.filter(student => student.id !== id);
        this.showMessage('Student deleted successfully!', 'success');
      },
      error: error => {
        console.error(error);
        this.showMessage('Failed to delete student.', 'error');
      }
    });
  }

  showMessage(
    text: string,
    type: 'success' | 'error' | 'info'
  ): void {
    this.message = text;
    this.messageType = type;
  }

  clearMessage(): void {
    this.message = '';
  }

  clearFilters(): void {
    this.searchText = '';
    this.ageFilter = 'all';
    this.sortBy = 'idAsc';
  }

  refreshStudents(): void {
    this.clearFilters();
    this.loadStudents();
  }

  toggleTheme(): void {
    this.isDarkMode = !this.isDarkMode;
  }
}
