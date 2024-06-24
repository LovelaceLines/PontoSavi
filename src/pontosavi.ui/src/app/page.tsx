import { AppBar as AppBarMUI, Avatar, Box, Button, Container, Grid, Toolbar, Typography } from "@mui/material";
import Image from "next/image";
import Link from "next/link";

import { colors } from "@/_theme";

const Logo = () => (
  <Link
    href="/app"
    style={{ display: "flex", flexDirection: "row", alignItems: "center", gap: 16 }}
  >
    <Box sx={{ display: { xs: "none", sm: "block" } }}>
      <Image
        src="/svg/logo/v1.svg"
        width={90} height={90} alt="Logo"
        style={{ height: "auto", }}
      />
    </Box>
    <Avatar
      src="/svg/logo/icon.svg"
      variant="circular"
      sx={{ width: 32, height: 32 }}
    />
  </Link>
);

const AppBar = () => (
  <AppBarMUI position="sticky" color="transparent" elevation={0} sx={{ bgcolor: `${colors.eerieBlack}` }}>
    <Toolbar disableGutters >
      <Container maxWidth="xl">
        <Grid container display="flex">
          <Grid item xs={1} sm={1}>
            <Logo />
          </Grid>
          <Grid item xs={11} sm={11} display="flex" justifyContent="flex-end" alignContent="center" gap={2}>
            <Link href="/features" style={{ textDecorationLine: "none" }}>
              <Button variant="text" sx={{ display: { xs: "none", sm: "block" } }}>
                Features
              </Button>
            </Link>
            <Link href="/support" style={{ textDecorationLine: "none" }}>
              <Button variant="text" sx={{ display: { xs: "none", sm: "block" } }}>
                Support
              </Button>
            </Link>
            <Link href="/signup">
              <Button variant="outlined" color="primary">Sign Up</Button>
            </Link>
            <Link href="/signin">
              <Button variant="contained">Sign In</Button>
            </Link>
          </ Grid>
        </Grid>
      </Container>
    </Toolbar>
  </AppBarMUI>
);

const Hero = () => (
  <Box display="flex" flexDirection="column" gap={2}>
    <Box display="flex" flexDirection={{ xs: "column-reverse", md: "column" }} gap={1}>
      <Typography variant="h6">
        Facilidade, Controle e Segurança para Suas Jornadas de Trabalho.
      </Typography>
      <Typography variant="h3">
        PontoSavi: A Melhor Solução para Gestão de Ponto!
      </Typography>
    </Box>
    <Typography variant="subtitle1">
      Desenvolvido para tornar a gestão de horas trabalhadas simples e eficiente. Configure feriados, organize jornadas fixas, personalize turnos e exporte dados com facilidade. Mantenha-se sempre atualizado com informações em tempo real, garantindo a conformidade com as normas trabalhistas e a segurança dos seus dados. Experimente agora a melhor solução para gestão de ponto!
    </Typography>
    <Button variant="outlined">Experimente Grátis</Button>
  </Box>
);

interface cardProps {
  imgPath: string;
  label: string;
}

const cardsProps: cardProps[] = [
  {
    imgPath: "bg-blur-01",
    label: "Phasellus sit amet nibh non lectus finibus bibendum. Praesent viverra quam placerat posuere tempor. In luctus sodales nisi, non feugiat libero faucibus eget. Proin ut dignissim ex. Suspendisse quis magna tempor, hendrerit enim vel, feugiat velit. Duis egestas, neque nec vulputate blandit, velit magna fringilla enim, a dignissim nibh justo non turpis. Sed imperdiet magna quis lobortis faucibus.",
  },
  {
    imgPath: "bg-blur-02",
    label: "Maecenas ultricies ac sapien sit amet facilisis. Etiam a leo mattis, sodales sem in, gravida nibh. Sed tincidunt egestas nunc a rutrum. Nullam at arcu accumsan, finibus quam vitae, auctor ante. Vivamus ligula felis, tempus et purus vel, lacinia sagittis mi. Vivamus id dolor at felis tincidunt facilisis quis vitae risus. Aenean tempor nulla quam, at convallis sapien laoreet at.",
  },
  {
    imgPath: "bg-blur-03",
    label: "In dictum ante accumsan nisi dictum rutrum. Sed sem orci, mattis sed pharetra ut, aliquet sed libero. Phasellus tempus leo ornare bibendum tristique. Nam id sem ultrices, pretium turpis at, egestas ipsum. Etiam sit amet mauris bibendum, dapibus massa quis, ullamcorper ex. Praesent",
  },
  {
    imgPath: "bg-blur-04",
    label: "congue, mi eu tempus viverra, enim nulla laoreet risus, sed convallis urna tortor nec quam. Ut blandit efficitur malesuada. Nulla nec urna vitae neque semper maximus consectetur sit amet purus. Etiam rutrum nibh erat. Vestibulum convallis justo ut nisi hendrerit accumsan. Aliquam cursus at mi sit amet tincidunt. Cras",
  },
  {
    imgPath: "bg-blur-05",
    label: "Mauris ullamcorper arcu sapien, eget ullamcorper ex aliquet id. Maecenas in dapibus urna. Maecenas dignissim nulla erat, a accumsan nibh cursus at. Pellentesque velit dolor, tincidunt sed ipsum nec, tempus faucibus enim. Vivamus congue nisl tortor, ac sollicitudin lorem imperdiet quis. Nulla dictum consequat ante eget suscipit. Aliquam malesuada tristique dolor, eget laoreet ex porttitor ac.",
  },
];

const Card = ({ imgPath, label }: cardProps) => (
  <Box sx={{ position: "relative" }}>
    <Typography
      variant="subtitle1"
      flexWrap={"wrap"}
      textOverflow={"ellipsis"}
      whiteSpace="wrap"
      overflow="hidden"
      sx={{
        position: "absolute",
        zIndex: 1,
        bottom: 20,
        left: 20,
        right: 20,
        top: 20,
      }}
    >
      {label}
    </Typography>
    <Image
      width={1980} height={1080} alt={""}
      src={`/landpage/img-cards/${imgPath}.jpg`}
      style={{ width: "100%", height: "100%", borderRadius: "5%" }}
    />
  </Box>
);

const Cards = () => (
  <Grid container spacing={2}>
    {cardsProps.map((cardProps, index) => (
      <Grid item xs={12} sm={6} lg={4} key={index}>
        <Card {...cardProps} />
      </Grid>
    ))}
  </Grid>
);

export default function Page() {
  return (
    <>
      <AppBar />
      <br />
      <Container maxWidth="xl">
        <Grid container spacing={6} rowSpacing={{ xs: 2, sm: 4, md: 6 }}>
          <Grid item xs={12} >
            <Hero />
          </Grid>
          <Grid item xs={12}>
            <Cards />
          </Grid>
        </Grid>
      </Container>
    </>
  );
}
