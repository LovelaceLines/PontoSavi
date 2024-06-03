import { combineReducers } from "redux";

import authReducer from "./features/auth/slice";
import handleSideBar from "./features/handleSideBar/slice";
import handleModal from "./features/handleModal/slice";
import roleReducer from "./features/role/slice";
import userReducer from "./features/user/slice";

export const rootReducer = combineReducers({
  auth: authReducer,
  handleSideBar: handleSideBar,
  handleModal: handleModal,
  role: roleReducer,
  user: userReducer,
});