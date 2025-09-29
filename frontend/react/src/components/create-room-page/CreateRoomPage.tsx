import { useContext, useEffect } from "react";
import { useNavigate } from "react-router";

import { FormsContextProvider } from "../../contexts/forms-context/FormsContextProvider";
import {
  defaultRoomData,
  FormsContext,
} from "../../contexts/forms-context/FormsContext";
import RoomFormWizard from "../common/room-form-wizard/RoomFormWizard";

import useToaster from "@hooks/useToaster";
import { BASE_API_URL } from "@utils/general";
import { CREATE_ROOM_PAGE_TITLE } from "./utils";
import type { CreateRoomResponse } from "./types";
import "@assets/styles/common/forms-room-page.scss";

const CreateRoomPage = () => {
  const { setRoomData } = useContext(FormsContext);
  const { showToast } = useToaster();
  const navigate = useNavigate();

  const createRoomFetchConfig = {
    url: `${BASE_API_URL}/api/rooms`,
    method: "POST",
    headers: { "Content-Type": "application/json" },
    onError: () => {
      showToast("Something went wrong. Try again.", "error", "large");
    },
    onSuccess: (response: CreateRoomResponse) => {
      setRoomData(defaultRoomData);
      navigate("/create-room/success", {
        state: {
          roomAndUserData: {
            invitationCode: response?.room?.invitationCode,
            invitationNote: response?.room?.invitationNote,
            userCode: response?.userCode,
          },
        },
      });
    },
  };

  useEffect(() => {
    document.title = CREATE_ROOM_PAGE_TITLE;
  }, []);

  return (
    <FormsContextProvider>
      <main className="forms-room-page">
        <div className="forms-room-page__content">
          <RoomFormWizard fetchConfig={createRoomFetchConfig} isAdmin />
        </div>
      </main>
    </FormsContextProvider>
  );
};

export default CreateRoomPage;
