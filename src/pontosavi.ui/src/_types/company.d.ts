export type companyFilter = {
  search?: string;

  publicId?: string;
  name?: string;
  tradeName?: string;
  cnpj?: string;

  pageIndex?: number;
  pageSize?: number;

  nameDescOrderSort?: boolean;
  tradeNameDescOrderSort?: boolean;
  cnpjDescOrderSort?: boolean;
}

export type company = {
  publicId?: string;
  name: string;
  tradeName: string;
  cnpj: string;
}
