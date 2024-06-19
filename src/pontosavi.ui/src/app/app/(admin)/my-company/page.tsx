"use client";

import { Typography } from "@mui/material";
import dynamic from "next/dynamic";
import { useEffect } from "react";
import { useSelector } from "react-redux";

import { Loading } from "@/_components";
import { useDispatch } from "react-redux";
import { AppDispatch } from "@/_redux/store";
import { getCompany } from "@/_redux/features/company/thunks";
import { selectCompany } from "@/_redux/features/company/slice";

const CompanyForm = dynamic(() => import("./companyForm").then(mod => mod.CompanyForm),
  { ssr: false, loading: () => <Loading /> });

export default function Page() {
  const dispatch = useDispatch<AppDispatch>();
  const currentCompany = useSelector(selectCompany);

  useEffect(() => {
    dispatch(getCompany());
  }, []);

  if (currentCompany === null) return <Loading />;

  return (
    <>
      <Typography variant="h5" mb={2}>My Company</Typography>
      <CompanyForm company={currentCompany} />
    </>
  );
}
