import { user } from "./user";

export type login = {
  username: string;
  password: string;
};

export type authToken = {
  accessToken: string;
  refreshToken: string;
};

export type userToken = {
  user: user;
  authToken: authToken;
};

export type jwtPayload = {
  unique_name: string;
  nameid: string;
  role: string[];
  nbf: number;
  exp: number;
  iat: number;
  iss: string;
  aud: string;
};