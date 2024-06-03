"use client";

import { colors, ThemeContext } from "@/_theme";
import { Close } from "@mui/icons-material";
import { Box, IconButton, Modal as ModalMUI } from "@mui/material";
import { ReactElement, useContext } from "react";
import { useDispatch, useSelector } from "react-redux";

import { handleModalClose, selectOpen } from "@/_redux/features/handleModal/slice";

export const Modal = ({ children, initOpen, id }: Readonly<{ children: ReactElement, initOpen: boolean, id: string }>) => {
  const dispatch = useDispatch();
  const open = useSelector(selectOpen)[id] ?? initOpen;

  const { themeName } = useContext(ThemeContext);

  const handleClose = () => dispatch(handleModalClose(id));

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