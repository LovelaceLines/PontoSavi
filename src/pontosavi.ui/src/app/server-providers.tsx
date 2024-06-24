import { ThemeProvider } from "@/_theme";

export default function ServerProviders({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <ThemeProvider>
      {children}
    </ThemeProvider>
  );
}
