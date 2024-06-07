import { createSlice } from "@reduxjs/toolkit";

import { deleteUser, getUsers, postUser, updatePassword, updateUser } from "./thunks";
import { user } from "@/_types";
import { getUser, setUser } from "@/_services";

interface initialStateProps {
  user: user | null;
  users: user[] | null;
  totalCount: number | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  user: null,
  users: null,
  totalCount: null,
  status: "idle",
  error: null,
};

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
  },
  extraReducers: builder => {
    builder.addCase(getUsers.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getUsers.fulfilled, (state, action) => {
      state.users = action.payload.items;
      state.totalCount = action.payload.totalCount;
      state.status = "idle";
    });
    builder.addCase(getUsers.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postUser.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postUser.fulfilled, (state, action) => {
      state.user = action.payload;
      state.status = "succeeded";
    });
    builder.addCase(postUser.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(updateUser.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(updateUser.fulfilled, (state, action) => {
      state.user = action.payload;

      if (action.payload.publicId == getUser().publicId)
        setUser(action.payload);

      state.status = "succeeded";
    });
    builder.addCase(updateUser.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(updatePassword.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(updatePassword.fulfilled, (state, action) => {
      state.user = action.payload;

      state.status = "succeeded";
    });
    builder.addCase(updatePassword.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(deleteUser.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(deleteUser.fulfilled, (state, action) => {
      state.user = action.payload;
      state.status = "succeeded";
    });
    builder.addCase(deleteUser.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectUser: state => state.user,
    selectUsers: state => state.users,
    selectTotalCount: state => state.totalCount,
    selectStatus: state => state.status,
    selectError: state => state.error,
  }
});

export const { } = userSlice.actions;
export const { selectError, selectStatus, selectTotalCount, selectUser, selectUsers } = userSlice.selectors;

export default userSlice.reducer;