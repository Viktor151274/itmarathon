/* eslint-disable @typescript-eslint/no-unused-vars */
import { useParams } from "react-router";
import ParticipantsList from "../participants-list/ParticipantsList";
import type { RoomPageContentProps } from "./types";
import { getCurrentUser } from "./utils";
import "./RoomPageContent.scss";

const RoomPageContent = ({
  participants,
  roomDetails,
}: RoomPageContentProps) => {
  const { userCode } = useParams();

  if (!userCode) {
    return null;
  }

  const currentUser = getCurrentUser(userCode, participants);
  const isAdmin = currentUser?.isAdmin;

  const isRandomized = !!roomDetails?.closedOn;

  return (
    <div className="room-page-content">
      <div className="room-page-content-column">
        <ParticipantsList participants={participants} />
      </div>
    </div>
  );
};

export default RoomPageContent;
