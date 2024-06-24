import { createSlice } from "@reduxjs/toolkit";

import { deleteUser, deleteWorkShift, getUserById, getUsers, postAddWorkShift, postUser, putUser, putUserPassword } from "./thunks";
import { user } from "@/_types";
import { getUser, setUser } from "@/_services";

interface initialStateProps {
  user: user | undefined;
  users: user[] | null;
  totalCount: number | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  user: undefined,
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
      state.error = null;
    });
    builder.addCase(getUsers.fulfilled, (state, action) => {
      state.users = action.payload.items;
      state.totalCount = action.payload.totalCount;
      state.status = "idle";
      state.error = null;
    });
    builder.addCase(getUsers.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getUserById.pending, (state) => {
      state.user = undefined;
      state.status = "loading";
      state.error = null;
    });
    builder.addCase(getUserById.fulfilled, (state, action) => {
      state.user = action.payload;
      state.status = "idle";
      state.error = null;
    });
    builder.addCase(getUserById.rejected, (state, action) => {
      state.user = undefined;
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postUser.pending, (state) => {
      state.error = null;
    });
    builder.addCase(postUser.fulfilled, (state, action) => {
      state.user = action.payload;
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(postUser.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putUser.pending, (state) => {
      state.error = null;
    });
    builder.addCase(putUser.fulfilled, (state, action) => {
      state.user = action.payload;

      if (action.payload.id == getUser().id)
        setUser(action.payload);

      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(putUser.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putUserPassword.pending, (state) => {
      state.error = null;
    });
    builder.addCase(putUserPassword.fulfilled, (state, action) => {
      state.user = action.payload;
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(putUserPassword.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(deleteUser.pending, (state) => {
      state.error = null;
    });
    builder.addCase(deleteUser.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(deleteUser.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postAddWorkShift.pending, (state) => {
      state.error = null;
    });
    builder.addCase(postAddWorkShift.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(postAddWorkShift.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(deleteWorkShift.pending, (state) => {
      state.error = null;
    });
    builder.addCase(deleteWorkShift.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(deleteWorkShift.rejected, (state, action) => {
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