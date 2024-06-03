"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";

import { Loading } from "@/_components";

const AccountsTable = dynamic(() => import("./accountsTable").then(mod => mod.AccountsTable),
  { ssr: false, loading: () => <Loading /> });

export default function Page() {
  return (
    <>
      <Typography variant="h5" mb={2}>Accounts</Typography>
      <AccountsTable />
    </>
  );
}