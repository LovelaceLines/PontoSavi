import { jsPDF } from "jspdf";
import autoTable from "jspdf-autotable";

export const exportDataToPDF = (filename: string, head: string[], body: string[][]) => {
  const doc = new jsPDF({
    orientation: "landscape",
  });

  autoTable(doc, {
    head: [head],
    body: body,
    theme: "grid",
  });

  doc.save(`${filename.toLowerCase()}-${new Date().toISOString()}.pdf`);
};