import { createAsyncThunk } from "@reduxjs/toolkit";

import { Axios } from "@/_http/axios";
import { company, companyFilter, queryResult } from "@/_types";

const COMPANY = "/Company";

export const getCompanies = createAsyncThunk(
  "company/getCompanies",
  async (filter?: companyFilter): Promise<queryResult<company>> => {
    const res = await Axios.get<queryResult<company>>(COMPANY, { params: filter });
    return res.data as queryResult<company>;
  }
);

export const getCompanyByPublicId = createAsyncThunk(
  "company/getCompanyByPublicId",
  async (publicId: string): Promise<company> => {
    const res = await Axios.get<company>(`${COMPANY}/${publicId}`);
    return res.data as company;
  }
);

export const postCompany = createAsyncThunk(
  "company/postCompany",
  async (company: company): Promise<company> => {
    const res = await Axios.post<company>(COMPANY, company);
    return res.data as company;
  }
);

export const putCompany = createAsyncThunk(
  "company/putCompany",
  async (company: company): Promise<company> => {
    const res = await Axios.put<company>(COMPANY, company);
    return res.data as company;
  }
);

export const deleteCompany = createAsyncThunk(
  "company/deleteCompany",
  async (publicId: string): Promise<company> => {
    const res = await Axios.delete<company>(`${COMPANY}/${publicId}`);
    return res.data as company;
  }
);
