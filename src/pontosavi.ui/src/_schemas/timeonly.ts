/* eslint-disable no-unused-vars */

import { z } from "zod";

export const transformTimeonly = (v: string): string => {
  const hourMinuteSecond = v.split(".")[0];
  const [hour, minute, _] = hourMinuteSecond.split(":").map(v => parseInt(v));

  if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
    throw new Error("Invalid time format");

  return `${hour.toString().padStart(2, "0")}:${minute.toString().padStart(2, "0")}:00`;
};

export const timeonlySchema = z.string().
  transform(v => transformTimeonly(v));

export type Schema = z.infer<typeof timeonlySchema>;
