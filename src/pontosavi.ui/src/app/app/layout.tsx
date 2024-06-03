"use client";

import { Box, Container } from "@mui/material";
import { AccountCircle, BrowseGallery, Delete, Settings } from "@mui/icons-material";
import { useEffect, useState } from "react";

import { AuthWrapper } from "@/app/auth-wrapper";
import { AppAppBar, ISideBarProps, SideBar } from "@/_components";
import { useSideBar } from "@/_contexts";

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
const minDrawerWidth = 56;

export default function Layout({ children }: Readonly<{ children: React.ReactNode }>) {
  const { open } = useSideBar();

  // Next.js Hydration
  const [isClient, setIsClient] = useState(false);

  useEffect(() => {
    setIsClient(true);
  }, []);

  if (!isClient) {
    return null;
  }

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
