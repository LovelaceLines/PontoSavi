"use client";

import { Typography } from "@mui/material";
import Link from "next/link";

import { CompanyAndUserForm } from "./companyAndUserForm";

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
