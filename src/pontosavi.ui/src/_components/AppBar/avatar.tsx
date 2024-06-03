"use client";

import { useContext, useState } from "react";
import { Avatar as AvatarMUI, IconButton } from "@mui/material";
import { useSelector } from "react-redux";

import { selectUser } from "@/_redux/features/auth/slice";
import { colors, ThemeContext } from "@/_theme";

export const Avatar = () => {
  const [avatarLetter] = useState<string | null>(useSelector(selectUser)?.userName[0].toUpperCase() ?? null);
  const { themeName } = useContext(ThemeContext);

  return (
    <IconButton href="my-account" color="inherit" sx={{ display: { xs: "none", sm: "flex" } }}>
      <AvatarMUI sx={{ width: 24, height: 24, bgcolor: `${themeName === "light" ? colors.black : colors.white}` }}>
        {avatarLetter}
      </AvatarMUI>
    </IconButton>
  );
};
