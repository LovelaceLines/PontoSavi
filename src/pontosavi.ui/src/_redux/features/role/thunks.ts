import { createAsyncThunk } from "@reduxjs/toolkit";

import { Axios } from "@/_http/axios";
import { queryResult, role, roleFilter } from "@/_types";

const ROLE = "/Role";

export const getRoles = createAsyncThunk(
  "role/getRoles",
  async (filter?: roleFilter): Promise<queryResult<role>> => {
    const res = await Axios.get<queryResult<role>>(ROLE, { params: filter });
    return res.data as queryResult<role>;
  }
);

export const getRoleByPublicId = createAsyncThunk(
  "role/getRolesByPublicId",
  async (publicId: string): Promise<role> => {
    const res = await Axios.get<role>(`${ROLE}/${publicId}`);
    return res.data as role;
  }
);

export const postRole = createAsyncThunk(
  "role/postRole",
  async (role: role): Promise<role> => {
    const res = await Axios.post<role>(ROLE, role);
    return res.data as role;
  }
);

export const putRole = createAsyncThunk(
  "role/putRole",
  async (role: role): Promise<role> => {
    const res = await Axios.put<role>(ROLE, role);
    return res.data as role;
  }
);

export const deleteRole = createAsyncThunk(
  "role/deleteRole",
  async (publicId: string): Promise<void> => {
    await Axios.delete(`${ROLE}/${publicId}`);
  }
);
