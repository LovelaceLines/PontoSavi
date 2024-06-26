export type userFilter = {
  search?: string;

  id?: int;
  name?: string;
  userName?: string;
  email?: string;
  phoneNumber?: string;
  role?: string;

  pageIndex?: number;
  pageSize?: number;

  idDescOrderSort?: boolean;
  nameDescOrderSort?: boolean;
  userNameDescOrderSort?: boolean;
  emailDescOrderSort?: boolean;
};

export type user = {
  id?: int;
  name: string;
  userName: string;
  email: string;
  phoneNumber: string;
  password?: string;
  roles?: role[];
  createdAt?: Date;
  updatedAt?: Date;
  tenantId?: int;
};

export type updatedPassword = {
  oldPassword: string;
  newPassword: string;
};

export type role = {
  id: int;
  name: string;
  createdAt?: Date;
  updatedAt?: Date;
  tenantId?: int;
};

export class roleFilter {
  search?: string;

  id?: int;
  name?: string;

  pageIndex?: number;
  pageSize?: number;

  nameDescOrderSort?: boolean;
}

export type userRole = {
  userId: int;
  roleId: int;
};

export type userWorkShift = {
  userId: int;
  workShiftId: int;
}
