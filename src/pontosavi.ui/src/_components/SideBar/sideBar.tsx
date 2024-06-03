"use client";

import { SwipeableDrawer, useMediaQuery, useTheme } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";

import { ButtonList, ISideBarProps } from "./buttomList";
import { Header } from "./header";
import { handleSideBarClose as Close, handleSideBarOpen as Open, selectOpen } from "@/_redux/features/handleSideBar/slice";
import { UpgradeToPro } from "./upgrade";

export const SideBar = ({ buttonList, drawerWidth = 240 }: Readonly<{ buttonList: ISideBarProps[][], drawerWidth: number | undefined }>) => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("sm"));

  const dispatch = useDispatch();
  const open = useSelector(selectOpen);

  const handleSideBarClose = () => dispatch(Close());
  const handleSideBarOpen = () => dispatch(Open());

  return (
    <SwipeableDrawer
      variant={isMobile ? "temporary" : "permanent"}
      open={isMobile ? open : true}
      onOpen={handleSideBarOpen}
      onClose={handleSideBarClose}
      sx={{ "& .MuiDrawer-paper": { width: drawerWidth, borderRight: "none", backgroundImage: "none" } }}
    >
      <Header />
      <ButtonList buttonList={buttonList} />
      <UpgradeToPro />
    </SwipeableDrawer>
  );
};