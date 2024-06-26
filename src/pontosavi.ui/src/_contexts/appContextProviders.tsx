"use client";

import { ModalProvider, SideBarProvider, SnackbarProvider } from "@/_contexts";

export const AppContextProviders = ({ children }: Readonly<{ children: React.ReactNode }>) => (
  <SideBarProvider>
    <SnackbarProvider>
      <ModalProvider>
        {children}
      </ModalProvider>
    </SnackbarProvider>
  </SideBarProvider>
);
