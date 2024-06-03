"use client";

import { Avatar, Box, IconButton, useMediaQuery, useTheme } from "@mui/material";
import { MenuOpen } from "@mui/icons-material";
import Image from "next/image";
import Link from "next/link";
import React from "react";

import { useSideBar } from "@/_contexts";

export const Header = () => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("sm"));
  const { open, toggleSideBar } = useSideBar();

  return (
    <Box
      display="flex"
      flexDirection="row"
      alignItems="center"
      gap={1} height={64} p={2}
    >
      <Link
        href="/app"
        style={{
          display: "flex",
          flexDirection: "row",
          alignItems: "center",
          gap: 16
        }}
      >
        <Image
          src="/svg/logo/v1.svg"
          alt="Logo"
          width={90} height={90}
          style={{
            height: "auto",
            display: (isMobile || open) ? "block" : "none",
          }}
        />
        <Avatar
          src="/svg/logo/icon.svg"
          variant="square"
          sx={{ width: 32, height: 32, marginLeft: -0.5 }}
        />
      </Link>
      <IconButton
        color="inherit"
        onClick={() => toggleSideBar()}
        sx={{ ml: "auto" }}
      >
        <MenuOpen />
      </IconButton>
    </Box>
  );
};