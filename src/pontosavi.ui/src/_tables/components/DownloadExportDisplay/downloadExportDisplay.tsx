"use client";

import { Box, Button, Divider, FormControl, FormControlLabel, Radio, RadioGroup, Typography } from "@mui/material";
import { useState } from "react";

import { Modal } from "@/_components";
import { exportDataToCSV, exportDataToJSON, exportDataToPDF, exportDataToXLSX } from "@/_lib";

interface DownloadExportDisplayProps {
  fileName?: string;
  head: { id: string, value: string }[];
  allRows: { [key: string]: any }[];
  selectedRows: { [key: string]: any }[];
}

const getValue = (obj: any, path: string) =>
  path.split(".").reduce((acc, part) => acc && acc[part], obj);

export const DownloadExportDisplay = (props: DownloadExportDisplayProps) => {
  const fileName = props.fileName ?? "table";
  const headId: string[] = props.head.map(({ id }) => id);
  const headValue: string[] = props.head.map(({ value }) => value);
  const allRows: string[][] = props.allRows.map(row => props.head.map(({ id }) => getValue(row, id)));
  const selectedRows: string[][] = props.selectedRows.map(row => props.head.map(({ id }) => getValue(row, id)));

  const [rowsMode, setRowsMode] = useState<"selected" | "all">("all");

  const handleRowsModeChange = (event: React.ChangeEvent<HTMLInputElement>) =>
    setRowsMode(event.target.value as "selected" | "all");

  const handleFormat = (format: "JSON" | "CSV" | "XLSX" | "PDF") => {
    const rows = rowsMode === "all" ? allRows : selectedRows;
    switch (format) {
      case "JSON": exportDataToJSON(fileName, headId, rows); break;
      case "CSV": exportDataToCSV(fileName, headId, rows); break;
      case "XLSX": exportDataToXLSX(fileName, headValue, rows); break;
      case "PDF": exportDataToPDF(fileName, headValue, rows); break;
    }
  };

  return (
    <Modal initOpen={false} id="download-export-display">
      <>
        <Typography variant="h6" align="center">Download/Export</Typography>

        <Divider />

        <Typography variant="subtitle1">Export rows:</Typography>
        <FormControl>
          <RadioGroup row value={rowsMode} onChange={handleRowsModeChange}>
            <FormControlLabel value="all" control={<Radio />} label="All rows" />
            <FormControlLabel value="selected" control={<Radio />} label="Only selected rows" />
          </RadioGroup>
        </FormControl>

        <Divider />

        <Typography variant="subtitle1">Export format:</Typography>
        <Box display="flex" gap={1}>
          <Button variant="contained" size="small" onClick={() => handleFormat("JSON")}>JSON</Button>
          <Button variant="contained" size="small" onClick={() => handleFormat("CSV")}>CSV</Button>
          <Button variant="contained" size="small" onClick={() => handleFormat("XLSX")}>XLSX</Button>
          <Button variant="contained" size="small" onClick={() => handleFormat("PDF")}>PDF</Button>
        </Box>
      </>
    </Modal>
  );
};