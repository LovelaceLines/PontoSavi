import { createAsyncThunk } from "@reduxjs/toolkit";

import { Axios } from "@/_http/axios";
import { queryResult, dayOff, dayOffFilter } from "@/_types";

const DAYOFF = "/DayOff";

export const getDaysOff = createAsyncThunk(
  "dayOff/getDaysOff",
  async (filter?: dayOffFilter): Promise<queryResult<dayOff>> => {
    const res = await Axios.get<queryResult<dayOff>>(DAYOFF, { params: filter });
    return res.data as queryResult<dayOff>;
  }
);

export const getDayOffById = createAsyncThunk(
  "dayOff/getDayOffById",
  async (id: number): Promise<dayOff> => {
    const res = await Axios.get<dayOff>(`${DAYOFF}/${id}`);
    return res.data as dayOff;
  }
);

export const postDayOff = createAsyncThunk(
  "dayOff/postDayOff",
  async (dayOff: dayOff): Promise<dayOff> => {
    const res = await Axios.post<dayOff>(DAYOFF, dayOff);
    return res.data as dayOff;
  }
);

export const putDayOff = createAsyncThunk(
  "dayOff/putDayOff",
  async (dayOff: dayOff): Promise<dayOff> => {
    const res = await Axios.put<dayOff>(DAYOFF, dayOff);
    return res.data as dayOff;
  }
);

export const deleteDayOff = createAsyncThunk(
  "dayOff/deleteDayOff",
  async (id: number): Promise<void> => {
    await Axios.delete(`${DAYOFF}/${id}`);
  }
);
