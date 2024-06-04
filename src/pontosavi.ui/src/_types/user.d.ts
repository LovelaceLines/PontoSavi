export type userFilter = {
  search?: string;

  id?: string;
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
  id?: string;
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
  id?: string;
  name: string;
};

export type userRole = {
  userId: string;
  roleName: string;
};
