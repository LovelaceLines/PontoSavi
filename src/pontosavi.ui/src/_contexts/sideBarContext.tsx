"use client";

import { createContext, useContext, useState } from "react";

import { getStorageValue, setStorageValue } from "@/_services";

interface ISideBarContextProps {
  open: boolean;
  toggleSideBar: () => void;
}

export const SideBarContext = createContext({} as ISideBarContextProps);

export const SideBarProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  const [open, _setOpen] = useState<boolean>(getStorageValue("sideBarOpen", true) as boolean);

  const toggleSideBar = () => {
    setStorageValue("sideBarOpen", !open);
    _setOpen(!open);
  };

  return (
    <SideBarContext.Provider value={{ open, toggleSideBar }}>
      {children}
    </SideBarContext.Provider>
  );
};

export const useSideBar = () => {
  const { open, toggleSideBar } = useContext(SideBarContext);
  return { open, toggleSideBar };
};
