"use client";

import { getStorageValue, setStorageValue } from "./localStorageService";
import { authToken, user } from "@/_types";

export const getAuthToken = (): authToken =>
  getStorageValue("authToken", { accessToken: "", refreshToken: "" } as authToken) as authToken;

export const setAuthToken = (authToken: authToken) =>
  setStorageValue("authToken", authToken);

export const getUser = (): user =>
  getStorageValue("user", { id: "", userName: "", email: "", phoneNumber: "", password: "", roles: [] } as user) as user;

export const setUser = (user: user) =>
  setStorageValue("user", user);