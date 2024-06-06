"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";

import { useSnackbar } from "@/_contexts";
import { getUserDefaultRole } from "@/globalSettings";
import { selectUser as selectAuthUser } from "@/_redux/features/auth/slice";
import { selectRoles } from "@/_redux/features/role/slice";
import { getRoles } from "@/_redux/features/role/thunks";
import { selectError, selectStatus } from "@/_redux/features/user/slice";
import { postUser, updateUser } from "@/_redux/features/user/thunks";
import { AppDispatch } from "@/_redux/store";
import { user } from "@/_types";
import { formDataSchema, Schema } from "./schema";

export const useAccountForm = ({ user }: { user?: user }) => {
  const dispatch = useDispatch<AppDispatch>();

  const Snackbar = useSnackbar();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const currentUser = useSelector(selectAuthUser);
  const allRoles = useSelector(selectRoles);

  const { register, handleSubmit, setValue, getValues, formState: { errors } } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
    defaultValues: { ...user, roles: user?.roles || [getUserDefaultRole()] },
  });

  useEffect(() => { dispatch(getRoles({ nameOrderSort: "asc" })); }, []);

  useEffect(() => { if (status === "succeeded") Snackbar("User saved successfully"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const onSubmit = ({ name, userName, email, phoneNumber, password, roles }: Schema) =>
    !user ? dispatch(postUser({ name, userName, email, phoneNumber, password, roles })) :
      dispatch(updateUser({ oldUser: user, newUser: { name, userName, email, phoneNumber, password, roles } }));

  const handleRoleAdd = (role: string) => {
    if (getValues().roles.includes(role)) return;
    setValue("roles", [...getValues().roles, role]);
  };

  const handleRoleDelete = (role: string) =>
    setValue("roles", getValues().roles.filter(r => r !== role));

  return ({
    currentUser,
    allRoles,
    getValues,
    register,
    handleSubmit,
    errors,
    onSubmit,
    handleRoleAdd,
    handleRoleDelete,
  });
};
