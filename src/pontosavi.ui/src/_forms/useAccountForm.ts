"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { z } from "zod";

import { useSnackbar } from "@/_contexts";
import { selectUser as selectAuthUser } from "@/_redux/features/auth/slice";
import { selectRoles } from "@/_redux/features/role/slice";
import { getAllRoles } from "@/_redux/features/role/thunks";
import { selectError, selectStatus } from "@/_redux/features/user/slice";
import { postUser, updateUser } from "@/_redux/features/user/thunks";
import { AppDispatch } from "@/_redux/store";
import { user } from "@/_types";

export const formDataSchema = z.object({
  id: z.string().optional(),
  name: z.string().min(3, { message: "Name must be at least 3 characters" }),
  userName: z.string().min(3, { message: "Username must be at least 3 characters" }),
  email: z.string().email({ message: "Invalid email" }),
  phoneNumber: z.string().min(8, { message: "Phone number must be at least 8 characters" }),
  password: z.string().min(6, { message: "Password must be at least 6 characters" }),
  confirmPassword: z.string().min(6, { message: "Confirm password must be at least 6 characters" }),
  roles: z.array(z.string()),
})
  .refine(data => data.password ? data.password === data.confirmPassword : true, { path: ["confirmPassword"], message: "Confirm password must match password" });

export type Schema = z.infer<typeof formDataSchema>;

export const useAccountForm = ({ user }: { user?: user }) => {
  const dispatch = useDispatch<AppDispatch>();

  const Snackbar = useSnackbar();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);

  const currentUser = useSelector(selectAuthUser);
  const allRoles = useSelector(selectRoles);

  const { register, handleSubmit, setValue, getValues, formState: { errors } } = useForm<Schema>({
    resolver: zodResolver(formDataSchema),
    defaultValues: { ...user, roles: user?.roles || ["Colaborador"] },
  });

  useEffect(() => { dispatch(getAllRoles()); }, []);

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
