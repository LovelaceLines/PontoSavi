"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";
import { useSelector } from "react-redux";

import { Loading } from "@/_components";
import { selectUser } from "@/_redux/features/auth/slice";

const PointsTable = dynamic(() => import("@/app/app/(admin)/points/pointsTable").then(mod => mod.PointsTable),
  { ssr: false, loading: () => <Loading /> });

export default function Page() {
  const currentUser = useSelector(selectUser);

  return (
    <>
      <Typography variant="h5" mb={2}>My Points</Typography>
      <PointsTable filters={{ userId: currentUser?.id ?? 0 }} mode="base" />
    </>
  );
}