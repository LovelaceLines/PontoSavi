"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";

import { Loading } from "@/_components";

const CompaniesTable = dynamic(() => import("./companiesTable").then(mod => mod.CompaniesTable),
  { ssr: false, loading: () => <Loading /> });

export default function Page() {
  return (
    <>
      <Typography variant="h5" mb={2}>Companies</Typography>
      <CompaniesTable />
    </>
  );
}