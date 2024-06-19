"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/point/slice";
import { putPointManualCheckOut, postPointManualCheckIn } from "@/_redux/features/point/thunks";
import { AppDispatch } from "@/_redux/store";
import { formDataSchema, Schema } from "./schema";
import { transformDateTime } from "@/_schemas";

export const useManualPoint = ({ mode }: { mode: "check-in" | "check-out" | "idle" }) => {
  const Snackbar = useSnackbar();

  const dispatch = useDispatch<AppDispatch>();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const { register, handleSubmit, formState: { errors } } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
    defaultValues: {
      checkIn: transformDateTime(new Date().toISOString()),
      checkOut: transformDateTime(new Date().toISOString())
    }
  });

  useEffect(() => { if (status === "succeeded") Snackbar(mode === "check-in" ? "Check In successful" : "Check Out successful"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = (data: Schema) =>
    mode === "check-in"
      ? dispatch(postPointManualCheckIn({ ...data, checkIn: new Date(data.checkIn), checkOut: undefined }))
      : dispatch(putPointManualCheckOut({ ...data, checkIn: new Date(), checkOut: new Date(data.checkOut?.toString() ?? "") }));

  return ({
    register,
    handleSubmit,
    errors,
    onSubmit
  });
};
