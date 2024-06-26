"use client";

import { Box, Button, Checkbox, FormControlLabel, Link, TextField, Typography } from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useRouter } from "next/navigation";

import { useSnackbar } from "@/_contexts";
import { loginUser } from "@/_redux/features/auth/thunks";
import { AppDispatch } from "@/_redux/store";
import { selectError, selectStatus } from "@/_redux/features/auth/slice";
import { useEffect } from "react";
import { login } from "@/_types";

const Links = () => (
  <Box display="flex" justifyContent="space-between">
    <Link href="identity" variant="body2">
      Forgot password?
    </Link>
    <Link href="signup" variant="body2">
      Don't have an account? Sign Up
    </Link>
  </Box>
);

export default function SingIn() {
  const { push } = useRouter();
  const dispatch = useDispatch<AppDispatch>();
  const error = useSelector(selectError);
  const status = useSelector(selectStatus);
  const { Snackbar } = useSnackbar();

  const { register, handleSubmit } = useForm<login>();

  const onSubmit: SubmitHandler<login> = data =>
    dispatch(loginUser(data));

  useEffect(() => {
    if (status === "failed" && error) Snackbar(error);
    if (status === "succeeded") push("/app");
  }, [status]);

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)} width="-webkit-fill-available">
      <Typography variant="h5" align="center">
        Sign in
      </Typography>
      <TextField
        margin="normal"
        fullWidth
        label="Username"
        autoFocus
        {...register("username", { required: true })}
        autoComplete="username"
      />
      <TextField
        margin='normal'
        fullWidth
        label="Password"
        type="password"
        {...register("password", { required: true })}
        autoComplete="current-password"
      />
      <FormControlLabel
        control={<Checkbox value="remember" />}
        label="Remember me"
      />
      <Button type="submit" fullWidth variant="contained" sx={{ my: 2 }}>
        Sign In
      </Button>
      <Links />
    </Box>
  );
}