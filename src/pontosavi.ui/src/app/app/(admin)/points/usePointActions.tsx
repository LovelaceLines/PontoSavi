"use client";

import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { useSnackbar } from "@/_contexts";
import { selectError, selectStatus } from "@/_redux/features/point/slice";
import { postPointApprove, postPointReject } from "@/_redux/features/point/thunks";
import { AppDispatch } from "@/_redux/store";
import { point } from "@/_types";

export const usePointActions = ({ row }: { row: point }) => {
  const dispatch = useDispatch<AppDispatch>();
  const { Snackbar } = useSnackbar();

  const status = useSelector(selectStatus);
  const error = useSelector(selectError);

  useEffect(() => { if (status === "succeeded") Snackbar("Success"); }, [status]);

  useEffect(() => { if (error) Snackbar(error); }, [error]);

  const handleApprove = () => dispatch(postPointApprove(row.id ?? 0));

  const handleReject = () => dispatch(postPointReject(row.id ?? 0));

  return {
    handleApprove,
    handleReject,
  };
};
