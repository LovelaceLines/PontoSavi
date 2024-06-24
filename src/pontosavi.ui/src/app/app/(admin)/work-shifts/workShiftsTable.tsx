"use client";

import { type MRT_ColumnDef } from "material-react-table";
import { ContentCopy } from "@mui/icons-material";
import { useMemo } from "react";

import { useDefaultMaterialReactTable, DateTimeToStr } from "@/_tables";
import { workShift } from "@/_types";
import { useWorkShiftsTable } from "./useWorkShiftsTable";
import { Actions } from "./workShiftComponents";

export const WorkShiftsTable = () => {
  const {
    workShits,
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
    },
    {
      accessorKey: "checkInToleranceMinutes",
      header: "Check-in Tolerance",
    },
    {
      accessorKey: "checkOut",
      header: "Check-out",
    },
    {
      accessorKey: "checkOutToleranceMinutes",
      header: "Check-out Tolerance",
    },
    {
      accessorKey: "description",
      header: "Description",
    },
    {
      accessorKey: "user.name",
      header: "User",
      enableColumnFilter: false,
      enableSorting: false,
    },
    {
      accessorKey: "company.name",
      header: "Company",
      enableColumnFilter: false,
      enableSorting: false,
    },
    {
      accessorKey: "createdAt",
      header: "Created At",
      enableColumnFilter: false,
      enableSorting: false,
      Cell: ({ cell }) => <DateTimeToStr date={cell.getValue() as Date} />,
    },
    {
      accessorKey: "updatedAt",
      header: "Updated At",
      enableColumnFilter: false,
      enableSorting: false,
      Cell: ({ cell }) => <DateTimeToStr date={cell.getValue() as Date} />,
    }
  ], []);

  return useDefaultMaterialReactTable({
    columns,
    data: workShits || [],
    title: "Accounts",

    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
    rowCount: rowCount ?? workShits?.length ?? undefined,

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

    enableRowActions: true,
    renderRowActions: ({ row }) => <Actions row={row.original} />,
  });
};