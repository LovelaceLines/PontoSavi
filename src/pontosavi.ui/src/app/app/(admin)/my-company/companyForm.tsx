"use client";

import { Button, Grid, TextField } from "@mui/material";

import { company } from "@/_types";
import { useCompanyForm } from "./useCompanyForm";

export const CompanyForm = ({ company, disabled, disabledLabels }: { company: company, disabled?: boolean, disabledLabels?: boolean }) => {
  const { errors, handleSubmit, onSubmit, register } = useCompanyForm({ company });

  return (
    <Grid container component="form" onSubmit={handleSubmit(onSubmit)} spacing={2}>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Trade Name"
          type="text"
          disabled={disabled || disabledLabels}
          {...register("tradeName")}
          error={!!errors.tradeName}
          helperText={errors.tradeName?.message}
        />
      </Grid>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Name"
          type="text"
          disabled={disabled || disabledLabels}
          {...register("name")}
          error={!!errors.name}
          helperText={errors.name?.message}
        />
      </Grid>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="CNPJ"
          type="text"
          disabled={disabled || disabledLabels}
          {...register("cnpj")}
          error={!!errors.cnpj}
          helperText={errors.cnpj?.message}
        />
      </Grid>
      <Grid item xs={12} display={disabled ? "none" : "block"}>
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
