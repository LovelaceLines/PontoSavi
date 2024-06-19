/* eslint-disable no-unused-vars */

import { z } from "zod";

export const transformDateTime = (v: string): string => {
  const [yyyymmdd, hhmmss] = v.split("T");
  const [yyyy, mm, dd, _] = yyyymmdd.split("-").map(v => parseInt(v));
  const [hh, mi, ss] = hhmmss.split(":").map(v => parseInt(v));

  if (yyyy < 1900 || yyyy > 2100 || mm < 1 || mm > 12 || dd < 1 || dd > 31 || hh < 0 || hh > 23 || mi < 0 || mi > 59 || ss < 0 || ss > 59) throw new Error("Invalid date");

  return `${yyyy}-${mm.toString().padStart(2, "0")}-${dd.toString().padStart(2, "0")}T${hh.toString().padStart(2, "0")}:${mi.toString().padStart(2, "0")}:00`;
};

export const dateTimeSchema = z.string().
  transform(v => transformDateTime(v));

export type Schema = z.infer<typeof dateTimeSchema>;
