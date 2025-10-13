import { useEffect } from "react";
import { useParams } from "react-router";
import type { GetParticipantsResponse, GetRoomResponse } from "@types/api.ts";
import Loader from "@components/common/loader/Loader.tsx";
import { useFetch } from "@hooks/useFetch.ts";
import useToaster from "@hooks/useToaster.ts";
import { BASE_API_URL } from "@utils/general.ts";
import RoomPageContent from "./room-page-content/RoomPageContent.tsx";
import { ROOM_PAGE_TITLE } from "./utils.ts";
import type {} from "./types.ts";
import "./RoomPage.scss";

const RoomPage = () => {
  const { showToast } = useToaster();
  const { userCode } = useParams();

  useEffect(() => {
    document.title = ROOM_PAGE_TITLE;
  }, []);

  const {
    data: roomDetails,
    isError: isErrorRoomDetails,
    isLoading: isLoadingRoomDetails,
  } = useFetch<GetRoomResponse>(
    {
      url: `${BASE_API_URL}/api/rooms?userCode=${userCode}`,
      method: "GET",
      headers: { "Content-Type": "application/json" },
      onError: () => {
        showToast("Something went wrong. Try again.", "error", "large");
      },
    },
    true,
  );

  const {
    isLoading: isLoadingParticipants,
    isError: isErrorParticipants,
    data: participants,
  } = useFetch<GetParticipantsResponse>({
    url: `${BASE_API_URL}/api/users?userCode=${userCode}`,
    method: "GET",
    headers: { "Content-Type": "application/json" },
    onError: () => {
      showToast("Something went wrong. Try again.", "error", "large");
    },
  });

  const isLoading = isLoadingParticipants || isLoadingRoomDetails;
  const isError = isErrorParticipants || isErrorRoomDetails;

  if (!userCode) {
    return null;
  }

  return (
    <main className="room-page">
      {isLoading ? <Loader /> : null}

      {!isLoading && !isError && !!participants && !!roomDetails ? (
        <RoomPageContent
          participants={participants}
          roomDetails={roomDetails}
        />
      ) : null}
    </main>
  );
};

export default RoomPage;
