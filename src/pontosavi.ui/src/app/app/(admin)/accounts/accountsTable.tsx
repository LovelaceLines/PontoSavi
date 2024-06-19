"use client";

import { type MRT_ColumnDef } from "material-react-table";
import { ContentCopy } from "@mui/icons-material";
import { useMemo } from "react";

import { useDefaultMaterialReactTable, DateTimeToStr } from "@/_tables";
import { user } from "@/_types";
import { useAccountsTable } from "./useAccountsTable";

export const AccountsTable = () => {
  const {
    users,
    rowCount,
    isLoading,
    onSubmit,
    toCreate,
    toEdit,
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
      Cell: ({ row }) => row.original.roles?.map(r => r.name).join(", "),
      enableColumnFilter: false,
      enableSorting: false,
    },
    {
      accessorKey: "createdAt",
      header: "Created At",
      size: 75,
      Cell: ({ cell }) => <DateTimeToStr date={cell.getValue() as Date} />,
    },
    {
      accessorKey: "updatedAt",
      header: "Updated At",
      size: 75,
      Cell: ({ cell }) => <DateTimeToStr date={cell.getValue() as Date} />,
    }
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
    toEdit,
    handleDelete,

    isLoading,
  });
};