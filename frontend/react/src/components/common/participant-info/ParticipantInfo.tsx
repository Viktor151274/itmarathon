import RoomLink from "../room-link/RoomLink";
import Button from "../button/Button";
import type { ParticipantInfoProps } from "./types";
import "./ParticipantInfo.scss";

const ParticipantInfo = ({
  participantName,
  roomName,
  participantLink,
  onViewInformation,
}: ParticipantInfoProps) => {
  return (
    <div className="participant-info">
      <div>
        <h3 className="participant-info__title">Hi, {participantName}</h3>
        <p className="participant-info__description">
          Get ready for happy gift exchange and fun in {roomName} game!
        </p>
      </div>

      <div>
        <RoomLink
          description="Your Participant Link"
          caption="Do not share this link with other users"
          url={participantLink}
          small
          white
        />

        <div className="participant-info__button">
          <Button variant="secondary" size="medium" onClick={onViewInformation}>
            View Information
          </Button>
        </div>
      </div>
    </div>
  );
};

export default ParticipantInfo;
