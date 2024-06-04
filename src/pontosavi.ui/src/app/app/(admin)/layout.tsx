"use client";

import { AuthWrapper } from "@/app/auth-wrapper";
import { getSuperUserRoles } from "@/globalSettings";

export default function Layout({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <AuthWrapper authorizedRoles={getSuperUserRoles()}>
      {children}
    </AuthWrapper>
  );
}
