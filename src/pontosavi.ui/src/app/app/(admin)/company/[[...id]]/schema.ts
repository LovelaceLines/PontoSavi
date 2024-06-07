import { z } from "zod";

export const formDataSchema = z.object({
  publicId: z.string().optional(),
  name: z.string().min(3, { message: "Name must be at least 3 characters" }),
  tradeName: z.string().min(3, { message: "Trade name must be at least 3 characters" }),
  cnpj: z.string().length(14, { message: "CNPJ must have 14 characters" })
});

export type Schema = z.infer<typeof formDataSchema>;
