import { createSlice } from "@reduxjs/toolkit";

import { deleteWorkShift, getWorkShiftById, getWorkShifts, postWorkShift, putWorkShift } from "./thunks";
import { workShift } from "@/_types";

interface initialStateProps {
  workShifts: workShift[];
  totalCount: number | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  workShifts: [],
  totalCount: null,
  status: "idle",
  error: null,
};

const workShiftSlice = createSlice({
  name: "workShift",
  initialState,
  reducers: {
  },
  extraReducers: builder => {
    builder.addCase(getWorkShifts.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getWorkShifts.fulfilled, (state, action) => {
      state.workShifts = action.payload.items;
      state.totalCount = action.payload.totalCount;
      state.status = "idle";
    });
    builder.addCase(getWorkShifts.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getWorkShiftById.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getWorkShiftById.fulfilled, (state) => {
      state.status = "idle";
    });
    builder.addCase(getWorkShiftById.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postWorkShift.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postWorkShift.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(postWorkShift.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putWorkShift.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(putWorkShift.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(putWorkShift.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(deleteWorkShift.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(deleteWorkShift.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(deleteWorkShift.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectWorkShifts: state => state.workShifts,
    selectTotalCount: state => state.totalCount,
    selectStatus: state => state.status,
    selectError: state => state.error,
  }
});

export const { } = workShiftSlice.actions;
export const { selectError, selectStatus, selectTotalCount, selectWorkShifts } = workShiftSlice.selectors;

export default workShiftSlice.reducer;