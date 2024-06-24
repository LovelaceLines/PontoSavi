"use client";

import { Button, Grid, TextField } from "@mui/material";

import { usePointForm } from "./usePointForm";
import { point } from "@/_types";

export const PointForm = ({ point }: { point: point }) => {
  const {
    errors,
    handleSubmit,
    onSubmit,
    register
  } = usePointForm({ point });

  return (
    <Grid container component="form" onSubmit={handleSubmit(onSubmit)} spacing={2} alignItems="center">
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Check In"
          type="datetime-local"
          disabled
          {...register("checkIn")}
          error={!!errors.checkIn}
          helperText={errors.checkIn?.message}
        />
      </Grid>
      <Grid item xs={12} md={9}>
        <TextField
          fullWidth
          label="Check In Description"
          {...register("checkInDescription")}
          error={!!errors.checkInDescription}
          helperText={errors.checkInDescription?.message}
        />
      </Grid>
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Check Out"
          type="datetime-local"
          disabled
          {...register("checkOut")}
          error={!!errors.checkOut}
          helperText={errors.checkOut?.message}
        />
      </Grid>
      <Grid item xs={12} md={9}>
        <TextField
          fullWidth
          label="Check Out Description"
          {...register("checkOutDescription")}
          error={!!errors.checkOutDescription}
          helperText={errors.checkOutDescription?.message}
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
