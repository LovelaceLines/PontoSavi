import { createAsyncThunk } from "@reduxjs/toolkit";

import { Axios } from "@/_http/axios";
import { authToken, login, user, userToken } from "@/_types";

const LOGIN = "/Auth/login";
const REFRESH_TOKEN = "/Auth/refresh-token";
const CURRENT_USER = "/Auth/user";

export const loginUser = createAsyncThunk(
  "auth/loginUser",
  async (loginData: login): Promise<userToken> => {
    const res = await Axios.post<userToken>(LOGIN, loginData);
    return res.data as userToken;
  }
);

export const refreshToken = createAsyncThunk(
  "auth/refreshToken",
  async (): Promise<authToken> => {
    const res = await Axios.get<authToken>(REFRESH_TOKEN);
    return res.data as authToken;
  }
);

export const currentUser = createAsyncThunk(
  "auth/currentUser",
  async (): Promise<user> => {
    const res = await Axios.get<user>(CURRENT_USER);
    return res.data as user;
  }
);