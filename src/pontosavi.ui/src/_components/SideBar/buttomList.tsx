"use client";

import { Divider, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Tooltip } from "@mui/material";
import { Logout } from "@mui/icons-material";
import Link from "next/link";
import React from "react";

import { useSideBar } from "@/_contexts";
import { useDispatch, useSelector } from "react-redux";
import { logoutUser, selectUser } from "@/_redux/features/auth/slice";
import { includes } from "@/_utils";
import { getUserDefaultRole } from "@/globalSettings";
import { colors } from "@/_theme";

export interface ISideBarProps {
  text: string;
  to: string;
  icon: React.ReactElement;
  allowRoles?: string[];
}

const logoutButton: ISideBarProps = { text: "Logout", to: "/signin", icon: <Logout /> };

export const ButtonList = ({ buttonList }: Readonly<{ buttonList: ISideBarProps[][] }>) => {
  const dispatch = useDispatch();
  const user = useSelector(selectUser);
  const { open } = useSideBar();

  return (
    <List>

      {buttonList.map((subList, index) =>
        <React.Fragment key={index}>
          {subList.map(({ text, to, icon, allowRoles }, index) => (
            <ListItem key={text + index} disablePadding sx={{ display: includes(user?.roles?.map(r => r.name) ?? [], allowRoles || [getUserDefaultRole()]) ? "block" : "none" }}>
              <Link href={to} style={{ color: "inherit", textDecoration: "none" }}>
                <ListItemButton>
                  <ListItemIcon sx={{ color: "inherit" }}>
                    <Tooltip title={text} placement="right" arrow disableHoverListener={open}>
                      {icon}
                    </Tooltip>
                  </ListItemIcon>
                  <ListItemText primary={text} sx={{ opacity: 1, textWrap: "nowrap" }} />
                </ListItemButton>
              </Link>
            </ListItem>
          ))}
          <Divider key={"Divider" + index} sx={{ borderColor: colors.jet }} />
        </React.Fragment>
      )}

      <ListItem key={logoutButton.text} disablePadding sx={{ display: "block" }}>
        <ListItemButton onClick={() => dispatch(logoutUser())}>
          <ListItemIcon sx={{ color: "inherit" }}>
            <Tooltip title={logoutButton.text} placement="right" disableHoverListener={open} arrow>
              {logoutButton.icon}
            </Tooltip>
          </ListItemIcon>
          <ListItemText primary={logoutButton.text} sx={{ opacity: 1 }} />
        </ListItemButton>
      </ListItem>

    </List>
  );
};