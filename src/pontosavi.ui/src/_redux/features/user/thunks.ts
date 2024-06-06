import { createAsyncThunk } from "@reduxjs/toolkit";

import { getUserDefaultRole } from "@/globalSettings";
import { Axios } from "@/_http/axios";
import { queryResult, updatedPassword, user, userFilter, userRole } from "@/_types";

const USER = "/User";
const USER_PASSWORD = "/User/password";
const USER_ADD_TO_ROLE = "/User/add-to-role";
const USER_REMOVE_FROM_ROLE = "/User/remove-from-role";

export const getUsers = createAsyncThunk(
  "user/getUsers",
  async (filter?: userFilter): Promise<queryResult<user>> => {
    const res = await Axios.get<queryResult<user>>(USER, { params: filter });
    return res.data as queryResult<user>;
  }
);

export const postUser = createAsyncThunk(
  "user/postUser",
  async (user: user): Promise<user> => {
    const res = await Axios.post<user>(USER, user);

    user.roles.filter(r => r !== getUserDefaultRole()).forEach(async role => {
      await Axios.post<user>(USER_ADD_TO_ROLE, { userId: res.data.publicId, roleName: role } as userRole);
    });

    res.data.roles = user.roles;
    return res.data as user;
  }
);

export const updateUser = createAsyncThunk(
  "user/updateUser",
  async ({ oldUser, newUser }: { oldUser: user, newUser: user }): Promise<user> => {
    if (oldUser.userName !== newUser.userName || oldUser.email !== newUser.email || oldUser.password !== newUser.password) {
      await Axios.put<user>(USER, newUser);
    }
    if (oldUser.roles !== newUser.roles) {
      const rolesToAdd = newUser.roles.filter(r => !oldUser.roles.includes(r));
      const rolesToDelete = oldUser.roles.filter(r => !newUser.roles.includes(r));

      for (const role of rolesToAdd) {
        await Axios.post<user>(USER_ADD_TO_ROLE, { userId: newUser.publicId, roleName: role } as userRole);
      }

      for (const role of rolesToDelete) {
        await Axios.delete<user>(USER_REMOVE_FROM_ROLE, { data: { userId: newUser.publicId, roleName: role } as userRole });
      }
    }

    return newUser;
  }
);

export const updatePassword = createAsyncThunk(
  "user/updatePassword",
  async (password: updatedPassword): Promise<user> => {
    const res = await Axios.put<user>(USER_PASSWORD, password);
    return res.data as user;
  }
);

export const deleteUser = createAsyncThunk(
  "user/deleteUser",
  async (publicId: string): Promise<user> => {
    const res = await Axios.delete<user>(`${USER}/${publicId}`);
    return res.data as user;
  }
);
