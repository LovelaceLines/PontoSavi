import { combineReducers } from "redux";

import authReducer from "./features/auth/slice";
import companyReducer from "./features/company/slice";
import handleModal from "./features/handleModal/slice";
import roleReducer from "./features/role/slice";
import userReducer from "./features/user/slice";

export const rootReducer = combineReducers({
  auth: authReducer,
  company: companyReducer,
  handleModal: handleModal,
  role: roleReducer,
  user: userReducer,
});