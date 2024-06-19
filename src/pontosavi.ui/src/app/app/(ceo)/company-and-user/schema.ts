import { z } from "zod";

import { companyFormDataSchema, userFormDataSchema } from "@/_schemas";

export const formDataSchema = z.object({
  user: userFormDataSchema,
  company: companyFormDataSchema,
});

export type Schema = z.infer<typeof formDataSchema>;
