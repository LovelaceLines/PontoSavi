import { Typography } from "@mui/material";

export default function Layout({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <>
      <Typography variant="h5" mb={2}>Point</Typography>
      {children}
    </>
  );
}
