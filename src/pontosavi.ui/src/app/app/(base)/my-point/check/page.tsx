"use client";

import { Button } from "@mui/material";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";

import { CheckPointForm } from "./checkPointForm";
import { Loading } from "@/_components";
import { selectOpenPoint, selectStatus } from "@/_redux/features/point/slice";
import { getCurrentPoint } from "@/_redux/features/point/thunks";
import { AppDispatch } from "@/_redux/store";

export default function Page() {
  const [auto, setAuto] = useState(true);

  const dispatch = useDispatch<AppDispatch>();
  const openPoint = useSelector(selectOpenPoint);
  const status = useSelector(selectStatus);

  useEffect(() => {
    dispatch(getCurrentPoint());
  }, []);

  if (status === "loading") return <Loading />;

  return (
    <>
      <CheckPointForm point={openPoint ?? undefined} auto={auto} />
      <br />
      <Button onClick={() => setAuto(!auto)}>
        {(auto ? "Manual" : "Auto") + " " + (openPoint ? "Check Out" : "Check In")}
      </Button>
    </>
  );
}
