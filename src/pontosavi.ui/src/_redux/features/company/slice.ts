import { createSlice } from "@reduxjs/toolkit";

import { deleteCompany, getCompanies, getCompanyByPublicId, postCompany, putCompany } from "./thunks";
import { company } from "@/_types";

interface initialStateProps {
  companies: company[] | null;
  totalCount: number | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  companies: null,
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
    builder.addCase(getCompanies.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getCompanies.fulfilled, (state, action) => {
      state.companies = action.payload.items;
      state.totalCount = action.payload.totalCount;
      state.status = "idle";
    });
    builder.addCase(getCompanies.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getCompanyByPublicId.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getCompanyByPublicId.fulfilled, (state) => {
      state.status = "idle";
    });
    builder.addCase(getCompanyByPublicId.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postCompany.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postCompany.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(postCompany.rejected, (state, action) => {
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

    builder.addCase(deleteCompany.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(deleteCompany.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(deleteCompany.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectCompanies: state => state.companies,
    selectTotalCount: state => state.totalCount,
    selectStatus: state => state.status,
    selectError: state => state.error,
  }
});

export const { } = companySlice.actions;
export const { selectError, selectStatus, selectTotalCount, selectCompanies } = companySlice.selectors;

export default companySlice.reducer;
