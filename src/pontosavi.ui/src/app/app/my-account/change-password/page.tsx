import { Typography } from "@mui/material";
import dynamic from "next/dynamic";

import { Loading } from "@/_components";

const UpdatePasswordForm = dynamic(() => import("./passwordForm").then(mod => mod.UpdatePasswordForm),
  { ssr: false, loading: () => <Loading /> });

export default function Page() {
  return (
    <>
      <Typography variant="h5" mb={2}>Change Password</Typography>
      <UpdatePasswordForm />
    </>
  );
}