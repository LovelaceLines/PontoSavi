"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";

import { Loading } from "@/_components";

const RolesTable = dynamic(() => import("./rolesTable").then(mod => mod.RolesTable),
  { ssr: false, loading: () => <Loading /> });

export default function Page() {
  return (
    <>
      <Typography variant="h5" mb={2}>Roles</Typography>
      <RolesTable />
    </>
  );
}