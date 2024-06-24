"use client";

import { Typography } from "@mui/material";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { Loading } from "@/_components";
import { AccountForm } from "@/_forms";
import { AppDispatch } from "@/_redux/store";
import { selectStatus, selectUser } from "@/_redux/features/user/slice";
import { getUserById } from "@/_redux/features/user/thunks";

export default function Page({ params }: { params: { id?: number } }) {
  const dispatch = useDispatch<AppDispatch>();
  const user = useSelector(selectUser);
  const status = useSelector(selectStatus);

  useEffect(() => {
    if (!params.id) return;
    dispatch(getUserById(params.id));
  }, [params.id]);

  if (status === "loading") return <Loading height="auto" />;

  return (
    <>
      <Typography variant="h5" mb={2}>Account</Typography>
      <AccountForm user={params.id ? user : undefined} />
    </>
  );
}
