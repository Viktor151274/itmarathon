/* eslint-disable @typescript-eslint/no-unused-vars */
import type { RoomPageContentProps } from "./types";
import { getCurrentUser } from "./utils";
import "./RoomPageContent.scss";
import { useParams } from "react-router";

const RoomPageContent = ({ users, roomDetails }: RoomPageContentProps) => {
  const { userCode } = useParams();

  if (!userCode) {
    return null;
  }

  const currentUser = getCurrentUser(userCode, users);
  const isAdmin = currentUser?.isAdmin;

  const isRandomized = !!roomDetails?.closedOn;

  return <div>Room Page Content</div>;
};

export default RoomPageContent;
