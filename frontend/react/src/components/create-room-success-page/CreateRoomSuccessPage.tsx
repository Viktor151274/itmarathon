import { useEffect } from "react";
import { useLocation, useNavigate } from "react-router";

import FormWrapper from "../common/form-wrapper/FormWrapper";
import RoomLink from "../common/room-link/RoomLink";
import InvitationNote from "../common/invitation-note/InvitationNote";
import type { CreateRoomSuccessPageProps, RoomAndUserData } from "./types";
import { CREATE_ROOM_SUCCESS_PAGE_TITLE } from "./utils";
import {
  generateRoomLink,
  generateParticipantLink,
  generateInvitationNoteContent,
} from "../../utils/general";

import "./CreateRoomSuccessPage.scss";

const CreateRoomSuccessPage = ({
  roomAndUserData: roomAndUserDataProp,
}: CreateRoomSuccessPageProps) => {
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    document.title = CREATE_ROOM_SUCCESS_PAGE_TITLE;
  }, []);

  const state = location.state as { roomAndUserData?: RoomAndUserData };
  const roomAndUserData = roomAndUserDataProp ?? state?.roomAndUserData;
  const userCode = roomAndUserData?.userCode;

  if (!roomAndUserData) {
    return null;
  }

  const { roomLink, participantLink, invitationNoteContent } = {
    roomLink: generateRoomLink(roomAndUserData.invitationCode),
    participantLink: generateParticipantLink(roomAndUserData.userCode),
    invitationNoteContent: generateInvitationNoteContent(
      roomAndUserData.invitationNote,
      roomAndUserData.invitationCode,
    ),
  };

  const handleVisitRoom = () => {
    if (userCode) {
      navigate(`/room/${userCode}`);
    }
  };

  return (
    <main className="create-room-success-page">
      <FormWrapper
        formKey="READY_ROOM"
        iconName="wreath"
        buttonProps={{
          children: "Visit Your Room",
          onClick: handleVisitRoom,
        }}
      >
        <div className="create-room-success-page__content">
          <RoomLink title="Your Room Link" url={roomLink} />
          <InvitationNote value={invitationNoteContent} />
          <RoomLink
            title="Your Personal Participant Link"
            description="This is your unique personal link to access the Secret Nick room."
            url={participantLink}
            caption="Do not share this link with other users"
          />
        </div>
      </FormWrapper>
    </main>
  );
};

export default CreateRoomSuccessPage;
