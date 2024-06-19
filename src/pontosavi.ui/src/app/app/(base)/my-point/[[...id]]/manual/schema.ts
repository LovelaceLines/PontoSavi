import { z } from "zod";

import { dateTimeSchema } from "@/_schemas";

export const formDataSchema = z.object({
  checkIn: dateTimeSchema,
  checkInDescription: z.string().optional(),
  checkOut: dateTimeSchema.optional(),
  checkOutDescription: z.string().optional(),
});

export type Schema = z.infer<typeof formDataSchema>;
