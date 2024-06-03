import { AppThemeProvider } from "@/_theme";

export default function ServerProviders({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <AppThemeProvider>
      {children}
    </AppThemeProvider>
  );
}
