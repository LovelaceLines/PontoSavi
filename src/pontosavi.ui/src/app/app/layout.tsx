"use client";

import { Box, Container } from "@mui/material";
import { AccountCircle, BrowseGallery, Delete, Settings } from "@mui/icons-material";

import { AuthWrapper } from "@/app/auth-wrapper";
import { AppAppBar, ISideBarProps, SideBar } from "@/_components";
import { useSelector } from "react-redux";
import { selectOpen } from "@/_redux/features/handleSideBar/slice";

const buttonList: ISideBarProps[][] = [
  [
    { text: "Ponto", to: "/app/ponto", icon: <BrowseGallery /> },
    { text: "Recycle Bin", to: "/app/recycle-bin", icon: <Delete /> },
  ],
  [
    { text: "My Account", to: "/app/my-account", icon: <AccountCircle /> },
    { text: "Settings", to: "/app/settings", icon: <Settings /> }
  ],
];

const drawerWidth = 240;
const minDrawerWidth = 48;

export default function Layout({ children }: Readonly<{ children: React.ReactNode }>) {
  const open = useSelector(selectOpen);

  return (
    <AuthWrapper>
      <Box display="flex">
        <SideBar buttonList={buttonList} drawerWidth={drawerWidth} minDrawerWidth={minDrawerWidth} />
        <Box
          display="flex"
          flexDirection="column"
          sx={{
            width: { xs: "100%", sm: `calc(100% - ${open ? drawerWidth : minDrawerWidth}px)` },
            ml: { sm: `${open ? drawerWidth : minDrawerWidth}px` }
          }}>
          <AppAppBar />
          <Container maxWidth="xl" sx={{ my: 2 }}>
            {children}
          </Container>
        </Box>
      </Box>
    </AuthWrapper>
  );
}
