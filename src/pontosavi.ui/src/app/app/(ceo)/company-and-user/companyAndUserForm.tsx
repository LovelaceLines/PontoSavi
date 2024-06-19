"use client";

import { Button, Grid, TextField, Typography } from "@mui/material";

import { useCompanyAndUserForm } from "./useCompanyAndUserForm";

export const CompanyAndUserForm = ({ disabled, disabledLabels }: { disabled?: boolean, disabledLabels?: boolean }) => {
  const { errors, handleSubmit, onSubmit, register } = useCompanyAndUserForm();

  return (
    <Grid container component="form" onSubmit={handleSubmit(onSubmit)} spacing={2}>
      <Grid item xs={12}>
        <Typography variant="h6">Company</Typography>
      </Grid>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Trade Name"
          type="text"
          disabled={disabled || disabledLabels}
          {...register("company.tradeName")}
          error={!!errors.company?.tradeName}
          helperText={errors.company?.tradeName?.message}
        />
      </Grid>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Name"
          type="text"
          disabled={disabled || disabledLabels}
          {...register("company.name")}
          error={!!errors.company?.name}
          helperText={errors.company?.name?.message}
        />
      </Grid>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="CNPJ"
          type="text"
          disabled={disabled || disabledLabels}
          {...register("company.cnpj")}
          error={!!errors.company?.cnpj}
          helperText={errors.company?.cnpj?.message}
        />
      </Grid>

      <Grid item xs={12}>
        <Typography variant="h6">User Admin</Typography>
      </Grid>

      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Name"
          disabled={disabled || disabledLabels}
          {...register("user.name")}
          error={!!errors.user?.name}
          helperText={errors.user?.name?.message}
        />
      </Grid>
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Username"
          disabled={disabled || disabledLabels}
          {...register("user.userName")}
          error={!!errors.user?.userName}
          helperText={errors.user?.userName?.message}
        />
      </Grid>
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Email"
          disabled={disabled || disabledLabels}
          {...register("user.email")}
          error={!!errors.user?.email}
          helperText={errors.user?.email?.message}
        />
      </Grid>
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Phone"
          disabled={disabled || disabledLabels}
          {...register("user.phoneNumber")}
          error={!!errors.user?.phoneNumber}
          helperText={errors.user?.phoneNumber?.message}
        />
      </Grid>

      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Password"
          type="password"
          {...register("user.password")}
          error={!!errors.user?.password}
          helperText={errors.user?.password?.message}
        />
      </Grid>
      <Grid item xs={12} md={4}>
        <TextField
          fullWidth
          label="Confirm Password"
          type="password"
          {...register("user.confirmPassword")}
          error={!!errors.user?.confirmPassword}
          helperText={errors.user?.confirmPassword?.message}
        />
      </Grid>

      <Grid item xs={12} display={disabled ? "none" : "block"}>
        <Button
          type="submit"
          variant="contained"
          color="primary"
          fullWidth
        >
          Create
        </Button>
      </Grid>
    </Grid>
  );
};
