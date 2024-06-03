"use client";

import { Close } from "@mui/icons-material";
import { IconButton, Snackbar as SnackbarMUI, SnackbarOrigin, useMediaQuery, useTheme } from "@mui/material";
import React from "react";
import { createContext, useContext, useState } from "react";

interface ISnackbarContextProps {
  // eslint-disable-next-line no-unused-vars
  Snackbar: (message: string) => void;
}

export const SnackbarContext = createContext({} as ISnackbarContextProps);

export const SnackbarProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  const isMobile = useMediaQuery(useTheme().breakpoints.down("sm"));
  const mobileAnchor: SnackbarOrigin = { vertical: "top", horizontal: "right" };
  const desktopAnchor: SnackbarOrigin = { vertical: "bottom", horizontal: "right" };

  const [open, setOpen] = useState<boolean>(false);
  const [message, setMessage] = useState<string>("");

  const handleClose = () => {
    setOpen(false);
    setMessage("");
  };

  const Snackbar = (message: string) => {
    setMessage(message);
    setOpen(true);
  };

  const action = (
    <IconButton
      size="small"
      color="inherit"
      onClick={handleClose}
    >
      <Close fontSize="small" />
    </IconButton>
  );

  return (
    <SnackbarContext.Provider value={{ Snackbar }}>
      {children}
      <SnackbarMUI
        open={open}
        onClose={handleClose}
        anchorOrigin={isMobile ? mobileAnchor : desktopAnchor}
        autoHideDuration={5000}
        message={message}
        action={action}
      />
    </SnackbarContext.Provider>
  );
};

export const useSnackbar = () => {
  const { Snackbar } = useContext(SnackbarContext);
  return Snackbar;
};
