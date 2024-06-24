import { colors } from "./colors";

export const scrollbarStyles = {
  "&::-webkit-scrollbar": {
    width: "3px",
    height: "10px",
  },
  "&::-webkit-scrollbar-thumb": {
    background: colors.jet,
  },
  "&::-webkit-scrollbar-thumb:hover": {
    background: colors.imperialRed,
  },
};
