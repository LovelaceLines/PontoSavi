"use client";

import { Button, Grid, TextField } from "@mui/material";

import { role } from "@/_types";
import { useRoleForm } from "./useRoleForm";

export const RoleForm = ({ role }: { role?: role }) => {
  const { errors, handleSubmit, onSubmit, register } = useRoleForm({ role });

  return (
    <Grid container component="form" onSubmit={handleSubmit(onSubmit)} spacing={2} alignItems="center">
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Name"
          {...register("name")}
          error={!!errors.name}
          helperText={errors.name?.message}
        />
      </Grid>
      <Grid item xs={12} md={2}>
        <Button
          type="submit"
          variant="contained"
          color="primary"
          fullWidth
        >
          {role ? "Update" : "Create"}
        </Button>
      </Grid>
    </Grid>
  );
};
