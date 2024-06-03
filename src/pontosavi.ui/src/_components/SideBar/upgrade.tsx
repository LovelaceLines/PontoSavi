import { Box, Button, Typography } from "@mui/material";
import Image from "next/image";

export const UpgradeToPro = () => {
  return (
    <Box display="flex" flexDirection="column" justifyContent="center" alignItems="center" width="100%" height="100%" p={2}>
      <Image src="/upgrade-to-pro-v2.svg" alt="Upgrade to Pro" width={200} height={200} style={{ height: "auto" }} />
      <Typography variant="h6" m={0} p={0}>
        Get More?
      </Typography>
      <Typography variant="subtitle1">
        From only $4.99/month
      </Typography>
      <Button variant="contained" color="primary" sx={{ mt: 1 }}>
        Upgrade to PRO
      </Button>
    </Box>
  );
};