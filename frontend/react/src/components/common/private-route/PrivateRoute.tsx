import { Navigate, useLocation } from "react-router";
import type { PrivateRouteProps } from "./types";
import { TOASTER_MESSAGES } from "./utils";

const PrivateRoute = ({ children, pageName }: PrivateRouteProps) => {
  const location = useLocation();
  const state = location.state;

  if (pageName === "create-room-success-page" && !state?.roomAndUserData) {
    return (
      <Navigate
        to="/home"
        replace
        state={{
          toastMessage: TOASTER_MESSAGES.CREATE_ROOM_SUCCESS_PAGE,
        }}
      />
    );
  }

  return children;
};

export default PrivateRoute;
