import { createSlice } from "@reduxjs/toolkit";

import { getAuthToken, getUser, setAuthToken, setUser } from "@/_services";
import { loginUser, refreshToken } from "./thunks";
import { authToken, user } from "@/_types";

interface initialStateProps {
  authToken: authToken | null;
  user: user | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: initialStateProps = {
  authToken: getAuthToken() || null,
  user: getUser() || null,
  status: "idle",
  error: null,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    logoutUser(state) {
      setAuthToken({} as authToken);
      setUser({} as user);
      state.authToken = null;
      state.user = null;
      state.status = "idle";
    },
  },
  extraReducers: builder => {
    builder.addCase(loginUser.pending, (state) => {
      state.status = "loading";
    });
    builder.addCase(loginUser.fulfilled, (state, action) => {
      state.authToken = action.payload.authToken;
      state.user = action.payload.user;

      setAuthToken(action.payload.authToken);
      setUser(action.payload.user);

      state.status = "succeeded";
    });
    builder.addCase(loginUser.rejected, (state, action) => {
      state.status = "failed";
      state.error = action.error.message || null;
    });

    builder.addCase(refreshToken.pending, (state) => {
      const { refreshToken } = getAuthToken() || { refreshToken: "" };
      setAuthToken({ accessToken: refreshToken, refreshToken: refreshToken } as authToken);

      state.status = "loading";
    });
    builder.addCase(refreshToken.fulfilled, (state, action) => {
      setAuthToken(action.payload);
      state.authToken = action.payload;

      state.status = "succeeded";
    });
    builder.addCase(refreshToken.rejected, (state, action) => {
      setAuthToken({} as authToken);
      state.authToken = null;
      setUser({} as user);
      state.user = null;

      state.status = "failed";
      state.error = action.error.message || null;
    });
  },
  selectors: {
    selectAuthToken: state => state.authToken,
    selectUser: state => state.user,
    selectStatus: state => state.status,
    selectError: state => state.error,
  }
});

export const { logoutUser } = authSlice.actions;
export const { selectAuthToken, selectError, selectStatus, selectUser } = authSlice.selectors;

export default authSlice.reducer;