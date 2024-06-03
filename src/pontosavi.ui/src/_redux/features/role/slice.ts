import { createSlice } from "@reduxjs/toolkit";

import { getAllRoles } from "./thunks";
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
    builder.addCase(getAllRoles.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getAllRoles.fulfilled, (state, action) => {
      state.roles = action.payload.items;
      state.totalCount = action.payload.totalCount;
      state.status = "succeeded";
    });
    builder.addCase(getAllRoles.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectRoles: state => state.roles,
    selectStatus: state => state.status,
    selectError: state => state.error,
  }
});

export const { } = roleSlice.actions;
export const { selectError, selectRoles, selectStatus } = roleSlice.selectors;

export default roleSlice.reducer;