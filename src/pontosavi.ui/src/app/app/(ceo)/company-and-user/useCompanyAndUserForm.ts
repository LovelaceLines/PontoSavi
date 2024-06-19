"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/ceo/slice";
import { postCompany } from "@/_redux/features/ceo/thunks";
import { AppDispatch } from "@/_redux/store";
import { formDataSchema, Schema } from "./schema";

export const useCompanyAndUserForm = () => {
  const Snackbar = useSnackbar();

  const dispatch = useDispatch<AppDispatch>();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const { register, handleSubmit, formState: { errors }, watch } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
  });

  useEffect(() => { if (status === "succeeded") Snackbar("Company created successfully"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = (data: Schema) =>
    dispatch(postCompany(data));

  return ({
    register,
    handleSubmit,
    errors,
    onSubmit,
    watch
  });
};
