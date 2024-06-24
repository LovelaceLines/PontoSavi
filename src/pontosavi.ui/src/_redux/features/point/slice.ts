import { createSlice } from "@reduxjs/toolkit";

import { getCurrentPoint, getPointById, getPoints, postPointApprove, postPointAutoCheckIn, postPointManualCheckIn, postPointReject, putPoint, putPointAutoCheckOut, putPointFull, putPointManualCheckOut } from "./thunks";
import { point } from "@/_types";

interface initialStateProps {
  points: point[];
  point: point | undefined;
  openPoint: point | null;
  totalCount: number;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  points: [],
  point: undefined,
  openPoint: null,
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
      state.error = null;
    });
    builder.addCase(getPoints.fulfilled, (state, action) => {
      state.points = action.payload.items;
      state.totalCount = action.payload.totalCount;
      state.status = "idle";
      state.error = null;
    });
    builder.addCase(getPoints.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getPointById.pending, (state) => {
      state.point = undefined;
      state.status = "loading";
      state.error = null;
    });
    builder.addCase(getPointById.fulfilled, (state, action) => {
      state.point = action.payload;
      state.status = "idle";
      state.error = null;
    });
    builder.addCase(getPointById.rejected, (state, action) => {
      state.point = undefined;
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(getCurrentPoint.pending, (state) => {
      state.openPoint = null;
      state.status = "loading";
      state.error = null;
    });
    builder.addCase(getCurrentPoint.fulfilled, (state, action) => {
      state.openPoint = action.payload;
      state.status = "idle";
      state.error = null;
    });
    builder.addCase(getCurrentPoint.rejected, (state) => {
      state.openPoint = null;
      state.status = "failed";
      // state.error = action.error.message || null;
    });

    builder.addCase(postPointAutoCheckIn.pending, (state) => {
      state.error = null;
    });
    builder.addCase(postPointAutoCheckIn.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(postPointAutoCheckIn.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putPointAutoCheckOut.pending, (state) => {
      state.error = null;
    });
    builder.addCase(putPointAutoCheckOut.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(putPointAutoCheckOut.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postPointManualCheckIn.pending, (state) => {
      state.error = null;
    });
    builder.addCase(postPointManualCheckIn.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(postPointManualCheckIn.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putPointManualCheckOut.pending, (state) => {
      state.error = null;
    });
    builder.addCase(putPointManualCheckOut.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(putPointManualCheckOut.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putPoint.pending, (state) => {
      state.error = null;
    });
    builder.addCase(putPoint.fulfilled, (state, action) => {
      state.point = action.payload;
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(putPoint.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(putPointFull.pending, (state) => {
      state.error = null;
    });
    builder.addCase(putPointFull.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(putPointFull.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postPointApprove.pending, (state) => {
      state.error = null;
    });
    builder.addCase(postPointApprove.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(postPointApprove.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(postPointReject.pending, (state) => {
      state.error = null;
    });
    builder.addCase(postPointReject.fulfilled, (state) => {
      state.status = "succeeded";
      state.error = null;
    });
    builder.addCase(postPointReject.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectError: state => state.error,
    selectPoint: state => state.point,
    selectOpenPoint: state => state.openPoint,
    selectPoints: state => state.points,
    selectStatus: state => state.status,
    selectTotalCount: state => state.totalCount,
  }
});

export const { } = pointSlice.actions;
export const { selectError, selectOpenPoint, selectPoint, selectPoints, selectStatus, selectTotalCount } = pointSlice.selectors;

export default pointSlice.reducer;