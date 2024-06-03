import { ThemeOptions, createTheme } from "@mui/material";
import { grey } from "@mui/material/colors";

import { colors } from "./colors";
import { GlobalTheme } from "./globalTheme";

export const DarkTheme = createTheme(GlobalTheme, {
  palette: {
    mode: "dark",
    background: {
      paper: colors.black,
      default: colors.eerieBlack,
    },
    text: {
      primary: colors.white,
      secondary: grey[300],
      disabled: grey[500],
    },
    action: {
      hover: "#06060F",
    },
    divider: "#16161F",
  }
} as ThemeOptions);
