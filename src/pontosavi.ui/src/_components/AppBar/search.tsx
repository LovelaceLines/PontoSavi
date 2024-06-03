"use client";

import { useEffect, useState, useRef } from "react";
import { Box, IconButton, TextField } from "@mui/material";
import { Search as SearchIcon, SearchOff } from "@mui/icons-material";

import { SubmitHandler, useForm } from "react-hook-form";

type Inputs = {
  search: string
}

export const Search = () => {
  const [open, setOpen] = useState(false);
  const { register, handleSubmit, } = useForm<Inputs>();
  const searchRef = useRef<HTMLInputElement>(null);

  const onSubmit: SubmitHandler<Inputs> = data => alert(data.search);

  useEffect(() => {
    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey && event.key === "k") {
        event.preventDefault();
        setOpen(true);
      }
    };

    window.addEventListener("keydown", handleKeyDown);

    return () => window.removeEventListener("keydown", handleKeyDown);
  }, []);

  useEffect(() => {
    if (open) searchRef.current?.focus();
  }, [open]);

  return (
    <Box
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      display="flex"
      alignItems="center"
      width="100%"
    >
      <TextField
        variant="outlined"
        margin="dense"
        fullWidth
        placeholder="Search... (Ctrl + K)"
        autoFocus
        inputRef={searchRef}
        {...register("search", { required: true })}
        sx={{
          display: open ? "block" : "none",
          "& .MuiInputBase-input": {
            height: 2,
            width: "-webkit-fill-available"
          }
        }}
      />

      <IconButton
        type="submit"
        color="inherit"
        onClick={() => setOpen(!open)}
      >
        {open ? <SearchOff /> : <SearchIcon />}
      </IconButton>
    </Box>
  );
};