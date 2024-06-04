"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/role/slice";
import { postRole, putRole } from "@/_redux/features/role/thunks";
import { AppDispatch } from "@/_redux/store";
import { formDataSchema, Schema } from "./schema";
import { role } from "@/_types";

export const useRoleForm = ({ role }: { role?: role } = {}) => {
  const Snackbar = useSnackbar();

  const dispatch = useDispatch<AppDispatch>();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const { register, handleSubmit, formState: { errors } } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
    defaultValues: role
  });

  useEffect(() => { if (status === "succeeded") Snackbar(role ? "Role updated successfully" : "Role created successfully"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = ({ id, name }: Schema) =>
    role ? dispatch(putRole({ id, name })) :
      dispatch(postRole({ name }));

  return ({
    register,
    handleSubmit,
    errors,
    onSubmit
  });
};
