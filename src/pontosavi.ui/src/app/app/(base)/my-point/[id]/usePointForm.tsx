"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/point/slice";
import { putPoint } from "@/_redux/features/point/thunks";
import { AppDispatch } from "@/_redux/store";
import { formDataSchema, Schema } from "./schema";
import { transformDateTime } from "@/_schemas";
import { point } from "@/_types";

export const usePointForm = ({ point }: { point: point }) => {
  const { Snackbar } = useSnackbar();

  const dispatch = useDispatch<AppDispatch>();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const { register, handleSubmit, formState: { errors }, watch } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
    defaultValues: {
      ...point,
      checkIn: transformDateTime((new Date(point.checkIn)).toISOString()),
      checkOut: point.checkOut ? transformDateTime((new Date(point.checkOut)).toISOString()) : undefined
    }
  });

  useEffect(() => { if (status === "succeeded") Snackbar("Point updated successfully"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = (data: Schema) => {
    dispatch(putPoint({
      ...data,
      checkIn: new Date(data.checkIn),
      checkOut: data.checkOut ? new Date(data.checkOut) : undefined,
    }));
  };

  return ({
    register,
    handleSubmit,
    errors,
    onSubmit,
    watch
  });
};
