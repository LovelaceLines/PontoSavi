"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";

import { Loading } from "@/_components";

const DaysOffTable = dynamic(() => import("./daysOffTable").then(mod => mod.DaysOffTable),
  { ssr: false, loading: () => <Loading /> });

export default function Page() {
  return (
    <>
      <Typography variant="h5" mb={2}>Days Off</Typography>
      <DaysOffTable />
    </>
  );
}
