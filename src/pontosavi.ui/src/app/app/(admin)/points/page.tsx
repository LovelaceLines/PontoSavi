"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";

import { Loading } from "@/_components";

const PointsTable = dynamic(() => import("./pointsTable").then(mod => mod.PointsTable),
  { ssr: false, loading: () => <Loading /> });

export default function Page() {
  return (
    <>
      <Typography variant="h5" mb={2}>Points</Typography>
      <PointsTable />
    </>
  );
}