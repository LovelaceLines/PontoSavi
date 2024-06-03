"use client";

import { MRT_ColumnFiltersState, MRT_PaginationState, MRT_SortingState } from "material-react-table";
import React, { createContext, Dispatch, SetStateAction, useContext, useState } from "react";

interface TableProps {
  state: {
    globalFilter: string;
    sorting: MRT_SortingState;
    columnFilters: MRT_ColumnFiltersState;
    pagination: MRT_PaginationState;
  };
  setGlobalFilter: Dispatch<SetStateAction<string>>;
  setSorting: Dispatch<SetStateAction<MRT_SortingState>>;
  setColumnFilters: Dispatch<SetStateAction<MRT_ColumnFiltersState>>;
  setPagination: Dispatch<SetStateAction<MRT_PaginationState>>;
}

const TableContext = createContext<TableProps>({} as TableProps);

export const TableProvider = ({ children }: { children: React.ReactNode }) => {
  const [state, setState] = useState<TableProps["state"]>({
    globalFilter: "",
    sorting: [],
    columnFilters: [],
    pagination: { pageIndex: 0, pageSize: 50 },
  });

  const setGlobalFilter: Dispatch<SetStateAction<string>> = (value: SetStateAction<string>) => {
    return setState(prev => ({
      ...prev,
      globalFilter: typeof value === "function" ?
        (value as (prevState: string) => string)(prev.globalFilter) :
        value
    }));
  };

  const setSorting: Dispatch<SetStateAction<MRT_SortingState>> = (value: SetStateAction<MRT_SortingState>) => {
    return setState(prev => ({
      ...prev,
      sorting: typeof value === "function" ?
        (value as (prevState: MRT_SortingState) => MRT_SortingState)(prev.sorting) :
        value
    }));
  };

  const setColumnFilters: Dispatch<SetStateAction<MRT_ColumnFiltersState>> = (value: SetStateAction<MRT_ColumnFiltersState>) => {
    return setState(prev => ({
      ...prev,
      columnFilters: typeof value === "function" ?
        (value as (prevState: MRT_ColumnFiltersState) => MRT_ColumnFiltersState)(prev.columnFilters) :
        value
    }));
  };

  const setPagination: Dispatch<SetStateAction<MRT_PaginationState>> = (value: SetStateAction<MRT_PaginationState>) => {
    return setState(prev => ({
      ...prev,
      pagination: typeof value === "function" ?
        (value as (prevState: MRT_PaginationState) => MRT_PaginationState)(prev.pagination) :
        value
    }));
  };

  return (
    <TableContext.Provider value={{
      state,
      setGlobalFilter,
      setSorting,
      setColumnFilters,
      setPagination,
    }}>
      {children}
    </TableContext.Provider>
  );
};

export const useTable = () => {
  return useContext(TableContext);
};