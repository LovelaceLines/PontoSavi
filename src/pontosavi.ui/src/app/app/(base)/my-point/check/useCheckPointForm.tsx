"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/point/slice";
import { putPointManualCheckOut, postPointManualCheckIn, postPointAutoCheckIn, putPointAutoCheckOut } from "@/_redux/features/point/thunks";
import { AppDispatch } from "@/_redux/store";
import { formDataSchema, Schema } from "./schema";
import { transformDateTime } from "@/_schemas";
import { point } from "@/_types";

export const useCheckPointForm = ({ point, auto }: { point?: point, auto: boolean }) => {
  const { Snackbar } = useSnackbar();

  const dispatch = useDispatch<AppDispatch>();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const { register, handleSubmit, formState: { errors } } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
    defaultValues: {
      id: point?.id,
      checkIn: transformDateTime(new Date().toISOString()),
      checkOut: transformDateTime(new Date().toISOString())
    }
  });

  useEffect(() => { if (status === "succeeded") Snackbar(point ? "Check Out successful" : "Check In successful"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = (data: Schema) => {
    if (!point && auto) {
      dispatch(postPointAutoCheckIn(data.checkInDescription));
      return;
    } else if (!point) {
      dispatch(postPointManualCheckIn({ ...data, checkIn: new Date(data.checkIn), checkOut: undefined }));
      return;
    } else if (point && auto) {
      dispatch(putPointAutoCheckOut(data.checkOutDescription));
      return;
    } else if (point) {
      dispatch(putPointManualCheckOut({ ...data, checkIn: new Date(), checkOut: new Date(data.checkOut?.toString() ?? "") }));
      return;
    }
  };

  return ({
    register,
    handleSubmit,
    errors,
    onSubmit
  });
};
