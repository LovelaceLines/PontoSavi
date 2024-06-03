"use client";

import { Avatar, Box, IconButton } from "@mui/material";
import { MenuOpen } from "@mui/icons-material";
import Image from "next/image";
import React from "react";
import { useDispatch } from "react-redux";

import { toggleSideBar } from "@/_redux/features/handleSideBar/slice";

export const Header = () => {
  const dispatch = useDispatch();

  return (
    <Box
      display="flex"
      flexDirection="row"
      alignItems="center"
      gap={1} height={64} p={2}
    >
      <Image
        src="/svg/logo/v1.svg"
        alt="Logo"
        width={90} height={90}
        style={{ height: "auto" }}
      />
      <Avatar
        src="/svg/logo/icon.svg"
        variant="square"
        sx={{ width: 32, height: 32 }}
      />
      <IconButton
        color="inherit"
        onClick={() => dispatch(toggleSideBar())}
        sx={{ ml: "auto" }}
      >
        <MenuOpen />
      </IconButton>
    </Box>
  );
};