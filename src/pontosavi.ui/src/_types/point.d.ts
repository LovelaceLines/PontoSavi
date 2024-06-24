/* eslint-disable no-unused-vars */

import { user } from "./user";

export type pointFilter = {
  search?: string;

  id?: number;
  userId?: number;
  managerId?: number;
  checkIn?: Date;
  checkInStatus?: pointStatus;
  checkOut?: Date;
  checkOutStatus?: pointStatus;

  pageIndex?: number;
  pageSize?: number;

  idDescOrderSort?: boolean;
  checkInDescOrderSort?: boolean;
  checkOutDescOrderSort?: boolean;
}

export type point = {
  id?: number;
  userId?: number;
  user?: user,
  managerId?: number;
  manager?: user,
  checkIn: Date;
  checkInStatus?: pointStatus;
  checkInDescription?: string;
  checkOut?: Date;
  checkOutStatus?: pointStatus;
  checkOutDescription?: string;

  createdAt?: Date;
  updatedAt?: Date;
}

export enum pointStatus {
  AutoCheck = 0,
  ManualCheck = 1,
  Pending = 2,
  Approved = 3,
  Rejected = 4,
}