"use client";

import { type MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useDefaultMaterialReactTable } from "@/_tables";
import { role } from "@/_types";
import { useRolesTable } from "./useRolesTable";

export const RolesTable = () => {
  const {
    roles,
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
  } = useRolesTable();

  const columns = useMemo<MRT_ColumnDef<role>[]>(() => [
    {
      accessorKey: "id",
      header: "ID",
      size: 80,
      Cell: ({ row }) => row.original.id ? row.original.id.slice(0, 8) + "..." : "",
      enableSorting: false,
    },
    {
      accessorKey: "name",
      header: "Name",
      size: 150,
    },
  ], []);

  return useDefaultMaterialReactTable({
    columns,
    data: roles || [],
    title: "Roles",

    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
    rowCount: rowCount ?? roles?.length ?? undefined,

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