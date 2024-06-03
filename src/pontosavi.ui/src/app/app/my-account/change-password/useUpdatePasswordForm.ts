"use client";

import { useForm, UseFormReturn } from "react-hook-form";
import { useDispatch } from "react-redux";

import { updatePassword } from "@/_redux/features/user/thunks";
import { AppDispatch } from "@/_redux/store";
import { updatedPassword } from "@/_types";

interface UseUpdatePasswordFormProps {
  formMethods: UseFormReturn<updatedPassword & { confirmPassword: string }>;
  onSubmit: (data: updatedPassword) => void;
}

export const UseUpdatePasswordForm = (): UseUpdatePasswordFormProps => {
  const dispatch = useDispatch<AppDispatch>();
  const formMethods = useForm<updatedPassword & { confirmPassword: string }>();

  const onSubmit = (data: updatedPassword) => {
    dispatch(updatePassword(data));
  };

  return {
    formMethods,
    onSubmit,
  };
};
