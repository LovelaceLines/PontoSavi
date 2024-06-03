import { Box, CircularProgress, Container } from "@mui/material";

export const Loading = () => {
  return (
    <Container>
      <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
        <CircularProgress />
      </Box>
    </Container>
  );
};