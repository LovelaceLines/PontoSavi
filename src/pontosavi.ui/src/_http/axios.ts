"use client";

import axios, { AxiosInstance } from "axios";

import env from "@/env";
import { getAuthToken } from "@/_services";
import { apiException, badRequestException } from "@/_types";

export const Axios: AxiosInstance = axios.create({
  baseURL: env.API_URL,
  timeout: 5000,
  headers: {
    "Content-Type": "application/json",
    "Authorization": `Bearer ${getAuthToken().accessToken || ""}`,
  },
});

Axios.interceptors.request.use(
  config => {
    config.headers.Authorization = `Bearer ${getAuthToken().accessToken || ""}`;
    console.debug("API/UI Request:", config);
    return config;
  },
  error => Promise.reject(error)
);

Axios.interceptors.response.use(
  response => {
    console.debug("API/UI Response:", response);
    return response;
  },
  error => {
    if (error.response) {
      if (error.response.data as apiException) {
        console.debug("API/UI Error:", error.response.data as apiException);
        return Promise.reject(error.response.data as apiException);
      } else if (error.response.data as badRequestException) {
        console.debug("API/UI Error:", error.response.data as badRequestException);
        return Promise.reject(error.response.data as badRequestException);
      } else {
        console.debug("API/UI Error:", error.response.data);
      }

      console.debug("API/UI Status:", error.response.status);
    } else if (error.request) {
      console.debug("API/UI Request Error:", error.request);
    } else {
      console.debug("API/UI Error:", error.message);
    }

    return Promise.reject(error.message);
  }
);
