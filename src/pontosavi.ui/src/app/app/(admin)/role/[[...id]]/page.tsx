"use client";

import { Typography } from "@mui/material";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { Loading } from "@/_components";
import { AppDispatch } from "@/_redux/store";
import { selectRole, selectStatus } from "@/_redux/features/role/slice";
import { getRoleById } from "@/_redux/features/role/thunks";
import { RoleForm } from "./roleForm";

export default function Page({ params }: { params: { id?: number } }) {
  const dispatch = useDispatch<AppDispatch>();
  const role = useSelector(selectRole);
  const status = useSelector(selectStatus);

  useEffect(() => {
    if (!params.id) return;
    dispatch(getRoleById(params.id));
  }, [params.id]);

  if (status === "loading") return <Loading height="auto" />;

  return (
    <>
      <Typography variant="h5" mb={2}>Role</Typography>
      <RoleForm role={params.id ? role : undefined} />
    </>
  );
}
