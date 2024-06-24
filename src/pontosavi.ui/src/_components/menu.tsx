"use client";

import { ReactElement, useState } from "react";
import { Menu as MenuMUI, MenuItem, Button } from "@mui/material";
import { MoreHoriz } from "@mui/icons-material";

export const Menu = ({ name, items }: { name?: string, items?: ReactElement[] }) => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => setAnchorEl(event.currentTarget);

  const handleClose = () => setAnchorEl(null);

  return (
    <>
      <Button
        size="small"
        onClick={handleClick}
      >
        {name ?? <MoreHoriz />}
      </Button>
      <MenuMUI
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
      >
        {items?.map((item, index) =>
          <MenuItem
            key={index}
            dense divider
            disableGutters={true}
            onClick={handleClose}
          >
            {item}
          </MenuItem>
        )}
      </MenuMUI>
    </>
  );
};
