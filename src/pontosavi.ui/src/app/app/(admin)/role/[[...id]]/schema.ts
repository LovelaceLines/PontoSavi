import { z } from "zod";

export const formDataSchema = z.object({
  id: z.string().optional(),
  name: z.string().min(3, { message: "Name must be at least 3 characters" }),
});

export type Schema = z.infer<typeof formDataSchema>;
