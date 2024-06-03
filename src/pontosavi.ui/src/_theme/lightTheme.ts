import { ThemeOptions, createTheme } from "@mui/material";
import { grey } from "@mui/material/colors";

import { colors } from "./colors";
import { GlobalTheme } from "./globalTheme";

export const LightTheme = createTheme(GlobalTheme, {
  palette: {
    mode: "light",
    background: {
      paper: colors.white,
      default: grey[200],
    },
    text: {
      primary: colors.black,
      secondary: grey[800],
      disabled: grey[500],
    }
  }
} as ThemeOptions);
