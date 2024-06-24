import { company, user } from "./index";

export type workShiftFilter = {
  search?: string;

  id?: number;
  checkIn?: tymeonly;
  checkInToleranceMinutes?: number;
  checkOut?: tymeonly;
  checkOutToleranceMinutes?: number;

  pageIndex?: number;
  pageSize?: number;

  idDescOrderSort?: boolean;
  checkInDescOrderSort?: boolean;
  checkOutDescOrderSort?: boolean;
}

export type workShift = {
  id?: number;
  checkIn: tymeonly;
  checkInToleranceMinutes: number;
  checkOut: tymeonly;
  checkOutToleranceMinutes: number;
  description?: string;
  user?: user;
  company?: company;
  createdAt?: Date;
  updatedAt?: Date;
}