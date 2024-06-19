import { createSlice } from "@reduxjs/toolkit";

import { deleteDayOff, getDaysOff, getDayOffById, postDayOff, putDayOff } from "./thunks";
import { dayOff } from "@/_types";

interface initialStateProps {
  daysOff: dayOff[];
  totalCount: number;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  daysOff: [],
  totalCount: 0,
  status: "idle",
  error: null,
};

const dayOffSlice = createSlice({
  name: "dayOff",
  initialState,
  reducers: {
  },
  extraReducers: builder => {
    builder.addCase(getDaysOff.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getDaysOff.fulfilled, (state, action) => {
      state.daysOff = action.payload.items;
      state.totalCount = action.payload.totalCount;
      state.status = "idle";
    });
    builder.addCase(getDaysOff.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getDayOffById.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getDayOffById.fulfilled, (state) => {
      state.status = "idle";
    });
    builder.addCase(getDayOffById.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postDayOff.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postDayOff.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(postDayOff.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putDayOff.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(putDayOff.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(putDayOff.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(deleteDayOff.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(deleteDayOff.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(deleteDayOff.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectDaysOff: state => state.daysOff,
    selectError: state => state.error,
    selectStatus: state => state.status,
    selectTotalCount: state => state.totalCount,
  }
});

export const { } = dayOffSlice.actions;
export const { selectDaysOff, selectError, selectStatus, selectTotalCount } = dayOffSlice.selectors;

export default dayOffSlice.reducer;