import { Routes, Route, Navigate } from "react-router";
import Layout from "./Layout";
import HomePage from "./components/home-page/HomePage";
import NotFoundPage from "./components/not-found-page/NotFoundPage";
import CreateRoomPage from "./components/create-room-page/CreateRoomPage";
import CreateRoomSuccessPage from "./components/create-room-success-page/CreateRoomSuccessPage";
import PrivateRoute from "./components/common/private-route/PrivateRoute";
import RoomPage from "./components/room-page/RoomPage";
import JoinRoomPage from "@components/join-room-page/JoinRoomPage";
import JoinRoomDetailsPage from "@components/join-room-details-page/JoinRoomDetailsPage";

const AppRoutes = () => (
  <Routes>
    <Route path="/" element={<Layout />}>
      <Route index element={<Navigate to="home" replace />} />

      <Route path="home" element={<HomePage />} />

      <Route path="create-room" element={<CreateRoomPage />} />

      <Route
        path="create-room/success"
        element={
          <PrivateRoute pageName="create-room-success-page">
            <CreateRoomSuccessPage />
          </PrivateRoute>
        }
      />

      <Route path="room/:userCode" element={<RoomPage />} />
      <Route path="join/:roomCode" element={<JoinRoomPage />} />
      <Route path="join/:roomCode/details" element={<JoinRoomDetailsPage />} />
      <Route path="*" element={<NotFoundPage />} />
    </Route>
  </Routes>
);

export default AppRoutes;
