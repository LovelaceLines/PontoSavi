"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";
import { useEffect, useState } from "react";

import { Loading } from "@/_components";
import { useDispatch } from "react-redux";
import { AppDispatch } from "@/_redux/store";
import { getWorkShiftById } from "@/_redux/features/workShift/thunks";
import { workShift } from "@/_types";

const WorkShiftForm = dynamic(() => import("./workShiftForm").then(mod => mod.WorkShiftForm),
  { ssr: false, loading: () => <Loading /> });

export default function Page({ params }: { params: { id: number } }) {
  const dispatch = useDispatch<AppDispatch>();
  const [workShift, setWorkShift] = useState<workShift | undefined>(undefined);

  useEffect(() => {
    dispatch(getWorkShiftById(params.id))
      .then(action => {
        if (getWorkShiftById.fulfilled.match(action)) {
          setWorkShift(action.payload);
        }
      });
  }, [params.id]);

  return (
    <>
      <Typography variant="h5" mb={2}>Work Shift</Typography>
      <WorkShiftForm workShift={workShift} />
    </>
  );
}
