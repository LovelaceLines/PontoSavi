import { createSlice } from "@reduxjs/toolkit";

import { deleteRole, getRoles, getRoleByPublicId, postRole, putRole } from "./thunks";
import { role } from "@/_types";

interface initialStateProps {
  roles: role[];
  totalCount: number;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  roles: [],
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
    builder.addCase(getRoles.fulfilled, (state, action) => {
      state.roles = action.payload.items;
      state.totalCount = action.payload.totalCount;
    });
    builder.addCase(getRoles.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getRoleByPublicId.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postRole.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postRole.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(postRole.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putRole.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(putRole.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(putRole.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(deleteRole.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(deleteRole.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectError: state => state.error,
    selectRoles: state => state.roles,
    selectStatus: state => state.status,
    selectTotalCount: state => state.totalCount,
  }
});

export const { } = roleSlice.actions;
export const { selectError, selectRoles, selectStatus, selectTotalCount } = roleSlice.selectors;

export default roleSlice.reducer;