"use client";

import { SwipeableDrawer, useMediaQuery, useTheme } from "@mui/material";

import { ButtonList, ISideBarProps } from "./buttomList";
import { useSideBar } from "@/_contexts";
import { Header } from "./header";
import { UpgradeToPro } from "./upgrade";

export const SideBar = ({ buttonList, drawerWidth = 240, minDrawerWidth = 56 }: Readonly<{ buttonList: ISideBarProps[][], drawerWidth: number | undefined, minDrawerWidth: number | undefined }>) => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("sm"));

  const { open, toggleSideBar } = useSideBar();

  return (
    <SwipeableDrawer
      variant={isMobile ? "temporary" : "permanent"}
      open={isMobile ? open : true}
      onOpen={() => toggleSideBar()}
      onClose={() => toggleSideBar()}
      sx={{
        "& .MuiDrawer-paper": {
          width: open ? drawerWidth : minDrawerWidth,
          borderRight: "none",
          backgroundImage: "none",
          overflowX: "hidden"
        }
      }}
    >
      <Header />
      <ButtonList buttonList={buttonList} />
      {!isMobile && open && <UpgradeToPro />}
    </SwipeableDrawer>
  );
};