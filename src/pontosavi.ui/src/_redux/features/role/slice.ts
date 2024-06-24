import { createSlice } from "@reduxjs/toolkit";

import { deleteRole, getRoles, getRoleById, postRole, putRole } from "./thunks";
import { role } from "@/_types";

interface initialStateProps {
  roles: role[];
  role: role | undefined;
  totalCount: number;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  roles: [],
  role: undefined,
  totalCount: 0,
  status: "idle",
  error: null,
};

const roleSlice = createSlice({
  name: "role",
  initialState,
  reducers: {
  },
  extraReducers: builder => {
    builder.addCase(getRoles.pending, (state) => {
      state.status = "loading";
      state.error = null;
    });
    builder.addCase(getRoles.fulfilled, (state, action) => {
      state.roles = action.payload.items;
      state.totalCount = action.payload.totalCount;
      state.status = "idle";
      state.error = null;
    });
    builder.addCase(getRoles.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getRoleById.pending, (state) => {
      state.role = undefined;
      state.status = "loading";
      state.error = null;
    });
    builder.addCase(getRoleById.fulfilled, (state, action) => {
      state.role = action.payload;
      state.status = "idle";
      state.error = null;
    });
    builder.addCase(getRoleById.rejected, (state, action) => {
      state.role = undefined;
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postRole.pending, (state) => {
      state.error = null;
    });
    builder.addCase(postRole.fulfilled, (state, action) => {
      state.status = "succeeded";
      state.role = action.payload;
      state.error = null;
    });
    builder.addCase(postRole.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putRole.pending, (state) => {
      state.error = null;
    });
    builder.addCase(putRole.fulfilled, (state, action) => {
      state.role = action.payload;
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(putRole.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(deleteRole.pending, (state) => {
      state.error = null;
    });
    builder.addCase(deleteRole.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(deleteRole.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectError: state => state.error,
    selectRoles: state => state.roles,
    selectRole: state => state.role,
    selectStatus: state => state.status,
    selectTotalCount: state => state.totalCount,
  }
});

export const { } = roleSlice.actions;
export const { selectError, selectRole, selectRoles, selectStatus, selectTotalCount } = roleSlice.selectors;

export default roleSlice.reducer;