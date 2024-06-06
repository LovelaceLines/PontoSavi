export type userFilter = {
  search?: string;

  publicId?: string;
  name?: string;
  userName?: string;
  email?: string;
  phoneNumber?: string;
  role?: string;

  pageIndex?: number;
  pageSize?: number;

  userNameOrderSort?: "asc" | "desc";
  emailOrderSort?: "asc" | "desc";
};

export type user = {
  publicId?: string;
  name: string;
  userName: string;
  email: string;
  phoneNumber: string;
  password: string;
  roles: string[];
};

export type updatedPassword = {
  oldPassword: string;
  newPassword: string;
};

export type role = {
  publicId?: string;
  name: string;
};

export class roleFilter {
  search?: string;

  publicId?: string;
  name?: string;

  pageIndex?: number;
  pageSize?: number;

  nameOrderSort?: "asc" | "desc";
}

export type userRole = {
  userId: string;
  roleName: string;
};
