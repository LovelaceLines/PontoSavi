import { Menu } from "@mui/icons-material";
import { IconButton } from "@mui/material";
import { useDispatch } from "react-redux";

import { handleSideBarOpen as Open } from "@/_redux/features/handleSideBar/slice";

export const ToggleSideBar = () => {
  const dispatch = useDispatch();

  const handleSideBarOpen = () => {
    dispatch(Open());
  };

  return (
    <IconButton
      color="inherit"
      onClick={handleSideBarOpen}
      sx={{ display: { xs: "flex", sm: "none" } }}
    >
      <Menu />
    </IconButton>
  );
};