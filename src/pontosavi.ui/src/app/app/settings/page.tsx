"use client";

import { InfoOutlined } from "@mui/icons-material";
import { Box, Button, Tooltip, Typography } from "@mui/material";

const resetTableSettings = () => {
  const keys: string[] = [];

  for (let i = 0; i < localStorage.length; i++) {
    var key = localStorage.key(i);
    if (key) keys.push(key);
  }

  keys.filter(key => key.startsWith("@pontosavi:mrt-column-visibility-"))
    .forEach(key => localStorage.removeItem(key));
};

export default function Page() {
  return (
    <>
      <Typography variant="h5" mb={2}>Settings</Typography>

      <Box display="flex" alignItems="center" gap={2}>
        <Button variant="contained" onClick={resetTableSettings}>
          Reset table settings
        </Button>
        <Tooltip title="Reset show/hide columns. This will reset all columns visibility settings for all tables in the application." placement="right">
          <InfoOutlined />
        </Tooltip >
      </Box >
    </>
  );
}