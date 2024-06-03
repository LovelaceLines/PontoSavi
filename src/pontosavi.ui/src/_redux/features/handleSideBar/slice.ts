import { getStorageValue, setStorageValue } from "@/_services";
import { createSlice } from "@reduxjs/toolkit";

interface initialStateProps {
  open: boolean;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  open: getStorageValue("sideBarOpen", true) as boolean || true,
  status: "idle",
  error: null,
};

const handleSideBarSlice = createSlice({
  name: "handleSideBar",
  initialState,
  reducers: {
    toggleSideBar: (state) => {
      state.open = !state.open;
      setStorageValue("sideBarOpen", state.open);
    },
  },
  selectors: {
    selectOpen: state => state.open,
    selectStatus: state => state.status,
    selectError: state => state.error,
  }
});

export const { toggleSideBar } = handleSideBarSlice.actions;
export const { selectError, selectOpen, selectStatus } = handleSideBarSlice.selectors;

export default handleSideBarSlice.reducer;
