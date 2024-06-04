"use client";

import { Typography } from "@mui/material";

import { AccountForm } from "@/_forms";

export default function Page() {
  return (
    <>
      <Typography variant="h5" mb={2}>Account</Typography>
      <AccountForm />
    </>
  );
}
