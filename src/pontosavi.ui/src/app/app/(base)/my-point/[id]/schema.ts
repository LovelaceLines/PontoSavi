import { z } from "zod";

import { dateTimeSchema } from "@/_schemas";

export const formDataSchema = z.object({
  id: z.number().optional(),
  checkIn: dateTimeSchema,
  checkInDescription: z.any().transform(v => v === null ? undefined : v as string).pipe(z.string().optional()),
  checkOut: dateTimeSchema.optional(),
  checkOutDescription: z.any().transform(v => v === null ? undefined : v as string).pipe(z.string().optional()),
});

export type Schema = z.infer<typeof formDataSchema>;
