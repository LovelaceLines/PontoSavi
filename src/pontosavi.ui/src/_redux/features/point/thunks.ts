import { createAsyncThunk } from "@reduxjs/toolkit";

import { Axios } from "@/_http/axios";
import { point, pointFilter, queryResult } from "@/_types";

const POINT = "/Point";
const POINT_CURRENT = "/Point/current";
const POINT_AUTO_CHECK = "/Point/auto";
const POINT_MANUAL_CHECK = "/Point/manual";
const POINT_FULL = "/Point/full";
const POINT_APPROVE = "/Point/Approve";
const POINT_REJECT = "/Point/Reject";

export const getPoints = createAsyncThunk(
  "point/getPoints",
  async (filter?: pointFilter): Promise<queryResult<point>> => {
    const res = await Axios.get<queryResult<point>>(POINT, { params: filter });
    return res.data as queryResult<point>;
  }
);

export const getPointById = createAsyncThunk(
  "point/getPointById",
  async (id: number): Promise<point> => {
    const res = await Axios.get<point>(`${POINT}/${id}`);
    return res.data as point;
  }
);

export const getCurrentPoint = createAsyncThunk(
  "point/getCurrentPoint",
  async (): Promise<point> => {
    const res = await Axios.get<point>(POINT_CURRENT);
    return res.data as point;
  }
);

export const postPointAutoCheckIn = createAsyncThunk(
  "point/postPointAutoCheckIn",
  async (description?: string): Promise<point> => {
    const res = await Axios.post<point>(POINT_AUTO_CHECK, description);
    return res.data as point;
  }
);

export const putPointAutoCheckOut = createAsyncThunk(
  "point/putPointAutoCheckOut",
  async (description?: string): Promise<point> => {
    const res = await Axios.put<point>(POINT_AUTO_CHECK, description);
    return res.data as point;
  }
);

export const postPointManualCheckIn = createAsyncThunk(
  "point/postPointManualCheckIn",
  async (point: point): Promise<point> => {
    const res = await Axios.post<point>(POINT_MANUAL_CHECK, point);
    return res.data as point;
  }
);

export const putPointManualCheckOut = createAsyncThunk(
  "point/putPointManualCheckOut",
  async (point: point): Promise<point> => {
    const res = await Axios.put<point>(POINT_MANUAL_CHECK, point);
    return res.data as point;
  }
);

export const putPoint = createAsyncThunk(
  "point/putPoint",
  async (point: point): Promise<point> => {
    const res = await Axios.put<point>(POINT, point);
    return res.data as point;
  }
);

export const putPointFull = createAsyncThunk(
  "point/putPointFull",
  async (point: point): Promise<point> => {
    const res = await Axios.put<point>(POINT_FULL, point);
    return res.data as point;
  }
);

export const postPointApprove = createAsyncThunk(
  "point/postPointApprove",
  async (id: number): Promise<point> => {
    const res = await Axios.post<point>(POINT_APPROVE, id);
    return res.data as point;
  }
);

export const postPointReject = createAsyncThunk(
  "point/postPointReject",
  async (id: number): Promise<point> => {
    const res = await Axios.post<point>(POINT_REJECT, id);
    return res.data as point;
  }
);
