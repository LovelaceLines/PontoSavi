"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";
import { useEffect, useState } from "react";

import { Loading } from "@/_components";
import { useDispatch } from "react-redux";
import { AppDispatch } from "@/_redux/store";
import { getCompanyByPublicId } from "@/_redux/features/company/thunks";
import { company } from "@/_types";

const CompanyForm = dynamic(() => import("./companyForm").then(mod => mod.CompanyForm),
  { ssr: false, loading: () => <Loading /> });

export default function Page({ params }: { params: { id?: string } }) {
  const dispatch = useDispatch<AppDispatch>();
  const [company, setRole] = useState<company | undefined>(undefined);

  useEffect(() => {
    if (!params.id) return;

    dispatch(getCompanyByPublicId(params.id))
      .then(action => {
        if (getCompanyByPublicId.fulfilled.match(action)) {
          setRole(action.payload);
        }
      });
  }, [params.id]);

  return (
    <>
      <Typography variant="h5" mb={2}>Company</Typography>
      <CompanyForm company={company} />
    </>
  );
}
