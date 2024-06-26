import { user } from "./user";

export type companyFilter = {
  search?: string;

  id?: int;
  name?: string;
  tradeName?: string;
  cnpj?: string;

  pageIndex?: number;
  pageSize?: number;

  companyId?: int;

  idDescOrderSort?: boolean;
  nameDescOrderSort?: boolean;
  tradeNameDescOrderSort?: boolean;
  cnpjDescOrderSort?: boolean;
}

export type company = {
  id?: int;
  name: string;
  tradeName: string;
  cnpj: string;
  createdAt?: Date;
  updatedAt?: Date;
}

export type companyAndUser = {
  company: company;
  user: user;
}

export type companyWorkShift = {
  tenantId?: int;
  workShiftId: int;
}
