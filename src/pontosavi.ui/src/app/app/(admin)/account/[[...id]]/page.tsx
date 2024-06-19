"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";
import { useEffect, useState } from "react";

import { Loading } from "@/_components";
import { useDispatch } from "react-redux";
import { AppDispatch } from "@/_redux/store";
import { getUserById } from "@/_redux/features/user/thunks";
import { user } from "@/_types";

const AccountForm = dynamic(() => import("@/_forms").then(mod => mod.AccountForm),
  { ssr: false, loading: () => <Loading /> });

export default function Page({ params }: { params: { id?: number } }) {
  const dispatch = useDispatch<AppDispatch>();
  const [user, setUser] = useState<user | undefined>(undefined);

  useEffect(() => {
    if (!params.id) return;

    dispatch(getUserById(params.id))
      .then(action => {
        if (getUserById.fulfilled.match(action)) {
          setUser(action.payload);
        }
      });
  }, [params.id]);

  return (
    <>
      <Typography variant="h5" mb={2}>Account</Typography>
      <AccountForm user={user} />
    </>
  );
}
