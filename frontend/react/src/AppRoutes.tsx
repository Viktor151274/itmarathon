import { Routes, Route, Navigate } from "react-router";
import Layout from "./Layout";
import HomePage from "./components/home-page/HomePage";
import NotFoundPage from "./components/not-found-page/NotFoundPage";

const AppRoutes = () => (
  <Routes>
    <Route path="/" element={<Layout />}>
      <Route index element={<Navigate to="home" replace />} />
      <Route path="home" element={<HomePage />} />

      <Route path="*" element={<NotFoundPage />} />
    </Route>
  </Routes>
);

export default AppRoutes;
