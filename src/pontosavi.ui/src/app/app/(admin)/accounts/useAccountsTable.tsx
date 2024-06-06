"use client";

import { useEffect, useMemo } from "react";
import { useDispatch, useSelector } from "react-redux";

import { selectStatus, selectTotalCount, selectUsers } from "@/_redux/features/user/slice";
import { deleteUser, getUsers } from "@/_redux/features/user/thunks";
import { AppDispatch } from "@/_redux/store";
import { useTable } from "@/_tables/useTable";

export const useAccountsTable = () => {
  const {
    state: { globalFilter, sorting, columnFilters, pagination },
    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
  } = useTable();

  useEffect(() => { onSubmit(); }, [pagination]);

  const dispatch = useDispatch<AppDispatch>();

  const users = useSelector(selectUsers);
  const rowCount = useSelector(selectTotalCount);
  const status = useSelector(selectStatus);
  const isLoading = () => status === "loading";

  const onSubmit = () => dispatch(getUsers({
    publicId: columnFilters.find(f => f.id === "publicId")?.value as string || undefined,
    email: columnFilters.find(f => f.id === "email")?.value as string || undefined,
    role: columnFilters.find(f => f.id === "roles")?.value as string || undefined,
    phoneNumber: columnFilters.find(f => f.id === "phoneNumber")?.value as string || undefined,
    name: columnFilters.find(f => f.id === "name")?.value as string || undefined,
    userName: columnFilters.find(f => f.id === "userName")?.value as string || undefined,
    search: globalFilter || undefined,
    pageIndex: pagination.pageIndex,
    pageSize: pagination.pageSize,
    emailOrderSort: sorting.find(s => s.id === "email") ? sorting.find(s => s.id === "email")?.desc ? "desc" : "asc" : undefined,
    userNameOrderSort: sorting.find(s => s.id === "userName") ? sorting.find(s => s.id === "userName")?.desc ? "desc" : "asc" : undefined,
  }));

  const toCreate = "account";
  const handleDelete = useMemo(() => (id: string) => dispatch(deleteUser(id)), [dispatch]);

  return {
    users,
    rowCount,
    isLoading,
    onSubmit,
    toCreate,
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
