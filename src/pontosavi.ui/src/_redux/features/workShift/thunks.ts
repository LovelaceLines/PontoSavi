import { createAsyncThunk } from "@reduxjs/toolkit";

import { Axios } from "@/_http/axios";
import { queryResult, workShift, workShiftFilter } from "@/_types";

const WORKSHIFT = "/WorkShift";

export const getWorkShifts = createAsyncThunk(
  "workShift/getWorkShifts",
  async (filter?: workShiftFilter): Promise<queryResult<workShift>> => {
    const res = await Axios.get<queryResult<workShift>>(WORKSHIFT, { params: filter });
    return res.data as queryResult<workShift>;
  }
);

export const getWorkShiftById = createAsyncThunk(
  "workShift/getWorkShiftById",
  async (id: number): Promise<workShift> => {
    const res = await Axios.get<workShift>(`${WORKSHIFT}/${id}`);
    return res.data as workShift;
  }
);

export const postWorkShift = createAsyncThunk(
  "workShift/postWorkShift",
  async (workShift: workShift): Promise<workShift> => {
    const res = await Axios.post<workShift>(WORKSHIFT, workShift);
    return res.data as workShift;
  }
);

export const putWorkShift = createAsyncThunk(
  "workShift/putWorkShift",
  async (workShift: workShift): Promise<workShift> => {
    const res = await Axios.put<workShift>(WORKSHIFT, workShift);
    return res.data as workShift;
  }
);

export const deleteWorkShift = createAsyncThunk(
  "workShift/deleteWorkShift",
  async (id: number): Promise<workShift> => {
    const res = await Axios.delete(`${WORKSHIFT}/${id}`);
    return res.data as workShift;
  }
);
