"use client";

import { Typography } from "@mui/material";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { Loading } from "@/_components";
import { DayOffForm } from "./dayOffForm";
import { selectDayOff, selectStatus } from "@/_redux/features/dayOff/slice";
import { getDayOffById } from "@/_redux/features/dayOff/thunks";
import { AppDispatch } from "@/_redux/store";

export default function Page({ params }: { params: { id?: number } }) {
  const dispatch = useDispatch<AppDispatch>();
  const dayOff = useSelector(selectDayOff);
  const status = useSelector(selectStatus);

  useEffect(() => {
    if (!params.id) return;
    dispatch(getDayOffById(params.id));
  }, [params.id]);

  if (status === "loading") return <Loading height="auto" />;

  return (
    <>
      <Typography variant="h5" mb={2}>DayOff</Typography>
      <DayOffForm dayOff={params.id ? dayOff : undefined} />
    </>
  );
}
