import { createSlice } from "@reduxjs/toolkit";

interface initialStateProps {
  open: boolean;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  open: false,
  status: "idle",
  error: null,
};

const handleSideBarSlice = createSlice({
  name: "handleSideBar",
  initialState,
  reducers: {
    handleSideBarOpen: (state) => {
      state.open = true;
    },
    handleSideBarClose: (state) => {
      state.open = false;
    },
  },
  selectors: {
    selectOpen: state => state.open,
    selectStatus: state => state.status,
    selectError: state => state.error,
  }
});

export const { handleSideBarClose, handleSideBarOpen } = handleSideBarSlice.actions;
export const { selectError, selectOpen, selectStatus } = handleSideBarSlice.selectors;

export default handleSideBarSlice.reducer;
