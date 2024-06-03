import { Menu } from "@mui/icons-material";
import { IconButton } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";

import { toggleSideBar, selectOpen } from "@/_redux/features/handleSideBar/slice";

export const ToggleSideBar = () => {
  const dispatch = useDispatch();
  const open = useSelector(selectOpen);

  return (
    <IconButton
      color="inherit"
      onClick={() => dispatch(toggleSideBar())}
      sx={{ display: { xs: "flex", sm: open ? "none" : "flex" } }}
    >
      <Menu />
    </IconButton>
  );
};