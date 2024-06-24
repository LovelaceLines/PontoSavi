"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/user/slice";
import { putUserPassword } from "@/_redux/features/user/thunks";
import { AppDispatch } from "@/_redux/store";
import { formDataSchema, Schema } from "./schema";

export const useUpdatePassword = () => {
  const Snackbar = useSnackbar();

  const dispatch = useDispatch<AppDispatch>();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const { register, handleSubmit, formState: { errors } } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
  });

  useEffect(() => { if (status === "succeeded") Snackbar("Password updated successfully"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = ({ newPassword, oldPassword }: Schema) =>
    dispatch(putUserPassword({ newPassword, oldPassword }));

  return ({
    register,
    handleSubmit,
    errors,
    onSubmit
  });
};
