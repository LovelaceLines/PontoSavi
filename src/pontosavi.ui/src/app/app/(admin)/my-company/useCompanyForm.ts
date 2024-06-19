"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/company/slice";
import { putCompany } from "@/_redux/features/company/thunks";
import { AppDispatch } from "@/_redux/store";
import { formDataSchema, Schema } from "./schema";
import { company } from "@/_types";

export const useCompanyForm = ({ company }: { company?: company }) => {
  const Snackbar = useSnackbar();

  const dispatch = useDispatch<AppDispatch>();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const { register, handleSubmit, formState: { errors }, watch } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
    defaultValues: company
  });

  useEffect(() => { if (status === "succeeded") Snackbar("Company updated successfully"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = (data: Schema) =>
    dispatch(putCompany(data));

  return ({
    register,
    handleSubmit,
    errors,
    onSubmit,
    watch
  });
};
