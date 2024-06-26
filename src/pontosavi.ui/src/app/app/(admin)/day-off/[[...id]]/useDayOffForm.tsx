"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { transformDateonly } from "@/_schemas";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/dayOff/slice";
import { postDayOff, putDayOff } from "@/_redux/features/dayOff/thunks";
import { AppDispatch } from "@/_redux/store";
import { formDataSchema, Schema } from "./schema";
import { dayOff } from "@/_types";

export const useDayOffForm = ({ dayOff }: { dayOff?: dayOff }) => {
  const { Snackbar } = useSnackbar();

  const dispatch = useDispatch<AppDispatch>();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const { register, handleSubmit, formState: { errors }, watch } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
    defaultValues: { ...dayOff, date: transformDateonly(dayOff?.date.toString() ?? new Date().toISOString()) }
  });

  useEffect(() => { if (status === "succeeded") Snackbar(dayOff ? "DayOff updated successfully" : "DayOff created successfully"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = (data: Schema) =>
    dayOff ? dispatch(putDayOff({ ...data, date: new Date(data.date) })) :
      dispatch(postDayOff({ ...data, date: new Date(data.date) }));

  return ({
    register,
    handleSubmit,
    errors,
    onSubmit,
    watch
  });
};
