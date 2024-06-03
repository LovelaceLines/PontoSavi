"use client";

import { Divider, List, ListItem, ListItemButton, ListItemIcon, ListItemText } from "@mui/material";
import { Logout } from "@mui/icons-material";
import Link from "next/link";
import React from "react";

import { useDispatch } from "react-redux";
import { logoutUser } from "@/_redux/features/auth/slice";

export interface ISideBarProps {
  text: string;
  to: string;
  icon: React.ReactNode;
}

const logoutButton: ISideBarProps = { text: "Logout", to: "/signin", icon: <Logout /> };

export const ButtonList = ({ buttonList }: Readonly<{ buttonList: ISideBarProps[][] }>) => {
  const dispatch = useDispatch();

  return (
    <List>

      {buttonList.map((subList, index) =>
        <React.Fragment key={index}>
          {subList.map(({ text, to, icon }, index) => (
            <ListItem key={text + index} disablePadding sx={{ display: "block" }}>
              <Link href={to} style={{ color: "inherit", textDecoration: "none" }}>
                <ListItemButton>
                  <ListItemIcon sx={{ color: "inherit" }}>{icon}</ListItemIcon>
                  <ListItemText primary={text} sx={{ opacity: 1, textWrap: "nowrap" }} />
                </ListItemButton>
              </Link>
            </ListItem>
          ))}
          <Divider key={"Divider" + index} />
        </React.Fragment>
      )}

      <ListItem key={logoutButton.text} disablePadding sx={{ display: "block" }}>
        <ListItemButton onClick={() => dispatch(logoutUser())}>
          <ListItemIcon sx={{ color: "inherit" }}>{logoutButton.icon}</ListItemIcon>
          <ListItemText primary={logoutButton.text} sx={{ opacity: 1 }} />
        </ListItemButton>
      </ListItem>

    </List>
  );
};