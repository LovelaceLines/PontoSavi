"use client";

import { Add } from "@mui/icons-material";
import { Button, Chip, FormControl, Grid, InputLabel, ListItemIcon, MenuItem, Select, TextField } from "@mui/material";

import { getSuperUserRoles } from "@/globalSettings";
import { user } from "@/_types";
import { includes } from "@/_utils";
import { useAccountForm } from "./useAccountForm";

export const AccountForm = ({ user, disabled, disabledLabels }: { user?: user, disabled?: boolean, disabledLabels?: boolean }) => {
  const {
    allRoles,
    currentUser,
    register,
    watch,
    handleSubmit,
    errors,
    onSubmit,
    handleRoleAdd,
    handleRoleDelete,
  } = useAccountForm({ user });

  return (
    <Grid container component="form" onSubmit={handleSubmit(onSubmit)} spacing={2}>
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Name"
          disabled={disabled || disabledLabels}
          {...register("name")}
          error={!!errors.name}
          helperText={errors.name?.message}
        />
      </Grid>
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Username"
          disabled={disabled || disabledLabels}
          {...register("userName")}
          error={!!errors.userName}
          helperText={errors.userName?.message}
        />
      </Grid>
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Email"
          disabled={disabled || disabledLabels}
          {...register("email")}
          error={!!errors.email}
          helperText={errors.email?.message}
        />
      </Grid>
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Phone"
          disabled={disabled || disabledLabels}
          {...register("phoneNumber")}
          error={!!errors.phoneNumber}
          helperText={errors.phoneNumber?.message}
        />
      </Grid>

      {!disabled && !disabledLabels && !user && (
        <>
          <Grid item xs={12} md={4}>
            <TextField
              fullWidth
              label="Password"
              type="password"
              {...register("password")}
              error={!!errors.password}
              helperText={errors.password?.message}
            />
          </Grid>
          <Grid item xs={12} md={4}>
            <TextField
              fullWidth
              label="Confirm Password"
              type="password"
              {...register("confirmPassword")}
              error={!!errors.confirmPassword}
              helperText={errors.confirmPassword?.message}
            />
          </Grid>
          <Grid item xs={12} md={4} />
        </>
      )}

      <Grid item xs={12} md={4} display={disabled ? "none" : "flex"}>
        <FormControl
          disabled={!includes(currentUser?.roles?.map(r => r.name) ?? [], getSuperUserRoles())}
          sx={{ width: "100%", "& .MuiSvgIcon-root": { color: "inherit" }, "& .MuiSelect-icon": { color: "inherit" } }}
        >
          <InputLabel id="input-modulo">Add Roles</InputLabel>
          <Select>
            <MenuItem value="" disabled>Add Roles</MenuItem>
            {allRoles.map((role, index) =>
              <MenuItem key={index + role.name} value={role.name} onClick={() => handleRoleAdd(role)}>
                <ListItemIcon sx={{ color: "inherit" }}>
                  <Add fontSize="small" />
                </ListItemIcon>
                {role.name}
              </MenuItem>
            )}
          </Select>
        </FormControl>
      </Grid>

      <Grid item xs={12} md={8} alignContent="center" alignItems="center" gap={1}>
        {watch("roles")?.map(role =>
          <Chip
            key={role.name}
            label={role.name}
            color="primary"
            disabled={disabled}
            onDelete={() => handleRoleDelete(role)}
            sx={{ m: 0.5 }}
          />
        )}
      </Grid>

      <Grid item xs={12} display={disabled ? "none" : "flex"}>
        <Button
          type="submit"
          variant="contained"
          color="primary"
          fullWidth
        >
          {user ? "Update" : "Create"}
        </Button>
      </Grid>
    </Grid>
  );
};