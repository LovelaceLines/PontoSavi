"use client";

import { type MRT_ColumnDef } from "material-react-table";
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
      size: 80,
      Cell: ({ row }) => row.original.id ? row.original.id.slice(0, 8) + "..." : "",
      enableSorting: false,
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
    toEdit,
    handleDelete,

    isLoading,
  });
};