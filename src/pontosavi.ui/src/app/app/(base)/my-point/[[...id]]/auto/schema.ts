import { z } from "zod";

export const formDataSchema = z.object({
  description: z.string().optional(),
});

export type Schema = z.infer<typeof formDataSchema>;
