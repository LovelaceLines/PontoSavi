import { z } from "zod";

const envSchema = z.object({
  NODE_ENV: z.string().default(process.env.NODE_ENV!),
  APP_URL: z.string().url().default(process.env.NEXT_PUBLIC_APP_URL!),
  API_URL: z.string().url().default(process.env.NEXT_PUBLIC_API_URL!),
});

export default envSchema.parse(process.env);
