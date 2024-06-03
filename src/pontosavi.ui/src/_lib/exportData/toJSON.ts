import exportFromJSON from "export-from-json";

export const exportDataToJSON = (filename: string, head: string[], body: string[][]) => {
  filename = `${filename.toLowerCase()}-${new Date().toISOString()}`;
  const data = body.map(row => head.reduce((acc, key, i) => ({ ...acc, [key]: row[i] }), {}));

  exportFromJSON({ data, fileName: filename, exportType: "json" });
};