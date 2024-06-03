import { createAsyncThunk } from "@reduxjs/toolkit";

import { Axios } from "@/_http/axios";
import { queryResult, role } from "@/_types";

const ROLE = "/Role";

export const getAllRoles = createAsyncThunk(
  "role/getAllRoles",
  async (): Promise<queryResult<role>> => {
    const res = await Axios.get<queryResult<role>>(ROLE);
    return res.data as queryResult<role>;
  }
);