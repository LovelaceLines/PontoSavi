import { z } from "zod";

import { dateonlySchema } from "@/_schemas";

export const formDataSchema = z.object({
  id: z.number().optional(),
  companyId: z.number().optional(),
  date: dateonlySchema,
  description: z.string().optional(),
});

export type Schema = z.infer<typeof formDataSchema>;
