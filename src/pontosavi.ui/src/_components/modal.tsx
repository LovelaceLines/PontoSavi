"use client";

import { colors, useThemeContext } from "@/_theme";
import { Close } from "@mui/icons-material";
import { Box, IconButton, Modal as ModalMUI } from "@mui/material";
import { ReactElement } from "react";

import { useModal } from "@/_contexts";

export const Modal = ({ children, initOpen, id }: Readonly<{ children: ReactElement, initOpen: boolean, id: string }>) => {
  const { handleModalClose, isOpen } = useModal();
  const { themeName } = useThemeContext();

  const open = isOpen(id) ?? initOpen;

  const handleClose = () => handleModalClose(id);

  return (
    <ModalMUI
      open={open}
      onClose={handleClose}
      closeAfterTransition
    >
      <Box
        display="flex"
        flexDirection="column"
        gap={1}
        position="absolute"
        top="50%"
        left="50%"
        bgcolor="background.paper"
        border="2px solid"
        borderColor={themeName === "light" ? colors.black : colors.white}
        boxShadow={24}
        p={4}
        sx={{
          transform: "translate(-50%, -50%)",
        }}
      >
        <IconButton onClick={handleClose} sx={{ position: "absolute", top: 0, right: 0 }}>
          <Close sx={{ color: themeName === "light" ? colors.black : colors.white }} />
        </IconButton>
        {children}
      </Box>
    </ModalMUI >
  );
};