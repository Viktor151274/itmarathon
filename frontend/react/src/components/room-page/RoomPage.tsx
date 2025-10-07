import { useEffect } from "react";
import { useParams } from "react-router";
import Loader from "@components/common/loader/Loader.tsx";
import { useFetch } from "@hooks/useFetch.ts";
import useToaster from "@hooks/useToaster.ts";
import { BASE_API_URL } from "@utils/general.ts";
import type { RoomDetailsResponse } from "./types.ts";
import { ROOM_PAGE_TITLE } from "./utils.ts";
import "./RoomPage.scss";

const RoomPage = () => {
  const { showToast } = useToaster();
  const { userCode } = useParams();

  const { data: roomDetails, isLoading: isLoadingRoomDetails } =
    useFetch<RoomDetailsResponse>(
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
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const isRandomized = !!roomDetails?.closedOn;

  useEffect(() => {
    document.title = ROOM_PAGE_TITLE;
  }, []);

  return (
    <main className="room-page">
      {isLoadingRoomDetails ? <Loader /> : null}
    </main>
  );
};

export default RoomPage;
