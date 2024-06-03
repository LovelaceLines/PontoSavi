"use client";

import { AppBar, Container, Grid, IconButton, Toolbar } from "@mui/material";
import { NotificationAdd } from "@mui/icons-material";
import Image from "next/image";
import { useContext } from "react";

import { Avatar } from "./avatar";
import { Search } from "./search";
import { colors, ThemeContext } from "@/_theme";
import { ToggleSideBar } from "./toggleSideBar";
import { ToggleThemeIcon } from "./toggleThemeIcon";

export const AppAppBar = () => {
  const { themeName } = useContext(ThemeContext);

  return (
    <AppBar position="sticky" color="transparent" elevation={0} sx={{ bgcolor: `${themeName === "light" ? colors.white : colors.black}` }}>
      <Toolbar>
        <Container disableGutters maxWidth="xl">
          <Grid container display="flex">
            <Grid item xs={9} sm={5} display="flex">
              <ToggleSideBar />
              <Search />
            </Grid>
            <Grid item xs={2} sm={4} />
            <Grid item xs={1} sm={3} display="flex" justifyContent="flex-end" alignContent="center">
              <IconButton color="inherit" sx={{ display: { xs: "none", sm: "flex" } }}>
                <Image src="/icons/languages/portuguese.png" alt="English" width={48} height={48} style={{ width: "24px", height: "24px" }} />
              </IconButton>
              <IconButton color="inherit">
                <NotificationAdd />
              </IconButton>
              <ToggleThemeIcon />
              <Avatar />
            </ Grid>
          </Grid>
        </Container>
      </Toolbar>
    </AppBar>
  );
};