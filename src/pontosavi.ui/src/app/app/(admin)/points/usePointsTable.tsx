"use client";

import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { selectStatus, selectTotalCount, selectPoints } from "@/_redux/features/point/slice";
import { getPoints } from "@/_redux/features/point/thunks";
import { AppDispatch } from "@/_redux/store";
import { useTable } from "@/_tables/useTable";
import { pointFilter, pointStatus } from "@/_types";

export const usePointsTable = ({ mode = "admin", filters }: { mode?: "admin" | "base", filters?: pointFilter }) => {
  const {
    state: { globalFilter, sorting, columnFilters, pagination },
    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
  } = useTable();

  useEffect(() => { onSubmit(); }, [pagination]);

  const dispatch = useDispatch<AppDispatch>();

  const points = useSelector(selectPoints);
  const rowCount = useSelector(selectTotalCount);
  const status = useSelector(selectStatus);
  const isLoading = () => status === "loading";

  const onSubmit = () => dispatch(getPoints({
    id: columnFilters.find(f => f.id === "id")?.value as number || undefined,
    userId: filters?.userId ?? columnFilters.find(f => f.id === "userId")?.value as number ?? undefined,
    managerId: columnFilters.find(f => f.id === "managerId")?.value as number || undefined,
    checkIn: columnFilters.find(f => f.id === "checkIn")?.value as Date || undefined,
    checkInStatus: columnFilters.find(f => f.id === "checkInStatus")?.value as pointStatus || undefined,
    checkOut: columnFilters.find(f => f.id === "checkOut")?.value as Date || undefined,
    checkOutStatus: columnFilters.find(f => f.id === "checkOutStatus")?.value as pointStatus || undefined,
    search: globalFilter || undefined,
    pageIndex: pagination.pageIndex,
    pageSize: pagination.pageSize,
    idDescOrderSort: sorting.find(s => s.id === "id") ? sorting.find(s => s.id === "id")?.desc : undefined,
    checkInDescOrderSort: sorting.find(s => s.id === "checkIn") ? sorting.find(s => s.id === "checkIn")?.desc : undefined,
    checkOutDescOrderSort: sorting.find(s => s.id === "checkOut") ? sorting.find(s => s.id === "checkOut")?.desc : undefined,
  }));

  const toCreate = mode === "admin" ? undefined : "my-point";
  const toEdit = mode === "admin" ? undefined : "my-point";

  return {
    points,
    rowCount,
    isLoading,
    onSubmit,
    toCreate,
    toEdit,

    globalFilter,
    sorting,
    columnFilters,
    pagination,
    setGlobalFilter,
    setSorting,
    setColumnFilters,
    setPagination,
  };
};
