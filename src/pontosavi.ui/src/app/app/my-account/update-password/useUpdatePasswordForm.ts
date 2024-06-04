"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { z } from "zod";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/user/slice";
import { updatePassword } from "@/_redux/features/user/thunks";
import { AppDispatch } from "@/_redux/store";

export const formDataSchema = z.object({
  oldPassword: z.string().min(6, { message: "Old password must be at least 6 characters" }),
  newPassword: z.string().min(6, { message: "New password must be at least 6 characters" }),
  confirmPassword: z.string().min(6, { message: "Confirm password must be at least 6 characters" })
})
  .refine(data => data.oldPassword !== data.newPassword, { path: ["newPassword"], message: "New password must be different from the old password" })
  .refine(data => data.newPassword === data.confirmPassword, { path: ["confirmPassword"], message: "Confirm password must match new password" });

export type Schema = z.infer<typeof formDataSchema>;

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
    dispatch(updatePassword({ newPassword, oldPassword }));

  return ({
    register,
    handleSubmit,
    errors,
    onSubmit
  });
};
