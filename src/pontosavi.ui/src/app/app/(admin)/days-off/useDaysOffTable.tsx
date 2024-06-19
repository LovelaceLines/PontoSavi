"use client";

import { useEffect, useMemo } from "react";
import { useDispatch, useSelector } from "react-redux";

import { selectStatus, selectTotalCount, selectDaysOff } from "@/_redux/features/dayOff/slice";
import { deleteDayOff, getDaysOff } from "@/_redux/features/dayOff/thunks";
import { AppDispatch } from "@/_redux/store";
import { useTable } from "@/_tables/useTable";

export const useDaysOffTable = () => {
  const {
    state: { globalFilter, sorting, columnFilters, pagination },
    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
  } = useTable();

  useEffect(() => { onSubmit(); }, [pagination]);

  const dispatch = useDispatch<AppDispatch>();

  const daysOff = useSelector(selectDaysOff);
  const rowCount = useSelector(selectTotalCount);
  const status = useSelector(selectStatus);
  const isLoading = () => status === "loading";

  const onSubmit = () => dispatch(getDaysOff({
    id: columnFilters.find(f => f.id === "id")?.value as number || undefined,
    date: columnFilters.find(f => f.id === "date")?.value as Date || undefined,
    description: columnFilters.find(f => f.id === "description")?.value as string || undefined,
    search: globalFilter || undefined,
    pageIndex: pagination.pageIndex,
    pageSize: pagination.pageSize,
    idDescOrderSort: sorting.find(s => s.id === "id") ? sorting.find(s => s.id === "id")?.desc : undefined,
    dateDescOrderSort: sorting.find(s => s.id === "date") ? sorting.find(s => s.id === "date")?.desc : undefined,
  }));

  const toCreate = "day-off";
  const toEdit = "day-off";
  const handleDelete = useMemo(() => (id: number) => dispatch(deleteDayOff(id)), [dispatch]);

  return {
    daysOff,
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
