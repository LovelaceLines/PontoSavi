"use client";

import { Button, Grid, TextField } from "@mui/material";

import { workShift } from "@/_types";
import { useWorkShiftForm } from "./useWorkShift";

export const WorkShiftForm = ({ workShift }: { workShift?: workShift }) => {
  const {
    errors,
    handleSubmit,
    onSubmit,
    register,
  } = useWorkShiftForm({ workShift });

  return (
    <Grid container component="form" onSubmit={handleSubmit(onSubmit)} spacing={2}>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Check-in"
          type="time"
          {...register("checkIn")}
          error={!!errors.checkIn}
          helperText={errors.checkIn?.message}
        />
      </Grid>
      <Grid item xs={12} md={2}>
        <TextField
          fullWidth
          label="Check-in tolerance"
          type="number"
          {...register("checkInToleranceMinutes")}
          error={!!errors.checkInToleranceMinutes}
          helperText={errors.checkInToleranceMinutes?.message}
        />
      </Grid>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Check-out"
          type="time"
          {...register("checkOut")}
          error={!!errors.checkOut}
          helperText={errors.checkOut?.message}
        />
      </Grid>
      <Grid item xs={12} md={2}>
        <TextField
          fullWidth
          label="Check-out tolerance"
          type="number"
          {...register("checkOutToleranceMinutes")}
          error={!!errors.checkOutToleranceMinutes}
          helperText={errors.checkOutToleranceMinutes?.message}
        />
      </Grid>
      <Grid item xs={12} md={12}>
        <TextField
          fullWidth
          label="Description"
          {...register("description")}
          error={!!errors.description}
          helperText={errors.description?.message}
        />
      </Grid>
      <Grid item xs={12}>
        <Button
          type="submit"
          variant="contained"
          color="primary"
          fullWidth
        >
          {workShift ? "Update" : "Create"}
        </Button>
      </Grid>
    </Grid>
  );
};
