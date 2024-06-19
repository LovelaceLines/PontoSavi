import { z } from "zod";

export const formDataSchema = z.object({
  id: z.number().optional(),
  name: z.string().min(3, { message: "Name must be at least 3 characters" }),
});

export type Schema = z.infer<typeof formDataSchema>;
