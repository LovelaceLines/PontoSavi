"use client";

import { Button, Grid, TextField } from "@mui/material";

import { useManualPoint } from "./useManualPointForm";

export const ManualPointForm = ({ mode }: { mode: "check-in" | "check-out" | "idle" }) => {
  const {
    errors,
    handleSubmit,
    onSubmit,
    register
  } = useManualPoint({ mode });

  return (
    <Grid container component="form" onSubmit={handleSubmit(onSubmit)} spacing={2} alignItems="center">
      <Grid item xs={12} md={3} display={mode === "check-out" ? "none" : "block"}>
        <TextField
          fullWidth
          label="Check In"
          type="datetime-local"
          {...register("checkIn")}
          error={!!errors.checkIn}
          helperText={errors.checkIn?.message}
        />
      </Grid>
      <Grid item xs={12} md={9} display={mode === "check-out" ? "none" : "block"}>
        <TextField
          fullWidth
          label="Check In Description"
          {...register("checkInDescription")}
          error={!!errors.checkInDescription}
          helperText={errors.checkInDescription?.message}
        />
      </Grid>
      <Grid item xs={12} md={3} display={mode === "check-in" ? "none" : "block"}>
        <TextField
          fullWidth
          label="Check Out"
          type="datetime-local"
          {...register("checkOut")}
          error={!!errors.checkOut}
          helperText={errors.checkOut?.message}
        />
      </Grid>
      <Grid item xs={12} md={9} display={mode === "check-in" ? "none" : "block"}>
        <TextField
          fullWidth
          label="Check Out Description"
          {...register("checkOutDescription")}
          error={!!errors.checkOutDescription}
          helperText={errors.checkOutDescription?.message}
        />
      </Grid>
      <Grid item xs={12} md={12}>
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
