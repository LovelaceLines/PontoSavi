"use client";

import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { Loading } from "@/_components";
import { PointForm } from "./pointForm";
import { selectPoint, selectStatus } from "@/_redux/features/point/slice";
import { getPointById } from "@/_redux/features/point/thunks";
import { AppDispatch } from "@/_redux/store";

export default function Page({ params }: { params: { id: number } }) {
  const dispatch = useDispatch<AppDispatch>();
  const point = useSelector(selectPoint);
  const status = useSelector(selectStatus);

  useEffect(() => {
    dispatch(getPointById(params.id));
  }, [params.id]);

  if (point === undefined || status === "loading") return <Loading />;

  return (
    <>
      <PointForm point={point} />
    </>
  );
}
