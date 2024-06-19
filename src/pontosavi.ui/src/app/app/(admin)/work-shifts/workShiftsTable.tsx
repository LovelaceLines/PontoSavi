"use client";

import { type MRT_ColumnDef } from "material-react-table";
import { ContentCopy } from "@mui/icons-material";
import { useMemo } from "react";

import { useDefaultMaterialReactTable, DateTimeToStr } from "@/_tables";
import { workShift } from "@/_types";
import { useWorkShiftsTable } from "./useWorkShiftsTable";

export const WorkShiftsTable = () => {
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
  } = useWorkShiftsTable();

  const columns = useMemo<MRT_ColumnDef<workShift>[]>(() => [
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
      accessorKey: "checkIn",
      header: "Check-in",
      size: 25,
    },
    {
      accessorKey: "checkInToleranceMinutes",
      header: "Check-in Tolerance",
      size: 25,
    },
    {
      accessorKey: "checkOut",
      header: "Check-out",
      size: 25,
    },
    {
      accessorKey: "checkOutToleranceMinutes",
      header: "Check-out Tolerance",
      size: 25,
    },
    {
      accessorKey: "description",
      header: "Description",
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