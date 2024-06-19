"use client";

import { type MRT_ColumnDef } from "material-react-table";
import { ContentCopy } from "@mui/icons-material";
import { useMemo } from "react";

import { useDefaultMaterialReactTable, DateOnlyToStr, DateTimeToStr } from "@/_tables";
import { dayOff } from "@/_types";
import { useDaysOffTable } from "./useDaysOffTable";

export const DaysOffTable = () => {
  const {
    daysOff,
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
  } = useDaysOffTable();

  const columns = useMemo<MRT_ColumnDef<dayOff>[]>(() => [
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
      accessorKey: "description",
      header: "Description",
      // size: 150,
    },
    {
      accessorKey: "date",
      header: "Date",
      size: 25,
      Cell: ({ cell }) => <DateOnlyToStr date={cell.getValue() as Date} />,
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
    },
  ], []);

  return useDefaultMaterialReactTable({
    columns,
    data: daysOff || [],
    title: "Accounts",

    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
    rowCount: rowCount ?? daysOff?.length ?? undefined,

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