"use client";

import { Provider } from "react-redux";

import { SnackbarProvider } from "@/_contexts";
import { store } from "@/_redux/store";
import { TableProvider } from "@/_tables";

export default function ClientProviders({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <>
      <Provider store={store}>
        <SnackbarProvider>
          <TableProvider>
            {children}
          </TableProvider>
        </SnackbarProvider>
      </Provider>
    </>
  );
}
