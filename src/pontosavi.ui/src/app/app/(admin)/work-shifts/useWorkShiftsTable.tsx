"use client";

import { useEffect, useMemo } from "react";
import { useDispatch, useSelector } from "react-redux";

import { selectStatus, selectTotalCount, selectWorkShifts } from "@/_redux/features/workShift/slice";
import { deleteWorkShift, getWorkShifts } from "@/_redux/features/workShift/thunks";
import { AppDispatch } from "@/_redux/store";
import { useTable } from "@/_tables/useTable";

export const useWorkShiftsTable = () => {
  const {
    state: { globalFilter, sorting, columnFilters, pagination },
    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
  } = useTable();

  useEffect(() => { onSubmit(); }, [pagination]);

  const dispatch = useDispatch<AppDispatch>();

  const workShits = useSelector(selectWorkShifts);
  const rowCount = useSelector(selectTotalCount);
  const status = useSelector(selectStatus);
  const isLoading = () => status === "loading";

  const onSubmit = () => dispatch(getWorkShifts({
    id: columnFilters.find(f => f.id === "id")?.value as number || undefined,
    checkIn: columnFilters.find(f => f.id === "checkIn")?.value as string || undefined,
    checkOut: columnFilters.find(f => f.id === "checkOut")?.value as string || undefined,
    checkInToleranceMinutes: columnFilters.find(f => f.id === "checkInToleranceMinutes")?.value as number || undefined,
    checkOutToleranceMinutes: columnFilters.find(f => f.id === "checkOutToleranceMinutes")?.value as number || undefined,
    search: globalFilter || undefined,
    pageIndex: pagination.pageIndex,
    pageSize: pagination.pageSize,
    idDescOrderSort: sorting.find(s => s.id === "id") ? sorting.find(s => s.id === "id")?.desc : undefined,
    checkInDescOrderSort: sorting.find(s => s.id === "checkIn") ? sorting.find(s => s.id === "checkIn")?.desc : undefined,
    checkOutDescOrderSort: sorting.find(s => s.id === "checkOut") ? sorting.find(s => s.id === "checkOut")?.desc : undefined,
  }));

  const toCreate = "work-shift";
  const toEdit = "work-shift";
  const handleDelete = useMemo(() => (id: number) => dispatch(deleteWorkShift(id)), [dispatch]);

  return {
    workShits,
    rowCount,
    isLoading,
    onSubmit,
    toCreate,
    toEdit,
    handleDelete,

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
