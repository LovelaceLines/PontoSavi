"use client";

import { AccountCircle, Store } from "@mui/icons-material";
import { Button, Divider, FormControl, InputLabel, ListItemIcon, MenuItem, Select } from "@mui/material";
import { Dispatch, SetStateAction } from "react";

import { Menu, Modal } from "@/_components";
import { user, workShift } from "@/_types";
import { useWorkShiftCompanyAccount } from "./useWorkShiftCompanyAccount";

export const Actions = ({ row }: { row: workShift }) => {
  const {
    allUsers,
    selectedUserId,
    setSelectedUserId,

    handlePostCompanyToWorkShift,
    handleDeleteCompanyFromWorkShift,
    handlePostUserToWorkShift,
    handleDeleteUserFromWorkShift,

    MODAL_ID,
    handleHandleModalOpen,
  } = useWorkShiftCompanyAccount({ row });

  return (
    <>
      <Menu items={[
        <Button
          key="post-company-to-work-shift"
          size="small"
          startIcon={<Store />}
          disabled={row.company?.id}
          onClick={() => handlePostCompanyToWorkShift()}
        >
          Add To Company
        </Button>,
        <Button
          key="delete-company-from-work-shift"
          size="small"
          startIcon={<Store />}
          disabled={!row.company?.id}
          onClick={() => handleDeleteCompanyFromWorkShift()}
        >
          Remove From Company
        </Button>,
        <Button
          key="post-user-to-work-shift"
          size="small"
          startIcon={<AccountCircle />}
          onClick={() => handleHandleModalOpen()}
        >
          Add/Remove From User
        </Button>,
      ]} />

      <Modal initOpen={false} id={MODAL_ID}>
        <>
          <UserFormControl allUsers={allUsers} selectedUserId={selectedUserId} setSelectedUserId={setSelectedUserId} />

          <Divider />

          <Button
            variant="contained"
            size="small"
            disabled={row.user?.id === selectedUserId}
            onClick={() => handlePostUserToWorkShift()}
          >
            Add
          </Button>
          <Button
            variant="outlined"
            size="small"
            disabled={row.user?.id !== selectedUserId}
            onClick={() => handleDeleteUserFromWorkShift()}
          >
            Remove
          </Button>
        </>
      </Modal>
    </>
  );
};

const UserFormControl = ({ allUsers, selectedUserId, setSelectedUserId }: { allUsers: user[] | null, selectedUserId?: number, setSelectedUserId: Dispatch<SetStateAction<number | undefined>> }) => {
  return (
    <FormControl
      sx={{ width: 300, "& .MuiSvgIcon-root": { color: "inherit" }, "& .MuiSelect-icon": { color: "inherit" } }}
    >
      <InputLabel id="input-modulo">Account</InputLabel>
      <Select defaultValue={selectedUserId} onChange={(event) => setSelectedUserId(event.target.value as number)}>
        <MenuItem value="" disabled>Accounts</MenuItem>
        {allUsers?.map((user, index) =>
          <MenuItem key={index + user.name} value={user.id}>
            <ListItemIcon sx={{ color: "inherit" }}>
              <AccountCircle fontSize="small" />
            </ListItemIcon>
            {user.name}
          </MenuItem>
        )}
      </Select>
    </FormControl>
  );
};