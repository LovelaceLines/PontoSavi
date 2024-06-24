"use client";

import { Store } from "@mui/icons-material";
import { Button } from "@mui/material";

import { Menu } from "@/_components";
import { point } from "@/_types";
import { usePointActions } from "./usePointActions";

export const Actions = ({ row }: { row: point }) => {
  const {
    handleApprove,
    handleReject,
  } = usePointActions({ row });

  return (
    <Menu items={[
      <Button
        key="approve-point"
        size="small"
        startIcon={<Store />}
        onClick={() => handleApprove()}
      >
        Approve Point
      </Button>,
      <Button
        key="reject-point"
        size="small"
        startIcon={<Store />}
        onClick={() => handleReject()}
      >
        Reject Point
      </Button>,
    ]} />
  );
};
