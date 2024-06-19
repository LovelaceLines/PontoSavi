"use client";

import { Button, Typography } from "@mui/material";
import dynamic from "next/dynamic";
import { useEffect, useState } from "react";

import { Loading } from "@/_components";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch } from "@/_redux/store";
import { selectStatus } from "@/_redux/features/point/slice";
import { getCurrentPoint, getPointById } from "@/_redux/features/point/thunks";

const AutoPointForm = dynamic(() => import("./auto/autoPointForm").then(mod => mod.AutoPointForm),
  { ssr: false, loading: () => <Loading /> });

const ManualPointForm = dynamic(() => import("./manual/manualPointForm").then(mod => mod.ManualPointForm),
  { ssr: false, loading: () => <Loading /> });

export default function Page({ params }: { params: { id?: number } }) {
  const [isAuto, setIsAuto] = useState(true);
  const [mode, setMode] = useState<"check-in" | "check-out" | "idle">("check-in");

  const dispatch = useDispatch<AppDispatch>();
  const status = useSelector(selectStatus);

  useEffect(() => {
    if (!params.id) {
      dispatch(getCurrentPoint())
        .then(action => {
          if (getCurrentPoint.fulfilled.match(action)) {
            setMode("check-out");
          }
        });

      return;
    }

    dispatch(getPointById(params.id))
      .then(action => {
        if (getPointById.fulfilled.match(action)) {
          setMode(action.payload.checkOut ? "idle" : "check-out");
        }
      });
  }, [params.id]);

  if (status === "loading") return <Loading />;

  return (
    <>
      <Typography variant="h5" mb={2}>Point</Typography>
      {isAuto
        ? <AutoPointForm mode={mode} />
        : <ManualPointForm mode={mode} />
      }
      <br />
      <Button onClick={() => setIsAuto(!isAuto)} disabled={mode === "idle"}>
        {(isAuto ? "Manual" : "Auto") + " " + mode}
      </Button>
    </>
  );
}
