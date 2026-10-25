export interface Student {
  id: number;
  name: string;
  email: string;
  age: number;
}

export interface CreateStudent {
  name: string;
  email: string;
  age: number;
  password: string;
}