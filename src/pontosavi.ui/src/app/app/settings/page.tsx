"use client";

import { Box, Button, Typography } from "@mui/material";
import { ArrowBackIos } from "@mui/icons-material";
import Image from "next/image";
import { useRouter } from "next/navigation";

export default function Page() {
  const { back } = useRouter();

  return (
    <Box display="flex" flexDirection="column" justifyContent="center" alignItems="center" gap={2}>
      <Box display="flex" alignItems="center" width={{ xs: "80vw", md: "50vw", lg: "30vw" }}>
        <Image src="https://c.tenor.com/ftaDAT-sWg4AAAAC/tenor.gif" alt="Imagem d" width={1080} height={1080} style={{ width: "100%", height: "auto" }} />
      </Box>

      <Box textAlign="center">
        <Typography variant="h1">501</Typography>
        <Typography variant="body1">Página não implementada :/</Typography>
      </Box>

      <Button variant="contained" startIcon={<ArrowBackIos />} onClick={() => back()}>
        Voltar para a página anterior
      </Button>
      <Button href="/app">
        Ir para a página inicial
      </Button>
    </Box>
  );
}