"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";
import Link from "next/link";
import { useSelector } from "react-redux";

import { selectUser } from "@/_redux/features/auth/slice";
import { user } from "@/_types";
import { Loading } from "@/_components";

const AccountForm = dynamic(() => import("@/_forms").then(mod => mod.AccountForm),
  { ssr: false, loading: () => <Loading /> });

export default function Page() {
  const currentUser = useSelector(selectUser);

  return (
    <>
      <Typography variant="h5" mb={2}>My Account</Typography>
      <AccountForm user={currentUser as user} />
      <Link href="my-account/update-password" style={{ textDecoration: "none", color: "inherit" }}>
        <Typography variant="subtitle2" color="primary" mt={2}>Update Password?</Typography>
      </Link>
    </>
  );
}
