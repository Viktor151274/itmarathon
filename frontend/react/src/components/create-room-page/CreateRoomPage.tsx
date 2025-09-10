import { useEffect } from "react";
import { CREATE_ROOM_PAGE_TITLE } from "./utils";
import "./CreateRoomPage.scss";

const CreateRoomPage = () => {
  useEffect(() => {
    document.title = CREATE_ROOM_PAGE_TITLE;
  }, []);

  return <main className="create-room-page">Create Room Page</main>;
};

export default CreateRoomPage;
