import { z } from "zod";

export const formDataSchema = z.object({
  publicId: z.string().optional(),
  name: z.string().min(3, { message: "Name must be at least 3 characters" }),
  userName: z.string().min(3, { message: "Username must be at least 3 characters" }),
  email: z.string().email({ message: "Invalid email" }),
  phoneNumber: z.string().min(8, { message: "Phone number must be at least 8 characters" }),
  password: z.string().min(6, { message: "Password must be at least 6 characters" }),
  confirmPassword: z.string().min(6, { message: "Confirm password must be at least 6 characters" }),
  roles: z.array(z.string()),
})
  .refine(data => data.password ? data.password === data.confirmPassword : true, { path: ["confirmPassword"], message: "Confirm password must match password" });

export type Schema = z.infer<typeof formDataSchema>;
