"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";

import { Loading } from "@/_components";
import Link from "next/link";

const CompanyAndUserForm = dynamic(() => import("./companyAndUserForm").then(mod => mod.CompanyAndUserForm),
  { ssr: false, loading: () => <Loading /> });

export default function Page() {
  return (
    <>
      <Typography variant="h5" mb={2}>Company And User</Typography>
      <CompanyAndUserForm />
      <Link href="add-role-company" style={{ textDecoration: "none", color: "inherit" }}>
        <Typography variant="subtitle2" color="primary" mt={2}>Add Roles?</Typography>
      </Link>
    </>
  );
}
