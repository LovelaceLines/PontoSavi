import { z } from "zod";

import { timeonlySchema } from "@/_schemas";

export const formDataSchema = z.object({
  id: z.number().optional(),
  checkIn: timeonlySchema,
  checkInToleranceMinutes: z.any().transform(v => parseInt(v)).pipe(z.number().int().min(0, { message: "Check-in tolerance must be a positive number" })),
  checkOut: timeonlySchema,
  checkOutToleranceMinutes: z.any().transform(v => parseInt(v)).pipe(z.number().int().min(0, { message: "Check-out tolerance must be a positive number" })),
  description: z.string().optional(),
});

export type Schema = z.infer<typeof formDataSchema>; 
