"use client";

import { Button, Grid, TextField } from "@mui/material";

import { useCheckPointForm } from "./useCheckPointForm";
import { point } from "@/_types";

export const CheckPointForm = ({ point, auto }: { point?: point, auto: boolean }) => {
  const {
    errors,
    handleSubmit,
    onSubmit,
    register
  } = useCheckPointForm({ point, auto });

  return (
    <Grid container component="form" onSubmit={handleSubmit(onSubmit)} spacing={2} alignItems="center">
      <Grid item xs={12} md={3} display={auto || point ? "none" : "block"}>
        <TextField
          fullWidth
          label="Check In"
          type="datetime-local"
          {...register("checkIn")}
          error={!!errors.checkIn}
          helperText={errors.checkIn?.message}
        />
      </Grid>
      <Grid item xs={12} md={auto ? 12 : 9} display={point ? "none" : "block"}>
        <TextField
          fullWidth
          label="Check In Description"
          {...register("checkInDescription")}
          error={!!errors.checkInDescription}
          helperText={errors.checkInDescription?.message}
        />
      </Grid>
      <Grid item xs={12} md={3} display={auto ? "none" : point ? "block" : "none"}>
        <TextField
          fullWidth
          label="Check Out"
          type="datetime-local"
          {...register("checkOut")}
          error={!!errors.checkOut}
          helperText={errors.checkOut?.message}
        />
      </Grid>
      <Grid item xs={12} md={auto ? 12 : 9} display={point ? "block" : "none"}>
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
        >
          Mark {auto ? "Auto" : "Manual"} {point ? "Check Out" : "Check In"}
        </Button>
      </Grid>
    </Grid>
  );
};
