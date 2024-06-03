import { exportmeExcel } from "excel-ent";

export const exportDataToXLSX = (fileName: string, head: string[], body: string[][]) => {
  fileName = `${fileName.toLowerCase()}-${new Date().toISOString()}`;
  const data = body.map(row => head.reduce((acc, key, i) => ({ ...acc, [key]: row[i] }), {}));

  exportmeExcel({
    data,
    fileName,
    exportAs: {
      type: "download",
    },
    options: {
      columnWidths: head.map(() => 40),
      headerStyle: {
        fill: {
          fgColor: {
            rgb: "0a1c3e",
          },
        },
        font: {
          bold: true,
          color: {
            rgb: "ffffff",
          },
        },
        alignment: {
          vertical: "center",
          horizontal: "center",
        },
      },
      bodyStyle: {
        font: {
          name: "sans-serif",
        },
        alignment: {
          vertical: "center",
          horizontal: "center",
        },
      },
      stripedRows: true,
    },
  });
};