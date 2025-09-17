import { useEffect } from "react";

import { CREATE_ROOM_PAGE_TITLE } from "./utils";
import { FormsContextProvider } from "../../contexts/forms-context/FormsContextProvider";

import PageContent from "./page-content/PageContent";
import "./CreateRoomPage.scss";

const CreateRoomPage = () => {
  useEffect(() => {
    document.title = CREATE_ROOM_PAGE_TITLE;
  }, []);

  return (
    <FormsContextProvider>
      <main className="create-room-page">
        <div className="create-room-page__content">
          <PageContent />
        </div>
      </main>
    </FormsContextProvider>
  );
};

export default CreateRoomPage;
