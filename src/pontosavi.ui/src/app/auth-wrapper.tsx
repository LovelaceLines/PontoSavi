"use client";

import { jwtDecode } from "jwt-decode";
import { useRouter } from "next/navigation";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { getUserDefaultRole } from "@/globalSettings";
import { logoutUser, selectAuthToken, selectStatus, selectUser } from "@/_redux/features/auth/slice";
import { refreshToken } from "@/_redux/features/auth/thunks";
import { AppDispatch } from "@/_redux/store";
import { Loading } from "@/_components";
import { jwtPayload } from "@/_types";
import { includes } from "@/_utils";

export const AuthWrapper = ({ children, authorizedRoles = [getUserDefaultRole()] }: Readonly<{ children: React.ReactNode, authorizedRoles?: string[] }>) => {
  const dispatch = useDispatch<AppDispatch>();
  const { push } = useRouter();
  const user = useSelector(selectUser);
  const token = useSelector(selectAuthToken);
  const status = useSelector(selectStatus);

  useEffect(() => {
    if (token?.accessToken) {
      const jwt: jwtPayload = jwtDecode(token?.accessToken || "");

      if (typeof jwt.role === "string") jwt.role = [jwt.role];

      const authorized = user?.id == jwt.nameid
        && user?.userName === jwt.unique_name
        && includes(authorizedRoles, jwt.role)
        && jwt.role.includes(getUserDefaultRole())
        && jwt.exp > Date.now() / 1000;

      if (authorized) return;
      else {
        dispatch(refreshToken());
        return;
      }
    }

    push("/signin");
    dispatch(logoutUser());
  }, [token]);

  return status === "loading" ? <Loading /> :
    status === "succeeded" || status === "idle" ? children :
      null;
};
