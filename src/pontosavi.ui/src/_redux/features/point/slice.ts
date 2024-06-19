import { createSlice } from "@reduxjs/toolkit";

import { getCurrentPoint, getPointById, getPoints, postPointApprove, postPointAutoCheckIn, postPointManualCheckIn, postPointReject, putPoint, putPointAutoCheckOut, putPointFull, putPointManualCheckOut } from "./thunks";
import { point } from "@/_types";

interface initialStateProps {
  points: point[];
  totalCount: number;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  points: [],
  totalCount: 0,
  status: "idle",
  error: null,
};

const pointSlice = createSlice({
  name: "point",
  initialState,
  reducers: {
  },
  extraReducers: builder => {
    builder.addCase(getPoints.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getPoints.fulfilled, (state, action) => {
      state.points = action.payload.items;
      state.totalCount = action.payload.totalCount;
      state.status = "idle";
    });
    builder.addCase(getPoints.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getPointById.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getPointById.fulfilled, (state) => {
      state.status = "idle";
    });
    builder.addCase(getPointById.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getCurrentPoint.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(getCurrentPoint.fulfilled, (state) => {
      state.status = "idle";
    });
    builder.addCase(getCurrentPoint.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postPointAutoCheckIn.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postPointAutoCheckIn.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(postPointAutoCheckIn.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putPointAutoCheckOut.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(putPointAutoCheckOut.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(putPointAutoCheckOut.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postPointManualCheckIn.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postPointManualCheckIn.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(postPointManualCheckIn.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putPointManualCheckOut.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(putPointManualCheckOut.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(putPointManualCheckOut.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putPoint.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(putPoint.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(putPoint.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putPointFull.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(putPointFull.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(putPointFull.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postPointApprove.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postPointApprove.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(postPointApprove.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postPointReject.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(postPointReject.fulfilled, (state) => {
      state.status = "succeeded";
    });
    builder.addCase(postPointReject.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectError: state => state.error,
    selectPoints: state => state.points,
    selectStatus: state => state.status,
    selectTotalCount: state => state.totalCount,
  }
});

export const { } = pointSlice.actions;
export const { selectError, selectPoints, selectStatus, selectTotalCount } = pointSlice.selectors;

export default pointSlice.reducer;