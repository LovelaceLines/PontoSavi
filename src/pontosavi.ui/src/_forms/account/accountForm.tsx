"use client";

import { Add } from "@mui/icons-material";
import { Button, Chip, FormControl, Grid, InputLabel, ListItemIcon, MenuItem, Select, TextField } from "@mui/material";

import { getSuperUserRoles } from "@/globalSettings";
import { user } from "@/_types";
import { includes } from "@/_utils";
import { useAccountForm } from "./useAccountForm";

export const AccountForm = ({ user }: { user?: user }) => {
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
          {...register("name")}
          error={!!errors.name}
          helperText={errors.name?.message}
        />
      </Grid>
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Username"
          {...register("userName")}
          error={!!errors.userName}
          helperText={errors.userName?.message}
        />
      </Grid>
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Email"
          {...register("email")}
          error={!!errors.email}
          helperText={errors.email?.message}
        />
      </Grid>
      <Grid item xs={12} md={3}>
        <TextField
          fullWidth
          label="Phone"
          {...register("phoneNumber")}
          error={!!errors.phoneNumber}
          helperText={errors.phoneNumber?.message}
        />
      </Grid>

      {!user && (
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

      <Grid item xs={12} md={4}>
        <FormControl
          disabled={!includes(currentUser ? currentUser.roles : [], getSuperUserRoles())}
          sx={{ width: "100%", "& .MuiSvgIcon-root": { color: "inherit" }, "& .MuiSelect-icon": { color: "inherit" } }}
        >
          <InputLabel id="input-modulo">Add Roles</InputLabel>
          <Select>
            <MenuItem value="" disabled>Add Roles</MenuItem>
            {allRoles.map((role, index) =>
              <MenuItem key={index + role.name} value={role.name} onClick={() => handleRoleAdd(role.name)}>
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
        {watch("roles").map(role =>
          <Chip
            key={role}
            label={role}
            color="primary"
            onDelete={() => handleRoleDelete(role)}
            sx={{ m: 0.5 }}
          />
        )}
      </Grid>

      <Grid item xs={12}>
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