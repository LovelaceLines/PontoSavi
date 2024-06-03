"use client";

import { ThemeProvider } from "@mui/material";

import { DarkTheme } from "@/_theme/darkTheme";

export const DarkThemeProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  return (
    <ThemeProvider theme={DarkTheme}>
      {children}
    </ThemeProvider>
  );
};
