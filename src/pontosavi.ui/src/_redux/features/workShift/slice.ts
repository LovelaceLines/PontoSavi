import { createSlice } from "@reduxjs/toolkit";

import { deleteWorkShift, getWorkShiftById, getWorkShifts, postWorkShift, putWorkShift } from "./thunks";
import { workShift } from "@/_types";

interface initialStateProps {
  workShifts: workShift[];
  workShift: workShift | undefined;
  totalCount: number | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  workShifts: [],
  workShift: undefined,
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
      state.error = null;
    });
    builder.addCase(getWorkShifts.fulfilled, (state, action) => {
      state.workShifts = action.payload.items;
      state.totalCount = action.payload.totalCount;
      state.status = "idle";
      state.error = null;
    });
    builder.addCase(getWorkShifts.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getWorkShiftById.pending, (state) => {
      state.workShift = undefined;
      state.status = "loading";
      state.error = null;
    });
    builder.addCase(getWorkShiftById.fulfilled, (state, action) => {
      state.workShift = action.payload;
      state.status = "idle";
      state.error = null;
    });
    builder.addCase(getWorkShiftById.rejected, (state, action) => {
      state.workShift = undefined;
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postWorkShift.pending, (state) => {
      state.error = null;
    });
    builder.addCase(postWorkShift.fulfilled, (state, action) => {
      state.workShift = action.payload;
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(postWorkShift.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putWorkShift.pending, (state) => {
      state.error = null;
    });
    builder.addCase(putWorkShift.fulfilled, (state, action) => {
      state.workShift = action.payload;
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(putWorkShift.rejected, (state, action) => {
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
    selectWorkShift: state => state.workShift,
    selectWorkShifts: state => state.workShifts,
    selectTotalCount: state => state.totalCount,
    selectStatus: state => state.status,
    selectError: state => state.error,
  }
});

export const { } = workShiftSlice.actions;
export const { selectError, selectStatus, selectTotalCount, selectWorkShift, selectWorkShifts } = workShiftSlice.selectors;

export default workShiftSlice.reducer;