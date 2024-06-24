"use client";

import { Typography } from "@mui/material";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { Loading } from "@/_components";
import { AppDispatch } from "@/_redux/store";
import { selectStatus, selectWorkShift } from "@/_redux/features/workShift/slice";
import { getWorkShiftById } from "@/_redux/features/workShift/thunks";
import { WorkShiftForm } from "./workShiftForm";

export default function Page({ params }: { params: { id?: number } }) {
  const dispatch = useDispatch<AppDispatch>();
  const workShift = useSelector(selectWorkShift);
  const status = useSelector(selectStatus);

  useEffect(() => {
    if (!params.id) return;
    dispatch(getWorkShiftById(params.id));
  }, [params.id]);

  if (status === "loading") return <Loading height="auto" />;

  return (
    <>
      <Typography variant="h5" mb={2}>Work Shift</Typography>
      <WorkShiftForm workShift={params.id ? workShift : undefined} />
    </>
  );
}
