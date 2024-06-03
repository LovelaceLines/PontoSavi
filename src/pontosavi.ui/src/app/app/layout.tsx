import { Box, Container } from "@mui/material";
import { AccountCircle, BrowseGallery, Delete, Settings } from "@mui/icons-material";

import { AuthWrapper } from "@/app/auth-wrapper";
import { AppAppBar, ISideBarProps, SideBar } from "@/_components";

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

export default function Layout({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <AuthWrapper>
      <Box display="flex">
        <SideBar buttonList={buttonList} drawerWidth={drawerWidth} />
        <Box display="flex" flexDirection="column" sx={{ width: { xs: "100%", sm: `calc(100% - ${drawerWidth}px)` }, ml: { sm: `${drawerWidth}px` } }}>
          <AppAppBar />
          <Container maxWidth="xl" sx={{ my: 2 }}>
            {children}
          </Container>
        </Box>
      </Box>
    </AuthWrapper>
  );
}
