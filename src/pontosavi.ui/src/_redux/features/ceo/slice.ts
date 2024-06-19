import { createSlice } from "@reduxjs/toolkit";

import { getCompanies, getCompanyById, postAddUserToRole, postCompany, postRole, postUser } from "./thunks";
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

const ceoSlice = createSlice({
  name: "ceo",
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

    builder.addCase(getCompanyById.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getCompanyById.fulfilled, (state) => {
      state.status = "idle";
    });
    builder.addCase(getCompanyById.rejected, (state, action) => {
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

    builder.addCase(postUser.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postUser.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(postUser.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postAddUserToRole.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postAddUserToRole.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(postAddUserToRole.rejected, (state, action) => {
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
  },
  selectors: {
    selectCompanies: state => state.companies,
    selectTotalCount: state => state.totalCount,
    selectStatus: state => state.status,
    selectError: state => state.error,
  }
});

export const { } = ceoSlice.actions;
export const { selectError, selectStatus, selectTotalCount, selectCompanies } = ceoSlice.selectors;

export default ceoSlice.reducer;
