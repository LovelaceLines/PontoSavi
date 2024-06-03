import { ThemeOptions, createTheme } from "@mui/material";
import { grey } from "@mui/material/colors";

import { colors } from "./colors";

export const GlobalTheme = createTheme({
  palette: {
    primary: {
      main: colors.imperialRed,
      contrastText: grey[50],
    },
    secondary: {
      main: colors.yellow,
      contrastText: grey[900],
    },
  },
} as ThemeOptions);
