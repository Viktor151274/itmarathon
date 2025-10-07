import InfoCard from "@components/common/info-card/InfoCard";
import Button from "@components/common/button/Button";
import { formatBudget, copyToClipboard, formatDate } from "@utils/general";
import type { RoomDetailsProps } from "./types";
import "./RoomDetails.scss";

const RoomDetails = ({
  name,
  description,
  exchangeDate,
  giftBudget,
  invitationNote,
  withoutInvitationCard = false,
}: RoomDetailsProps) => {
  const handleCopyInvitationNote = () => {
    copyToClipboard(invitationNote);
  };

  return (
    <div className="room-details">
      <div className="room-details__content">
        <h2>{name}</h2>
        <p className="room-details__description">{description}</p>
      </div>

      <div className="room-details__cards-container">
        <InfoCard
          title="Exchange Date"
          description={formatDate(exchangeDate)}
          iconName="star"
          variant="white"
        />

        <InfoCard
          title="Gift Budget"
          description={formatBudget(giftBudget)}
          iconName="presents"
          variant="white"
        />

        {!withoutInvitationCard ? (
          <InfoCard title="Invitation Note" iconName="note" variant="white">
            <Button
              size="small"
              variant="secondary"
              onClick={handleCopyInvitationNote}
            >
              Invite New Members
            </Button>
          </InfoCard>
        ) : null}
      </div>
    </div>
  );
};

export default RoomDetails;
