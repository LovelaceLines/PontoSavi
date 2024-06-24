"use client";

import { Box, Container } from "@mui/material";
import { AccessTime, AccountCircle, AddBusiness, CalendarToday, ControlPointDuplicate, Group, GroupAdd, GroupWork, PersonAdd, Settings, Store, WorkHistory } from "@mui/icons-material";
import { useEffect, useState } from "react";

import { AuthWrapper } from "@/app/auth-wrapper";
import { AppAppBar, ISideBarProps, SideBar } from "@/_components";
import { useSideBar } from "@/_contexts";
import { getBaseUserRoles, getCEOUserRoles, getSuperUserRoles } from "@/globalSettings";

const buttonList: ISideBarProps[][] = [
  [
    { text: "Companies", to: "/app/companies", icon: <Store />, allowRoles: getCEOUserRoles() },
    { text: "Add Company", to: "/app/company-and-user", icon: <AddBusiness />, allowRoles: getCEOUserRoles() },
    { text: "Add User", to: "/app/add-user-company", icon: <PersonAdd />, allowRoles: getCEOUserRoles() },
    { text: "Add Role", to: "/app/add-role-company", icon: <GroupAdd />, allowRoles: getCEOUserRoles() },
  ],
  [
    { text: "My Company", to: "/app/my-company", icon: <Store />, allowRoles: getSuperUserRoles() },
    { text: "Days Off", to: "/app/days-off", icon: <CalendarToday />, allowRoles: getSuperUserRoles() },
    { text: "Work Shifts", to: "/app/work-shifts", icon: <WorkHistory />, allowRoles: getSuperUserRoles() },
    { text: "Accounts", to: "/app/accounts", icon: <Group />, allowRoles: getSuperUserRoles() },
    { text: "Roles", to: "/app/roles", icon: <GroupWork />, allowRoles: getSuperUserRoles() },
    { text: "Points", to: "/app/points", icon: <ControlPointDuplicate />, allowRoles: getSuperUserRoles() },
  ],
  [
    { text: "Point", to: "/app/my-point/check", icon: <AccessTime />, allowRoles: getBaseUserRoles() },
    { text: "My Points", to: "/app/my-points", icon: <ControlPointDuplicate />, allowRoles: getBaseUserRoles() },
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
