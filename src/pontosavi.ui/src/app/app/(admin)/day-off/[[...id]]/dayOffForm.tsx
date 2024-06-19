"use client";

import { Button, Grid, TextField } from "@mui/material";

import { dayOff } from "@/_types";
import { useDayOffForm } from "./useDayOffForm";

export const DayOffForm = ({ dayOff }: { dayOff?: dayOff }) => {
  const { errors, handleSubmit, onSubmit, register } = useDayOffForm({ dayOff });

  return (
    <Grid container component="form" onSubmit={handleSubmit(onSubmit)} spacing={2} alignItems="center">
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Date"
          type="date"
          {...register("date")}
          error={!!errors.date}
          helperText={errors.date?.message}
        />
      </Grid>
      <Grid item xs={12} md={7}>
        <TextField
          fullWidth
          label="Description"
          {...register("description")}
          error={!!errors.description}
          helperText={errors.description?.message}
        />
      </Grid>
      <Grid item xs={12} md={2}>
        <Button
          type="submit"
          variant="contained"
          color="primary"
          fullWidth
        >
          {dayOff ? "Update" : "Create"}
        </Button>
      </Grid>
    </Grid>
  );
};
