"use client";

import { Button, Grid, TextField } from "@mui/material";

import { useAutoPoint } from "./useAutoPointForm";

export const AutoPointForm = ({ mode }: { mode: "check-in" | "check-out" | "idle" }) => {
  const {
    errors,
    handleSubmit,
    onSubmit,
    register
  } = useAutoPoint({ mode });

  return (
    <Grid container component="form" onSubmit={handleSubmit(onSubmit)} spacing={2} alignItems="center">
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Description"
          {...register("description")}
          error={!!errors.description}
          helperText={errors.description?.message}
        />
      </Grid>
      <Grid item xs={12} md={4}>
        <Button
          type="submit"
          variant="contained"
          color="primary"
          fullWidth
          disabled={mode === "idle"}
        >
          {mode === "check-in" ? "Check In" : "Check Out"}
        </Button>
      </Grid>
    </Grid>
  );
};
