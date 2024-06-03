"use client";

import { SwipeableDrawer, useMediaQuery, useTheme } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";

import { ButtonList, ISideBarProps } from "./buttomList";
import { Header } from "./header";
import { toggleSideBar, selectOpen } from "@/_redux/features/handleSideBar/slice";
import { UpgradeToPro } from "./upgrade";

export const SideBar = ({ buttonList, drawerWidth = 240, minDrawerWidth = 240 }: Readonly<{ buttonList: ISideBarProps[][], drawerWidth: number | undefined, minDrawerWidth: number | undefined }>) => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("sm"));

  const dispatch = useDispatch();
  const open = useSelector(selectOpen);

  return (
    <SwipeableDrawer
      variant={isMobile ? "temporary" : "permanent"}
      open={isMobile ? open : true}
      onOpen={() => dispatch(toggleSideBar())}
      onClose={() => dispatch(toggleSideBar())}
      sx={{ "& .MuiDrawer-paper": { width: open ? drawerWidth : minDrawerWidth, borderRight: "none", backgroundImage: "none" } }}
    >
      <Header />
      <ButtonList buttonList={buttonList} />
      <UpgradeToPro />
    </SwipeableDrawer>
  );
};