import { createSlice } from "@reduxjs/toolkit";

import { deleteRemoveWorkShift, getCompany, postAddWorkShift, putCompany } from "./thunks";
import { company } from "@/_types";

interface initialStateProps {
  company: company | null;
  totalCount: number | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  company: null,
  totalCount: null,
  status: "idle",
  error: null,
};

const companySlice = createSlice({
  name: "company",
  initialState,
  reducers: {
  },
  extraReducers: builder => {
    builder.addCase(getCompany.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getCompany.fulfilled, (state, action) => {
      state.company = action.payload;
      state.status = "idle";
    });
    builder.addCase(getCompany.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putCompany.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(putCompany.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(putCompany.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postAddWorkShift.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postAddWorkShift.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(postAddWorkShift.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(deleteRemoveWorkShift.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(deleteRemoveWorkShift.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(deleteRemoveWorkShift.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectCompany: state => state.company,
    selectTotalCount: state => state.totalCount,
    selectStatus: state => state.status,
    selectError: state => state.error,
  }
});

export const { } = companySlice.actions;
export const { selectCompany, selectError, selectStatus, selectTotalCount } = companySlice.selectors;

export default companySlice.reducer;
