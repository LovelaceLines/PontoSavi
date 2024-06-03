"use client";

import { useContext, useEffect, useState } from "react";
import { Avatar as AvatarMUI, IconButton } from "@mui/material";
import { useSelector } from "react-redux";

import { selectUser } from "@/_redux/features/auth/slice";
import { colors, ThemeContext } from "@/_theme";

export const Avatar = () => {
  const user = useSelector(selectUser);
  const [avatarLetter, setAvatarLetter] = useState<string | null>(null);
  const { themeName } = useContext(ThemeContext);

  useEffect(() => { if (user) setAvatarLetter(user.userName[0].toUpperCase()); }, [user]);

  return (
    <IconButton href="my-account" color="inherit" sx={{ display: { xs: "none", sm: "flex" } }}>
      <AvatarMUI sx={{ width: 24, height: 24, bgcolor: `${themeName === "light" ? colors.black : colors.white}` }}>
        {avatarLetter}
      </AvatarMUI>
    </IconButton>
  );
};
