import { mkConfig, generateCsv, download } from "export-to-csv";

export const exportDataToCSV = (filename: string, head: string[], body: string[][]) => {
  const csvConfig = mkConfig({
    filename: `${filename.toLowerCase()}-${new Date().toISOString()}`,
    fieldSeparator: ",",
    decimalSeparator: ".",
    useKeysAsHeaders: true,
  });

  const data: { [k: string]: any;[k: number]: any; }[] =
    body.map(row => head.reduce((acc, key, i) => ({ ...acc, [key]: row[i] }), {}));

  const csv = generateCsv(csvConfig)(data);
  download(csvConfig)(csv);
};
