import { useEffect } from "react";
import { FormsContextProvider } from "../../contexts/forms-context/FormsContextProvider";
import RoomFormWizard from "@components/common/room-form-wizard/RoomFormWizard";
import { JOIN_ROOM_DETAILS_PAGE_TITLE } from "./utils";
import "@assets/styles/common/forms-room-page.scss";

const JoinRoomDetailsPage = () => {
  const joinRoomFetchConfig = {
    url: "",
    method: "POST",
    headers: { "Content-Type": "application/json" },
    onError: () => {},
    onSuccess: () => {},
  };

  useEffect(() => {
    document.title = JOIN_ROOM_DETAILS_PAGE_TITLE;
  }, []);

  return (
    <FormsContextProvider>
      <main className="forms-room-page">
        <div className="forms-room-page__content">
          <RoomFormWizard fetchConfig={joinRoomFetchConfig} />
        </div>
      </main>
    </FormsContextProvider>
  );
};

export default JoinRoomDetailsPage;
