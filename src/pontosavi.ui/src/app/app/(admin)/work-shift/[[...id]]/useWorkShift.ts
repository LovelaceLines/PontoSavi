"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { transformTimeonly } from "@/_schemas";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/workShift/slice";
import { postWorkShift, putWorkShift } from "@/_redux/features/workShift/thunks";
import { AppDispatch } from "@/_redux/store";
import { formDataSchema, Schema } from "./schema";
import { workShift } from "@/_types";

export const useWorkShiftForm = ({ workShift }: { workShift?: workShift }) => {
  const { Snackbar } = useSnackbar();

  const dispatch = useDispatch<AppDispatch>();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const { register, handleSubmit, formState: { errors }, watch } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
    defaultValues: {
      ...workShift,
      checkIn: workShift ? transformTimeonly(workShift?.checkIn) : "08:00:00",
      checkOut: workShift ? transformTimeonly(workShift?.checkOut) : "18:00:00",
    }
  });

  useEffect(() => { if (status === "succeeded") Snackbar(workShift ? "Work Shift updated successfully" : "Work Shift created successfully"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = (data: Schema) =>
    workShift ? dispatch(putWorkShift(data)) :
      dispatch(postWorkShift(data));

  return ({
    register,
    handleSubmit,
    errors,
    onSubmit,
    watch
  });
};
