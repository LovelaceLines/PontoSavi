"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";
import { useEffect, useState } from "react";

import { Loading } from "@/_components";
import { useDispatch } from "react-redux";
import { AppDispatch } from "@/_redux/store";
import { getRoleById } from "@/_redux/features/role/thunks";
import { role } from "@/_types";

const RoleForm = dynamic(() => import("./roleForm").then(mod => mod.RoleForm),
  { ssr: false, loading: () => <Loading /> });

export default function Page({ params }: { params: { id?: number } }) {
  const dispatch = useDispatch<AppDispatch>();
  const [role, setRole] = useState<role | undefined>(undefined);

  useEffect(() => {
    if (!params.id) return;

    dispatch(getRoleById(params.id))
      .then(action => {
        if (getRoleById.fulfilled.match(action)) {
          setRole(action.payload);
        }
      });
  }, [params.id]);

  return (
    <>
      <Typography variant="h5" mb={2}>Role</Typography>
      <RoleForm role={role} />
    </>
  );
}
