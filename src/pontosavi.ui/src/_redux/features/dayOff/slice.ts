import { createSlice } from "@reduxjs/toolkit";

import { deleteDayOff, getDaysOff, getDayOffById, postDayOff, putDayOff } from "./thunks";
import { dayOff } from "@/_types";

interface initialStateProps {
  daysOff: dayOff[];
  dayOff: dayOff | undefined;
  totalCount: number;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  daysOff: [],
  dayOff: undefined,
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
      state.error = null;
    });
    builder.addCase(getDaysOff.fulfilled, (state, action) => {
      state.daysOff = action.payload.items;
      state.totalCount = action.payload.totalCount;
      state.status = "idle";
      state.error = null;
    });
    builder.addCase(getDaysOff.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getDayOffById.pending, (state) => {
      state.dayOff = undefined;
      state.status = "loading";
      state.error = null;
    });
    builder.addCase(getDayOffById.fulfilled, (state, action) => {
      state.dayOff = action.payload;
      state.status = "idle";
      state.error = null;
    });
    builder.addCase(getDayOffById.rejected, (state, action) => {
      state.dayOff = undefined;
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postDayOff.pending, (state) => {
      state.error = null;
    });
    builder.addCase(postDayOff.fulfilled, (state, action) => {
      state.dayOff = action.payload;
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(postDayOff.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putDayOff.pending, (state) => {
      state.error = null;
    });
    builder.addCase(putDayOff.fulfilled, (state, action) => {
      state.dayOff = action.payload;
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(putDayOff.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(deleteDayOff.pending, (state) => {
      state.error = null;
    });
    builder.addCase(deleteDayOff.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(deleteDayOff.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectDaysOff: state => state.daysOff,
    selectDayOff: state => state.dayOff,
    selectError: state => state.error,
    selectStatus: state => state.status,
    selectTotalCount: state => state.totalCount,
  }
});

export const { } = dayOffSlice.actions;
export const { selectDayOff, selectDaysOff, selectError, selectStatus, selectTotalCount } = dayOffSlice.selectors;

export default dayOffSlice.reducer;