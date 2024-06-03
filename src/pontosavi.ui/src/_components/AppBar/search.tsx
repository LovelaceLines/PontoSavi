"use client";

import { useState } from "react";
import { Box, IconButton, TextField } from "@mui/material";
import { Search as SearchIcon } from "@mui/icons-material";

import { SubmitHandler, useForm } from "react-hook-form";

type Inputs = {
  search: string
}

export const Search = () => {
  const [open, setOpen] = useState(false);
  const { register, handleSubmit, } = useForm<Inputs>();

  const onSubmit: SubmitHandler<Inputs> = data => alert(data.search);

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)} display="flex" alignItems="center">
      <TextField
        variant="outlined"
        margin="dense"
        fullWidth
        placeholder="Search..."
        autoFocus
        disabled={!open}
        {...register("search", { required: true })}
        sx={{ display: open ? "block" : "none", "& .MuiInputBase-input": { height: 2 } }}
      />
      <IconButton type="submit" color="inherit" onClick={() => setOpen(!open)}>
        <SearchIcon />
      </IconButton>
    </Box>
  );
};