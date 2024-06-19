"use client";

import { AuthWrapper } from "@/app/auth-wrapper";
import { getBaseUserRoles } from "@/globalSettings";

export default function Layout({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <AuthWrapper authorizedRoles={getBaseUserRoles()}>
      {children}
    </AuthWrapper>
  );
}
