export type dayOffFilter = {
  search?: string;

  id?: number;
  date?: Date;
  description?: string;

  pageIndex?: number;
  pageSize?: number;

  idDescOrderSort?: boolean;
  dateDescOrderSort?: boolean;
}

export type dayOff = {
  id?: number;
  date: Date;
  createdAt?: Date;
  updatedAt?: Date;
  description?: string;
}
