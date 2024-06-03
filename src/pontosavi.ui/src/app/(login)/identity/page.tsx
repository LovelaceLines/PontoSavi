"use client";

import { Box, Button, Link, TextField, Typography } from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";

type Inputs = {
  username: string
  email: string
}

const Links = () => (
  <Box display="flex" justifyContent="space-between">
    <Link href="signin" variant="body2">
      Already have an account? Sign in
    </Link>
    <Link href="signup" variant="body2">
      Don't have an account? Sign Up
    </Link>
  </Box>
);

export default function Identity() {
  const { register, handleSubmit, } = useForm<Inputs>();

  const onSubmit: SubmitHandler<Inputs> = (data) => alert(data.username + " " + data.email);

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)} width="-webkit-fill-available">
      <Typography variant="h5" align="center">
        Identity
      </Typography>
      <TextField
        margin="normal"
        fullWidth
        label="Username"
        autoFocus
        {...register("username", { required: true })}
      />
      <TextField
        margin="normal"
        fullWidth
        label="Email Address"
        autoFocus
        {...register("email", { required: true })}
      />
      <Typography variant="body2" align="center" mt={1}>
        You will receive an email with instructions on how to recover your account.
      </Typography>
      <Button type="submit" fullWidth variant="contained" sx={{ my: 2 }}>
        Recover
      </Button>
      <Links />
    </Box>
  );
}