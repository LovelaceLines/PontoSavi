"use client";

import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { selectStatus, selectTotalCount, selectCompanies } from "@/_redux/features/ceo/slice";
import { getCompanies } from "@/_redux/features/ceo/thunks";
import { AppDispatch } from "@/_redux/store";
import { useTable } from "@/_tables/useTable";

export const useCompaniesTable = () => {
  const {
    state: { globalFilter, sorting, columnFilters, pagination },
    setGlobalFilter,
    setColumnFilters,
    setSorting,
    setPagination,
  } = useTable();

  useEffect(() => { onSubmit(); }, [pagination]);

  const dispatch = useDispatch<AppDispatch>();

  const companies = useSelector(selectCompanies);
  const rowCount = useSelector(selectTotalCount);
  const status = useSelector(selectStatus);
  const isLoading = () => status === "loading";

  const onSubmit = () => dispatch(getCompanies({
    id: columnFilters.find(f => f.id === "id")?.value as number || undefined,
    name: columnFilters.find(f => f.id === "name")?.value as string || undefined,
    tradeName: columnFilters.find(f => f.id === "tradeName")?.value as string || undefined,
    cnpj: columnFilters.find(f => f.id === "cnpj")?.value as string || undefined,
    search: globalFilter || undefined,
    pageIndex: pagination.pageIndex,
    pageSize: pagination.pageSize,
    idDescOrderSort: sorting.find(s => s.id === "id") ? sorting.find(s => s.id === "id")?.desc : undefined,
    nameDescOrderSort: sorting.find(s => s.id === "name") ? sorting.find(s => s.id === "name")?.desc : undefined,
    tradeNameDescOrderSort: sorting.find(s => s.id === "tradeName") ? sorting.find(s => s.id === "tradeName")?.desc : undefined,
    cnpjDescOrderSort: sorting.find(s => s.id === "cnpj") ? sorting.find(s => s.id === "cnpj")?.desc : undefined,
  }));

  const toCreate = "company-and-user";

  return {
    companies,
    rowCount,
    isLoading,
    onSubmit,
    toCreate,

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
