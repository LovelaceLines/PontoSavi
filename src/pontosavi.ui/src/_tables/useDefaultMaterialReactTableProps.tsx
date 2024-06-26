/* eslint-disable no-unused-vars */

"use client";

import { MRT_ColumnFiltersState, MRT_PaginationState, MRT_RowSelectionState, MRT_ShowHideColumnsButton, MRT_SortingState, MRT_TableInstance, MRT_TableState, MRT_ToggleDensePaddingButton, MRT_ToggleFiltersButton, MRT_ToggleFullScreenButton, MRT_ToggleGlobalFilterButton, MRT_VisibilityState, useMaterialReactTable, type MRT_ColumnDef, type MRT_RowData, type MRT_TableOptions, } from "material-react-table";
import { Add, ClearAll, Delete, Edit, FileDownload, Share } from "@mui/icons-material";
import { Box, Button, IconButton, Tooltip } from "@mui/material";
import Link from "next/link";
import React, { Dispatch, SetStateAction, useCallback, useState } from "react";

import { useSnackbar, useModal } from "@/_contexts";
import { getStorageValue, setStorageValue } from "@/_services";
import { useThemeContext } from "@/_theme";

export interface Props<TData extends MRT_RowData> extends MRT_TableOptions<TData> {
  columns: MRT_ColumnDef<TData>[];
  data: TData[];
  setGlobalFilter?: Dispatch<SetStateAction<string>>;
  setColumnFilters?: Dispatch<SetStateAction<MRT_ColumnFiltersState>>;
  setSorting?: Dispatch<SetStateAction<MRT_SortingState>>;
  setPagination?: Dispatch<SetStateAction<MRT_PaginationState>>;
  rowCount?: number;
  initialSatate?: Partial<MRT_TableState<TData>>;
  state?: Partial<MRT_TableState<TData>>;
  enableRowSelection?: boolean;
  onSubmit?: () => void;
  isLoading?: () => boolean;
  title: string;
  toCreate?: string;
  toEdit?: string;
  handleDelete?: (id: number) => void;
}

export const useDefaultMaterialReactTableProps = <TData extends MRT_RowData>(
  { columns, data, ...props }: Props<TData>
) => {

  const [columnVisibility, setColumnVisibility] = useState<MRT_VisibilityState>(getStorageValue(`mrt-column-visibility-${props.title}`, {}));

  const handleColumnVisibility: Dispatch<SetStateAction<MRT_VisibilityState>> = useCallback((value: SetStateAction<MRT_VisibilityState>) => {
    setStorageValue(`mrt-column-visibility-${props.title}`, (value as (state: MRT_VisibilityState) => MRT_VisibilityState)(columnVisibility));
    return setColumnVisibility(value);
  }, [columnVisibility]);

  const [rowSelection, setRowSelection] = useState<MRT_RowSelectionState>({});

  const { Snackbar } = useSnackbar();
  const { handleModalOpen, isOpen } = useModal();

  const handleShare = useCallback(() => {
    const url = typeof window === "undefined" ? "" : `${window.location.href}`;

    navigator.clipboard.writeText(url)
      .then(() => Snackbar("Link copied to clipboard"));
  }, []);

  const handleClearFilters = useCallback(() => {
    props.setGlobalFilter && props.setGlobalFilter("");
    props.setColumnFilters && props.setColumnFilters([]);
    props.setSorting && props.setSorting([]);
  }, []);

  const handleDownloadExportRows = useCallback(() => {
    handleModalOpen("download-export-display");
  }, []);

  const handleDelete = useCallback(async () => {
    if (!Object.keys(rowSelection).length) {
      Snackbar("Select a row to delete");
      return;
    }

    const id = parseInt(Object.keys(rowSelection)[0] ?? 0);
    props.handleDelete && await props.handleDelete(id);
    props.onSubmit && props.onSubmit();
    Snackbar("Record deleted!");
  }, [rowSelection]);

  const renderTopToolbarCustomActions = ({ table }: { table: MRT_TableInstance<TData> }) => (
    <Box display="flex" alignItems="center" gap={1}>
      {props.onSubmit && <Button key="run" variant="contained" size="small" onClick={props.onSubmit}>Run</Button>}
      {props.toCreate && <Link href={props.toCreate}><Button key="create" variant="outlined" size="small" endIcon={<Add />}>Create</Button></Link>}
      {props.toEdit && <Link href={`${props.toEdit}/${Object.keys(rowSelection)[0] ?? ""}`}><Button key="edit" variant="outlined" size="small" endIcon={<Edit />}>Edit</Button></Link>}
      {props.handleDelete && <Button key="delete" variant="outlined" size="small" endIcon={<Delete />} onClick={handleDelete}>Delete</Button>}
    </Box>
  );

  const renderToolbarInternalActions = ({ table }: { table: MRT_TableInstance<TData> }) => [
    <MRT_ToggleGlobalFilterButton key="globalFilter" table={table} />,
    <Tooltip key="Share" title="Share"><IconButton key="share" size="medium" aria-label="teste" onClick={handleShare}><Share /></IconButton></Tooltip>,
    <Tooltip key="Clear Filters" title="Clear Filters"><IconButton key="clear-filters" size="medium" onClick={handleClearFilters}><ClearAll /></IconButton></Tooltip>,
    <Tooltip key="Export" title="Export"><IconButton key="export" size="medium" onClick={handleDownloadExportRows}><FileDownload /></IconButton></Tooltip>,
    <MRT_ToggleFiltersButton key="toggleFilters" table={table} />,
    <MRT_ShowHideColumnsButton key="showHideColumns" table={table} />,
    <MRT_ToggleDensePaddingButton key="toggleDensePadding" table={table} />,
    <MRT_ToggleFullScreenButton key="toggleFullScreen" table={table} />,
  ];

  return ({
    columnVisibility,
    handleColumnVisibility,
    rowSelection,
    setRowSelection,

    isOpen,

    renderTopToolbarCustomActions,
    renderToolbarInternalActions,
  });
};
