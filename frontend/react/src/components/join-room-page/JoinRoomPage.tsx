import { useEffect } from "react";
import { useNavigate, useParams } from "react-router";
import Loader from "@components/common/loader/Loader";
import { useFetch } from "@hooks/useFetch";
import type { GetRoomResponse } from "@types/api";
import { BASE_API_URL } from "@utils/general";
import JoinRoomPageContent from "./join-room-page-content/JoinRoomPageContent";
import { JOIN_ROOM_PAGE_TITLE } from "./utils";
import "./JoinRoomPage.scss";

const JoinRoomPage = () => {
  const navigate = useNavigate();
  const { roomCode } = useParams();

  const {
    isLoading,
    isError,
    data: joinRoomInfo,
  } = useFetch({
    url: `${BASE_API_URL}/api/rooms?roomCode=${roomCode}`,
    method: "GET",
    headers: { "Content-Type": "application/json" },
    onError: () => {
      navigate("/home", {
        state: {
          toastMessage: "Something went wrong. Try again.",
        },
      });
    },
    onSuccess: (response: GetRoomResponse) => {
      if (response?.closedOn && Date.parse(response?.closedOn) <= Date.now()) {
        navigate("/home", {
          state: {
            toastMessage: "Room is unavailable or the link has expired",
          },
        });
      }
    },
  });

  const handleNavigate = () => {
    navigate(`/join/${roomCode}/details`, {
      state: {
        giftBudget: joinRoomInfo?.giftMaximumBudget,
      },
    });
  };

  useEffect(() => {
    document.title = JOIN_ROOM_PAGE_TITLE;
  }, []);

  return (
    <main className="join-room">
      {isLoading ? <Loader /> : null}

      {!isLoading && !isError && !!joinRoomInfo ? (
        <JoinRoomPageContent
          exchangeDate={joinRoomInfo?.giftExchangeDate}
          giftBudget={joinRoomInfo?.giftMaximumBudget}
          handleNavigate={handleNavigate}
        />
      ) : null}
    </main>
  );
};

export default JoinRoomPage;
