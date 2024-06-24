"use client";

import { createContext, useContext } from "react";
import { CssBaseline, GlobalStyles, ThemeProvider as ThemeProviderMUI, useMediaQuery } from "@mui/material";

import { DarkTheme } from "./darkTheme";
import { LightTheme } from "./lightTheme";
import { useLocalStorage } from "@/_hooks";
import { scrollbarStyles } from "./scrollbarStyles";

interface IThemeContextProps {
  themeName: "light" | "dark";
  toggleTheme: () => void;
  isMobile: boolean;
}

export const ThemeContext = createContext({} as IThemeContextProps);

export const ThemeProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const isLightModePreferred = useMediaQuery("(prefers-color-scheme: light)");
  const [themeName, setThemeName] = useLocalStorage("theme", isLightModePreferred ? "light" : "dark");
  const toggleTheme = () => setThemeName(themeName === "light" ? "dark" : "light");

  const theme = themeName === "light" ? LightTheme : DarkTheme;

  const isMobile = useMediaQuery(theme.breakpoints.down("sm"));

  return (
    <ThemeProviderMUI theme={theme}>
      <CssBaseline enableColorScheme />
      <GlobalStyles styles={{ ...scrollbarStyles }} />
      <ThemeContext.Provider value={{ themeName, toggleTheme, isMobile }}>
        {children}
      </ThemeContext.Provider>
    </ThemeProviderMUI>
  );
};

export const useThemeContext = () => {
  const { ...props } = useContext(ThemeContext);
  return { ...props };
};
