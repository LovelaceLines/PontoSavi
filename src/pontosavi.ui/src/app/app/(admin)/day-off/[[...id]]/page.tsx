"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";
import { useEffect, useState } from "react";

import { Loading } from "@/_components";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch } from "@/_redux/store";
import { getDayOffById } from "@/_redux/features/dayOff/thunks";
import { dayOff } from "@/_types";
import { selectStatus } from "@/_redux/features/dayOff/slice";

const DayOffForm = dynamic(() => import("./dayOffForm").then(mod => mod.DayOffForm),
  { ssr: false, loading: () => <Loading /> });

export default function Page({ params }: { params: { id?: number } }) {
  const dispatch = useDispatch<AppDispatch>();
  const status = useSelector(selectStatus);
  const [dayOff, setDayOff] = useState<dayOff | undefined>(undefined);

  useEffect(() => {
    if (!params.id) return;

    dispatch(getDayOffById(params.id))
      .then(action => {
        if (getDayOffById.fulfilled.match(action)) {
          setDayOff(action.payload);
        }
      });
  }, [params.id]);

  if (status === "loading") return <Loading />;

  return (
    <>
      <Typography variant="h5" mb={2}>DayOff</Typography>
      <DayOffForm dayOff={dayOff} />
    </>
  );
}
