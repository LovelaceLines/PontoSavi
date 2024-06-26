/* eslint-disable no-unused-vars */

"use client";

import { createContext, useContext, useState } from "react";

interface IModalContextProps {
  handleModalClose: (key: string) => void;
  handleModalOpen: (key: string) => void;
  isOpen: (key: string) => boolean;
}

export const ModalContext = createContext({} as IModalContextProps);

export const ModalProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  const [open, setOpen] = useState<{ [key: string]: boolean }>({});

  const handleModalClose = (key: string) => {
    setOpen({ ...open, [key]: false });
  };

  const handleModalOpen = (key: string) => {
    setOpen({ ...open, [key]: true });
  };

  const isOpen = (key: string) => open[key] ?? false;

  return (
    <ModalContext.Provider value={{ handleModalClose, handleModalOpen, isOpen }}>
      {children}
    </ModalContext.Provider>
  );
};

export const useModal = () => useContext(ModalContext);
