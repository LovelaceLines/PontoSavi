"use client";

import { type MRT_ColumnDef } from "material-react-table";
import { ContentCopy } from "@mui/icons-material";
import { useMemo } from "react";

import { DateTimeToStr, useDefaultMaterialReactTable } from "@/_tables";
import { company } from "@/_types";
import { useCompaniesTable } from "./useCompaniesTable";

export const CompaniesTable = () => {
  const {
    companies,
    rowCount,
    isLoading,
    onSubmit,
    toCreate,

    globalFilter,
    setGlobalFilter,
    sorting,
    setSorting,
    columnFilters,
    setColumnFilters,
    pagination,
    setPagination,
  } = useCompaniesTable();

  const columns = useMemo<MRT_ColumnDef<company>[]>(() => [
    {
      accessorKey: "id",
      header: "ID",
      size: 25,
      enableClickToCopy: true,
      muiCopyButtonProps: {
        fullWidth: true,
        startIcon: <ContentCopy />,
        sx: { justifyContent: "flex-start" },
      },
    },
    {
      accessorKey: "name",
      header: "Name",
      // size: 150,
    },
    {
      accessorKey: "tradeName",
      header: "Trade Name",
      // size: 150,
    },
    {
      accessorKey: "cnpj",
      header: "CNPJ",
      enableSorting: false,
      enableClickToCopy: true,
      muiCopyButtonProps: {
        fullWidth: true,
        startIcon: <ContentCopy />,
        sx: { justifyContent: "flex-start" },
      },
      Cell: ({ cell }) => (
        <>{(cell.getValue() as string).replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5")}</>
      ),
    },
    {
      accessorKey: "createdAt",
      header: "Created At",
      Cell: ({ cell }) => <DateTimeToStr date={cell.getValue() as Date} />,
    },
    {
      accessorKey: "updatedAt",
      header: "Updated At",
      Cell: ({ cell }) => <DateTimeToStr date={cell.getValue() as Date} />,
    },
  ], []);

  return useDefaultMaterialReactTable({
    columns,
    data: companies || [],
    title: "Accounts",

    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
    rowCount: rowCount ?? companies?.length ?? undefined,

    state: {
      globalFilter,
      columnFilters,
      sorting,
      pagination,
    },

    onSubmit,
    toCreate,

    isLoading,
  });
};