"use client";

import { ThemeProvider } from "@mui/material";

import { LightTheme } from "@/_theme/lightTheme";

export const LightThemeProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  return (
    <ThemeProvider theme={LightTheme}>
      {children}
    </ThemeProvider>
  );
};
