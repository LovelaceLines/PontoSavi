"use client";

import { type MRT_ColumnDef } from "material-react-table";
import { ContentCopy } from "@mui/icons-material";
import { useMemo } from "react";

import { useDefaultMaterialReactTable } from "@/_tables";
import { user } from "@/_types";
import { useAccountsTable } from "./useAccountsTable";

export const AccountsTable = () => {
  const {
    users,
    rowCount,
    isLoading,
    onSubmit,
    toCreate,
    handleDelete,

    globalFilter,
    setGlobalFilter,
    sorting,
    setSorting,
    columnFilters,
    setColumnFilters,
    pagination,
    setPagination,
  } = useAccountsTable();

  const columns = useMemo<MRT_ColumnDef<user>[]>(() => [
    {
      accessorKey: "publicId",
      header: "ID",
      size: 80,
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
      size: 150,
    },
    {
      accessorKey: "userName",
      header: "User Name",
      size: 150,
    },
    {
      accessorKey: "email",
      header: "Email",
      size: 250,
    },
    {
      accessorKey: "phoneNumber",
      header: "Phone Number",
      size: 200,
      enableSorting: false,
    },
    {
      accessorKey: "roles",
      header: "Roles",
      size: 200,
      Cell: ({ row }) => row.original.roles.join(", "),
      enableColumnFilter: false,
      enableSorting: false,
    },
  ], []);

  return useDefaultMaterialReactTable({
    columns,
    data: users || [],
    title: "Accounts",

    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
    rowCount: rowCount ?? users?.length ?? undefined,

    state: {
      globalFilter,
      columnFilters,
      sorting,
      pagination,
    },

    onSubmit,
    toCreate,
    handleDelete,

    isLoading,
  });
};