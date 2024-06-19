import { createAsyncThunk } from "@reduxjs/toolkit";

import { Axios } from "@/_http/axios";
import { company } from "@/_types";
import { companyWorkShift } from "@/_types/company";

const COMPANY = "/Company";
const COMPANY_ADD_WORK_SHIFT = "/Company/add-work-shift";
const COMPANY_REMOVE_WORK_SHIFT = "/Company/remove-work-shift";

export const getCompany = createAsyncThunk(
  "company/getCompany",
  async (): Promise<company> => {
    const res = await Axios.get<company>(COMPANY);
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

export const postAddWorkShift = createAsyncThunk(
  "company/postAddWorkShift",
  async (companyWorkShift: companyWorkShift): Promise<companyWorkShift> => {
    const res = await Axios.post<companyWorkShift>(COMPANY_ADD_WORK_SHIFT, companyWorkShift);
    return res.data as companyWorkShift;
  }
);

export const deleteRemoveWorkShift = createAsyncThunk(
  "company/deleteRemoveWorkShift",
  async (companyWorkShift: companyWorkShift): Promise<companyWorkShift> => {
    const res = await Axios.delete<companyWorkShift>(COMPANY_REMOVE_WORK_SHIFT, { data: companyWorkShift });
    return res.data as companyWorkShift;
  }
);
