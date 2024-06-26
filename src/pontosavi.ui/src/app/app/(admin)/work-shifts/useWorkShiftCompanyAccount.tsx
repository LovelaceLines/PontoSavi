"use client";

import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";

import { useModal, useSnackbar } from "@/_contexts";
import { selectStatus as selectCompanyStatus, selectError as selectCompanyError } from "@/_redux/features/company/slice";
import { deleteRemoveWorkShift as deleteCompanyFromWorkShift, postAddWorkShift as postCompanyToWorkShift } from "@/_redux/features/company/thunks";
import { selectStatus as selectUserStatus, selectError as selectUserError, selectUsers } from "@/_redux/features/user/slice";
import { deleteWorkShift as deleteUserFromWorkShift, getUsers, postAddWorkShift as postUserToWorkShift } from "@/_redux/features/user/thunks";
import { AppDispatch } from "@/_redux/store";
import { workShift } from "@/_types";

export const useWorkShiftCompanyAccount = ({ row }: { row: workShift }) => {
  const { handleModalOpen } = useModal();
  const { Snackbar } = useSnackbar();

  const dispatch = useDispatch<AppDispatch>();

  const companyStatus = useSelector(selectCompanyStatus);
  const companyError = useSelector(selectCompanyError);

  const userStatus = useSelector(selectUserStatus);
  const userError = useSelector(selectUserError);

  const allUsers = useSelector(selectUsers);
  const [selectedUserId, setSelectedUserId] = useState<number | undefined>(row.user?.id ?? undefined);

  useEffect(() => { if (companyStatus === "succeeded") Snackbar("Company added to Work Shift successfully"); }, [companyStatus]);
  useEffect(() => { if (userStatus === "succeeded") Snackbar("User added to Work Shift successfully"); }, [userStatus]);

  useEffect(() => { if (userError) Snackbar(userError); }, [userError]);
  useEffect(() => { if (companyError) Snackbar(companyError); }, [companyError]);

  useEffect(() => { dispatch(getUsers({ nameDescOrderSort: false })); }, []);

  const handleDeleteCompanyFromWorkShift = () => dispatch(deleteCompanyFromWorkShift({ workShiftId: row.id, tenantId: row.company?.id }));
  const handlePostCompanyToWorkShift = () => dispatch(postCompanyToWorkShift({ workShiftId: row.id, tenantId: row.company?.id }));

  const handleDeleteUserFromWorkShift = () => dispatch(deleteUserFromWorkShift({ workShiftId: row.id, userId: selectedUserId }));
  const handlePostUserToWorkShift = () => dispatch(postUserToWorkShift({ workShiftId: row.id, userId: selectedUserId }));

  const MODAL_ID = `add-user-to-work-shift-${row.id}-${row.user?.id}`;
  const handleHandleModalOpen = () => handleModalOpen(MODAL_ID);

  return {
    allUsers,
    selectedUserId,
    setSelectedUserId,

    handleDeleteCompanyFromWorkShift,
    handlePostCompanyToWorkShift,
    handleDeleteUserFromWorkShift,
    handlePostUserToWorkShift,

    MODAL_ID,
    handleHandleModalOpen,
  };
};
