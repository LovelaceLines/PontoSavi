import { combineReducers } from "redux";

import authReducer from "./features/auth/slice";
import handleModal from "./features/handleModal/slice";
import roleReducer from "./features/role/slice";
import userReducer from "./features/user/slice";

export const rootReducer = combineReducers({
  auth: authReducer,
  handleModal: handleModal,
  role: roleReducer,
  user: userReducer,
});