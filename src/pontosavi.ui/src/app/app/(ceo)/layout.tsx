"use client";

import { AuthWrapper } from "@/app/auth-wrapper";
import { getCEOUserRoles } from "@/globalSettings";

export default function Layout({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <AuthWrapper authorizedRoles={getCEOUserRoles()}>
      {children}
    </AuthWrapper>
  );
}
