import { combineReducers } from "redux";

import authReducer from "./features/auth/slice";
import ceoReducer from "./features/ceo/slice";
import companyReducer from "./features/company/slice";
import dayOffReducer from "./features/dayOff/slice";
import pointReducer from "./features/point/slice";
import roleReducer from "./features/role/slice";
import userReducer from "./features/user/slice";
import workShiftReducer from "./features/workShift/slice";

export const rootReducer = combineReducers({
  auth: authReducer,
  ceo: ceoReducer,
  company: companyReducer,
  dayOff: dayOffReducer,
  point: pointReducer,
  role: roleReducer,
  user: userReducer,
  workShift: workShiftReducer,
});