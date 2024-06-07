"use client";

import { useEffect, useMemo } from "react";
import { useDispatch, useSelector } from "react-redux";

import { selectStatus, selectTotalCount, selectCompanies } from "@/_redux/features/company/slice";
import { deleteCompany, getCompanies } from "@/_redux/features/company/thunks";
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
    publicId: columnFilters.find(f => f.id === "publicId")?.value as string || undefined,
    name: columnFilters.find(f => f.id === "name")?.value as string || undefined,
    tradeName: columnFilters.find(f => f.id === "tradeName")?.value as string || undefined,
    cnpj: columnFilters.find(f => f.id === "cnpj")?.value as string || undefined,
    search: globalFilter || undefined,
    pageIndex: pagination.pageIndex,
    pageSize: pagination.pageSize,
    nameDescOrderSort: sorting.find(s => s.id === "name") ? sorting.find(s => s.id === "name")?.desc : undefined,
    tradeNameDescOrderSort: sorting.find(s => s.id === "tradeName") ? sorting.find(s => s.id === "tradeName")?.desc : undefined,
    cnpjDescOrderSort: sorting.find(s => s.id === "cnpj") ? sorting.find(s => s.id === "cnpj")?.desc : undefined,
  }));

  const toCreate = "company";
  const toEdit = "company";
  const handleDelete = useMemo(() => (id: string) => dispatch(deleteCompany(id)), [dispatch]);

  return {
    companies,
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
