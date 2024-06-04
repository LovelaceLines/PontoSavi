import { z } from "zod";

export const formDataSchema = z.object({
  oldPassword: z.string().min(6, { message: "Old password must be at least 6 characters" }),
  newPassword: z.string().min(6, { message: "New password must be at least 6 characters" }),
  confirmPassword: z.string().min(6, { message: "Confirm password must be at least 6 characters" })
})
  .refine(data => data.oldPassword !== data.newPassword, { path: ["newPassword"], message: "New password must be different from the old password" })
  .refine(data => data.newPassword === data.confirmPassword, { path: ["confirmPassword"], message: "Confirm password must match new password" });

export type Schema = z.infer<typeof formDataSchema>;
