"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";

import { Loading } from "@/_components";

const WorkShiftsTable = dynamic(() => import("./workShiftsTable").then(mod => mod.WorkShiftsTable),
  { ssr: false, loading: () => <Loading /> });

export default function Page() {
  return (
    <>
      <Typography variant="h5" mb={2}>Work Shifts</Typography>
      <WorkShiftsTable />
    </>
  );
}
