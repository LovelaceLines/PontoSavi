"use client";

import { Box, Button, Checkbox, FormControlLabel, Link, TextField, Typography } from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";

type Inputs = {
  username: string
  email: string
  password: string
}

const Links = () => (
  <Box display="flex" justifyContent="space-between">
    <Link href="signin" variant="body2">
      Already have an account? Sign in
    </Link>
  </Box>
);

export default function SingUp() {
  const { register, handleSubmit, } = useForm<Inputs>();

  const onSubmit: SubmitHandler<Inputs> = (data) => alert(data.username + " " + data.email + " " + data.password);

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)} width="-webkit-fill-available">
      <Typography variant="h5" align="center">
        Sign up
      </Typography>
      <TextField
        autoComplete="username"
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
      <TextField
        margin="normal"
        fullWidth
        label="Password"
        type="password"
        {...register("password", { required: true })}
      />
      <FormControlLabel
        control={<Checkbox value="allowExtraEmails" />}
        label="I want to receive notifications via email."
      />
      <Button type="submit" fullWidth variant="contained" sx={{ my: 2 }}>
        Sign Up
      </Button>
      <Links />
    </Box>
  );
}