/* eslint-disable no-unused-vars */
import { z } from "zod";

export const transformDateonly = (v: string): string => {
  const yyyymmdd = v.split("T")[0];
  const [yyyy, mm, dd, _] = yyyymmdd.split("-").map(v => parseInt(v));

  if (yyyy < 1900 || yyyy > 2100 || mm < 1 || mm > 12 || dd < 1 || dd > 31)
    throw new Error("Invalid date");

  return `${yyyy}-${mm.toString().padStart(2, "0")}-${dd.toString().padStart(2, "0")}`;
};

export const dateonlySchema = z.string()
  .transform(v => transformDateonly(v));

export type Schema = z.infer<typeof dateonlySchema>;
