"use client";

import { type MRT_ColumnDef } from "material-react-table";
import { ContentCopy } from "@mui/icons-material";
import { useMemo } from "react";

import { useDefaultMaterialReactTable, DateTimeToStr } from "@/_tables";
import { point, pointFilter } from "@/_types";
import { usePointsTable } from "./usePointsTable";

export const Status = ({ value }: { value: number }): JSX.Element => {
  switch (value) {
    case 0: return <>Auto Check</>;
    case 1: return <>Manual Check</>;
    case 2: return <>Pending</>;
    case 3: return <>Approved</>;
    case 4: return <>Rejected</>;
    default: return <></>;
  }
};

export const PointsTable = ({ mode = "admin", filters }: { mode: "admin" | "base", filters?: pointFilter }) => {
  const {
    points,
    rowCount,
    isLoading,
    onSubmit,
    toCreate,
    toEdit,

    globalFilter,
    setGlobalFilter,
    sorting,
    setSorting,
    columnFilters,
    setColumnFilters,
    pagination,
    setPagination,
  } = usePointsTable({ mode, filters });

  const columns = useMemo<MRT_ColumnDef<point>[]>(() => [
    {
      accessorKey: "id",
      header: "ID",
      size: 75,
      enableClickToCopy: true,
      muiCopyButtonProps: {
        fullWidth: true,
        startIcon: <ContentCopy />,
        sx: { justifyContent: "flex-start" },
      },
    },
    {
      accessorKey: "userId",
      header: "User ID",
      size: 125,
      enableClickToCopy: true,
      muiCopyButtonProps: {
        fullWidth: true,
        startIcon: <ContentCopy />,
        sx: { justifyContent: "flex-start" },
      },
    },
    {
      accessorKey: "managerId",
      header: "Manager ID",
      size: 125,
      enableClickToCopy: true,
      muiCopyButtonProps: {
        fullWidth: true,
        startIcon: <ContentCopy />,
        sx: { justifyContent: "flex-start" },
      },
    },
    {
      accessorKey: "checkIn",
      header: "Check In",
      Cell: ({ cell }) => <DateTimeToStr date={cell.getValue() as Date} />,
    },
    {
      accessorKey: "checkInStatus",
      header: "Check In Status",
      Cell: ({ cell }) => <Status value={cell.getValue() as number} />,
    },
    {
      accessorKey: "checkInDescription",
      header: "Check In Description",
    },
    {
      accessorKey: "checkOut",
      header: "Check Out",
      Cell: ({ cell }) => <DateTimeToStr date={cell.getValue() as Date} />,
    },
    {
      accessorKey: "checkOutStatus",
      header: "Check Out Status",
      Cell: ({ cell }) => <Status value={cell.getValue() as number} />,
    },
    {
      accessorKey: "checkOutDescription",
      header: "Check Out Description",
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
    data: points || [],
    title: "Accounts",

    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
    rowCount: rowCount ?? points?.length ?? undefined,

    state: {
      globalFilter,
      columnFilters,
      sorting,
      pagination,
    },

    onSubmit,
    toCreate,
    toEdit,

    isLoading,
  });
};