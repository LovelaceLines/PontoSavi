"use client";

import { Button, Grid, TextField } from "@mui/material";

import { useUpdatePassword } from "./useUpdatePasswordForm";

export const UpdatePasswordForm = () => {
  const { errors, handleSubmit, onSubmit, register } = useUpdatePassword();

  return (
    <Grid container component="form" onSubmit={handleSubmit(onSubmit)} spacing={2}>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Old Password"
          type="password"
          {...register("oldPassword")}
          error={!!errors.oldPassword}
          helperText={errors.oldPassword?.message}
        />
      </Grid>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="New Password"
          type="password"
          {...register("newPassword")}
          error={!!errors.newPassword}
          helperText={errors.newPassword?.message}
        />
      </Grid>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Confirm New Password"
          type="password"
          {...register("confirmPassword")}
          error={!!errors.confirmPassword}
          helperText={errors.confirmPassword?.message}
        />
      </Grid>
      <Grid item xs={12}>
        <Button
          type="submit"
          variant="contained"
          color="primary"
          fullWidth
        >
          Update
        </Button>
      </Grid>
    </Grid>
  );
};
