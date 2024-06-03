"use client";

import { useContext } from "react";
import { IconButton } from "@mui/material";
import { Brightness4, Brightness5 } from "@mui/icons-material";

import { ThemeContext } from "@/_theme";

export const ToggleThemeIcon = () => {
  const { themeName, toggleTheme } = useContext(ThemeContext);

  return (
    <IconButton onClick={toggleTheme} color="inherit" sx={{ display: { xs: "none", sm: "flex" } }}>
      {themeName === "light" ? <Brightness4 /> : <Brightness5 />}
    </IconButton>
  );
};