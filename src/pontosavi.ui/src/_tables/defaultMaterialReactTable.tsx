/* eslint-disable no-unused-vars */

"use client";

import { MaterialReactTable, useMaterialReactTable, type MRT_RowData } from "material-react-table";

import { DownloadExportDisplay } from "./components/DownloadExportDisplay/downloadExportDisplay";
import { colors, useThemeContext } from "@/_theme";
import { Props, useDefaultMaterialReactTableProps } from "./useDefaultMaterialReactTableProps";

export const useDefaultMaterialReactTable = <TData extends MRT_RowData>(
  { columns, data, ...props }: Props<TData>
) => {

  const {
    columnVisibility,
    handleColumnVisibility,
    rowSelection,
    setRowSelection,

    isOpen,

    renderTopToolbarCustomActions,
    renderToolbarInternalActions,
  } = useDefaultMaterialReactTableProps({ columns, data, ...props });

  const { themeName } = useThemeContext();

  const table = useMaterialReactTable({
    columns,
    data,
    ...props,

    renderTopToolbarCustomActions: props.renderTopToolbarCustomActions ?? renderTopToolbarCustomActions,
    renderToolbarInternalActions: props.renderToolbarInternalActions ?? renderToolbarInternalActions,

    //#region setStates

    onGlobalFilterChange: props.setGlobalFilter,

    manualFiltering: props.setColumnFilters ? true : false,
    onColumnFiltersChange: props.setColumnFilters,

    manualSorting: props.setSorting ? true : false,
    onSortingChange: props.setSorting,

    manualPagination: props.setPagination ? true : false,
    onPaginationChange: props.setPagination,

    enableRowSelection: props.enableRowSelection ?? true,
    getRowId: row => row.id ?? "",
    onRowSelectionChange: setRowSelection,

    onColumnVisibilityChange: handleColumnVisibility,

    initialState: {
      showColumnFilters: true,
      density: "compact",
      ...props.initialState,
    },

    state: {
      isLoading: props.isLoading ? props.isLoading() : false,
      rowSelection,
      columnVisibility,
      ...props.state,
    },

    //#endregion

    layoutMode: "grid",
    enableColumnResizing: props.enableColumnResizing ?? true,
    positionToolbarAlertBanner: "none",

    rowCount: props.rowCount,

    //#region Styles

    muiTablePaperProps: ({ table }) => ({
      style: {
        zIndex: table.getState().isFullScreen ? 9999 : undefined,
      },
    }),

    muiTopToolbarProps: ({ table }) => ({
      sx: () => ({
        "& .MuiIconButton-root": {
          color: `${themeName === "light" ? colors.black : colors.white}`
        },
      })
    }),

    muiTableProps: ({ table }) => ({
      sx: () => ({
        "& .MuiIconButton-root, .MuiSelect-icon": {
          color: `${themeName === "light" ? colors.black : colors.white}`
        },
      })
    }),

    muiFilterSliderProps: ({ table }) => ({
      sx: () => ({
        "& .MuiIconButton-root, .MuiSelect-icon": {
          color: `${themeName === "light" ? colors.black : colors.white}`
        },
      })
    }),

    muiBottomToolbarProps: ({ table }) => ({
      sx: () => ({
        "& .MuiIconButton-root, .MuiSelect-icon": {
          color: `${themeName === "light" ? colors.black : colors.white}`
        },
      })
    }),

    muiPaginationProps: ({ table }) => ({
      rowsPerPageOptions: [5, 10, 15, 20, 25, 30, 50, 100, 200, 500],
    }),

    //#endregion

  });

  return (
    <>
      <MaterialReactTable table={table} />
      {isOpen("download-export-display") && <DownloadExportDisplay
        fileName={props.title}
        head={table.getVisibleFlatColumns().map(c => c.columnDef).map(c => ({ id: c.id ?? "", value: c.header?.toString() ?? "" })).filter(c => c.id !== "mrt-row-select" && c.id !== "mrt-row-actions")}
        allRows={table.getRowModel().rows.map(r => r.original)}
        selectedRows={table.getSelectedRowModel().rows.map(r => r.original)}
      />}
    </>
  );
};
