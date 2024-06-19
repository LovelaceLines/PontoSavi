import { createAsyncThunk } from "@reduxjs/toolkit";

import { getUserDefaultRole } from "@/globalSettings";
import { Axios } from "@/_http/axios";
import { queryResult, updatedPassword, user, userFilter, userRole, userWorkShift } from "@/_types";

const USER = "/User";
const USER_PASSWORD = "/User/password";
const USER_ADD_TO_ROLE = "/User/add-to-role";
const USER_REMOVE_FROM_ROLE = "/User/remove-from-role";
const USER_ADD_WORK_SHIFT = "/User/add-work-shift";
const USER_REMOVE_WORK_SHIFT = "/User/remove-work-shift";

export const getUsers = createAsyncThunk(
  "user/getUsers",
  async (filter?: userFilter): Promise<queryResult<user>> => {
    const res = await Axios.get<queryResult<user>>(USER, { params: filter });
    return res.data as queryResult<user>;
  }
);

export const getUserById = createAsyncThunk(
  "user/getUserById",
  async (id: number): Promise<user> => {
    const res = await Axios.get<user>(`${USER}/${id}`);
    return res.data as user;
  }
);

export const postUser = createAsyncThunk(
  "user/postUser",
  async (user: user): Promise<user> => {
    const res = await Axios.post<user>(USER, user);

    for (const role of user.roles?.filter(r => r.name !== getUserDefaultRole()) ?? []) {
      await new Promise(resolve => setTimeout(resolve, 1000));
      await Axios.post(USER_ADD_TO_ROLE, { userId: res.data.id, roleId: role.id } as userRole);
    }

    return { ...user, id: res.data.id };
  }
);

// TODO putUser
export const updateUser = createAsyncThunk(
  "user/updateUser",
  async ({ oldUser, newUser }: { oldUser: user, newUser: user }): Promise<user> => {
    if (oldUser.name !== newUser.name || oldUser.userName !== newUser.userName || oldUser.email !== newUser.email || oldUser.phoneNumber !== newUser.phoneNumber) {
      await Axios.put<user>(USER, newUser);
    }
    if (oldUser.roles !== newUser.roles) {
      const rolesToAdd = newUser.roles?.filter(r => !oldUser.roles?.map(r1 => r1.name).includes(r.name)) ?? [];
      const rolesToDelete = oldUser.roles?.filter(r => !newUser.roles?.map(r1 => r1.name).includes(r.name)) ?? [];

      for (const role of rolesToAdd) {
        await new Promise(resolve => setTimeout(resolve, 1000));
        await Axios.post<user>(USER_ADD_TO_ROLE, { userId: newUser.id, roleId: role.id } as userRole);
      }

      for (const role of rolesToDelete) {
        await new Promise(resolve => setTimeout(resolve, 1000));
        await Axios.delete<user>(USER_REMOVE_FROM_ROLE, { data: { userId: newUser.id, roleId: role.id } as userRole });
      }
    }

    return newUser;
  }
);

// TODO putUserPassword
export const updatePassword = createAsyncThunk(
  "user/updatePassword",
  async (password: updatedPassword): Promise<user> => {
    const res = await Axios.put<user>(USER_PASSWORD, password);
    return res.data as user;
  }
);

export const deleteUser = createAsyncThunk(
  "user/deleteUser",
  async (id: number): Promise<user> => {
    const res = await Axios.delete<user>(`${USER}/${id}`);
    return res.data as user;
  }
);

export const postAddWorkShift = createAsyncThunk(
  "user/postAddWorkShift",
  async (userWorkShift: userWorkShift): Promise<userWorkShift> => {
    const res = await Axios.post<userWorkShift>(USER_ADD_WORK_SHIFT, userWorkShift);
    return res.data as userWorkShift;
  }
);

export const deleteWorkShift = createAsyncThunk(
  "user/deleteWorkShift",
  async (userWorkShift: userWorkShift): Promise<userWorkShift> => {
    const res = await Axios.delete<userWorkShift>(USER_REMOVE_WORK_SHIFT, { data: userWorkShift });
    return res.data as userWorkShift;
  }
);
