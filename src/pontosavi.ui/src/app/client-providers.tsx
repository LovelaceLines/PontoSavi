"use client";

import { Provider } from "react-redux";

import { ModalProvider, SideBarProvider, SnackbarProvider } from "@/_contexts";
import { store } from "@/_redux/store";
import { TableProvider } from "@/_tables";

export default function ClientProviders({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <>
      <Provider store={store}>
        <SideBarProvider>
          <SnackbarProvider>
            <ModalProvider>
              <TableProvider>
                {children}
              </TableProvider>
            </ModalProvider>
          </SnackbarProvider>
        </SideBarProvider>
      </Provider>
    </>
  );
}
