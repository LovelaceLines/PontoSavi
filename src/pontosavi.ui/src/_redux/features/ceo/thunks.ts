import { createAsyncThunk } from "@reduxjs/toolkit";

import { Axios } from "@/_http/axios";
import { company, companyAndUser, companyFilter, queryResult, role, user, userRole } from "@/_types";

const CEO_COMPANY = "/CEO/company";
const CEO_USER = "/CEO/user";
const CEO_USER_ADD_TO_ROLE = "/CEO/user/add-to-role";
const CEO_ROLE = "/CEO/role";

export const getCompanies = createAsyncThunk(
  "ceo/company/getCompanies",
  async (filter?: companyFilter): Promise<queryResult<company>> => {
    const res = await Axios.get<queryResult<company>>(CEO_COMPANY, { params: filter });
    return res.data as queryResult<company>;
  }
);

export const getCompanyById = createAsyncThunk(
  "ceo/company/getCompanyById",
  async (id: number): Promise<company> => {
    const res = await Axios.get<company>(`${CEO_COMPANY}/${id}`);
    return res.data as company;
  }
);

export const postCompany = createAsyncThunk(
  "ceo/company/postCompany",
  async (company: companyAndUser): Promise<companyAndUser> => {
    const res = await Axios.post<companyAndUser>(CEO_COMPANY, company);
    return res.data as companyAndUser;
  }
);

export const postUser = createAsyncThunk(
  "ceo/company/postUser",
  async (user: user): Promise<user> => {
    const res = await Axios.post<user>(CEO_USER, user);
    return res.data as user;
  }
);

export const postAddUserToRole = createAsyncThunk(
  "ceo/company/postAddUserToRole",
  async (userRole: userRole): Promise<any> => {
    const res = await Axios.post<any>(CEO_USER_ADD_TO_ROLE, userRole);
    return res.data as any;
  }
);

export const postRole = createAsyncThunk(
  "ceo/company/postRole",
  async (role: role): Promise<role> => {
    const res = await Axios.post<role>(CEO_ROLE, role);
    return res.data as role;
  }
);
