"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/point/slice";
import { putPointAutoCheckOut, postPointAutoCheckIn } from "@/_redux/features/point/thunks";
import { AppDispatch } from "@/_redux/store";
import { formDataSchema, Schema } from "./schema";

export const useAutoPoint = ({ mode }: { mode: "check-in" | "check-out" | "idle" }) => {
  const Snackbar = useSnackbar();

  const dispatch = useDispatch<AppDispatch>();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const { register, handleSubmit, formState: { errors } } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
  });

  useEffect(() => { if (status === "succeeded") Snackbar(mode === "check-in" ? "Check In successful" : "Check Out successful"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = (data: Schema) =>
    mode === "check-in"
      ? dispatch(postPointAutoCheckIn(data.description))
      : dispatch(putPointAutoCheckOut(data.description));

  return ({
    register,
    handleSubmit,
    errors,
    onSubmit
  });
};
