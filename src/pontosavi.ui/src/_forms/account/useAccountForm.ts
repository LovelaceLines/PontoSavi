"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";

import { useSnackbar } from "@/_contexts";
import { selectUser as selectAuthUser } from "@/_redux/features/auth/slice";
import { selectRoles } from "@/_redux/features/role/slice";
import { getRoles } from "@/_redux/features/role/thunks";
import { selectError, selectStatus } from "@/_redux/features/user/slice";
import { postUser, putUser } from "@/_redux/features/user/thunks";
import { AppDispatch } from "@/_redux/store";
import { user, role } from "@/_types";
import { formDataSchema, Schema } from "./schema";

export const useAccountForm = ({ user }: { user?: user }) => {
  const dispatch = useDispatch<AppDispatch>();

  const Snackbar = useSnackbar();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const currentUser = useSelector(selectAuthUser);
  const allRoles = useSelector(selectRoles);

  const { register, handleSubmit, setValue, getValues, formState: { errors }, watch } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
    defaultValues: { ...user, roles: user?.roles || [] },
  });

  useEffect(() => { dispatch(getRoles({ nameDescOrderSort: false })); }, []);

  useEffect(() => { if (status === "succeeded") Snackbar("User saved successfully"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = ({ password, ...data }: Schema) =>
    !user ? dispatch(postUser({ password: password ?? "", ...data })) :
      dispatch(putUser({ oldUser: user, newUser: { password: password ?? "", ...data } }));

  const handleRoleAdd = (role: role) => {
    if (getValues().roles?.map(r => r.name).includes(role.name)) return;
    setValue("roles", [...getValues().roles ?? [], role]);
  };

  const handleRoleDelete = (role: role) =>
    setValue("roles", getValues().roles?.filter(r => r.name !== role.name));

  return ({
    currentUser,
    allRoles,
    register,
    watch,
    handleSubmit,
    errors,
    onSubmit,
    handleRoleAdd,
    handleRoleDelete,
  });
};
