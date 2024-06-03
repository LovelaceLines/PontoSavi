import { Avatar, Box, Grid, Link, Paper, SxProps, Theme, Typography } from "@mui/material";
import Image from "next/image";

import env from "@/env";
import { LightThemeProvider } from "@/_theme";

const backgroundStyle: SxProps<Theme> = {
  backgroundImage: "url(/background/bg1.jpg)",
  backgroundRepeat: "no-repeat",
  backgroundSize: "cover",
  backgroundPosition: "center",
};

const Logo = () => (
  <Box display="flex" flexDirection="row" alignItems="center" gap={2}>
    <Image src="svg/logo/v1.svg" alt="Logo" width={200} height={200} style={{ height: "auto" }} />
    <Avatar src="svg/logo/icon.svg" variant="square" sx={{ width: 56, height: 56 }} />
  </Box>
);

const Copyright = () => (
  <Typography variant="body2" color="text.secondary" align="center">
    {"Copyright © "}
    <Link color="inherit" href={env.APP_URL}>
      PontoSavi
    </Link>{" "}
    {new Date().getFullYear()}
    {"."}
  </Typography>
);

export default function Login({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <LightThemeProvider>
      <Grid container component="main">
        <Grid item xs={false} sm={4} md={7} sx={backgroundStyle} />
        <Grid item xs={12} sm={8} md={5} component={Paper} square>
          <Box display="flex" flexDirection="column" justifyContent="center" alignItems="center" gap={2} px={4} height="100vh">
            <Logo />
            {children}
            <Copyright />
          </Box>
        </Grid>
      </Grid>
    </LightThemeProvider >
  );
}
