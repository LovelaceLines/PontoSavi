"use client";

import { useEffect, useMemo } from "react";
import { useDispatch, useSelector } from "react-redux";

import { selectStatus, selectTotalCount, selectRoles } from "@/_redux/features/role/slice";
import { deleteRole, getRoles } from "@/_redux/features/role/thunks";
import { AppDispatch } from "@/_redux/store";
import { useTable } from "@/_tables/useTable";

export const useRolesTable = () => {
  const {
    state: { globalFilter, sorting, columnFilters, pagination },
    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
  } = useTable();

  useEffect(() => { onSubmit(); }, [pagination]);

  const dispatch = useDispatch<AppDispatch>();

  const roles = useSelector(selectRoles);
  const rowCount = useSelector(selectTotalCount);
  const status = useSelector(selectStatus);
  const isLoading = () => status === "loading";

  const onSubmit = () => dispatch(getRoles({
    id: columnFilters.find(f => f.id === "id")?.value as number || undefined,
    name: columnFilters.find(f => f.id === "name")?.value as string || undefined,
    nameDescOrderSort: sorting.find(s => s.id === "name") ? sorting.find(s => s.id === "name")?.desc : undefined,
  }));

  const toCreate = "role";
  const toEdit = "role";
  const handleDelete = useMemo(() => (id: number) => dispatch(deleteRole(id)), [dispatch]);

  return {
    roles,
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
