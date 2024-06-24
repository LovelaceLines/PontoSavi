import { Box, CircularProgress, Container } from "@mui/material";

export const Loading = ({ height = "100vh" }: { height?: "100vh" | "auto" }) => {
  return (
    <Container>
      <Box display="flex" justifyContent="center" alignItems="center" height={height}>
        <CircularProgress />
      </Box>
    </Container>
  );
};