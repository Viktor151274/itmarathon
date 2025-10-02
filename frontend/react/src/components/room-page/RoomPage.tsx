import { useEffect } from "react";
import { ROOM_PAGE_TITLE } from "./utils.ts";
import "./RoomPage.scss";

const RoomPage = () => {
  useEffect(() => {
    document.title = ROOM_PAGE_TITLE;
  }, []);

  return <main className="room-page">Your room</main>;
};

export default RoomPage;
