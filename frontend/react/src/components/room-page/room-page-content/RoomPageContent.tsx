import { useState } from "react";
import { useParams } from "react-router";
import ParticipantsList from "../participants-list/ParticipantsList";
import RoomDetails from "../room-details/RoomDetails";
import RandomizationModal from "@components/common/modals/randomization-modal/RandomizationModal";
import RandomizationPanel from "../randomization-panel/RandomizationPanel";
import { generateRoomLink } from "@utils/general";
import { getCurrentUser, getParticipantInfoById } from "./utils";
import type { WishlistProps } from "@components/common/wishlist/types";
import type { RoomPageContentProps } from "./types";
import "./RoomPageContent.scss";

const RoomPageContent = ({
  participants,
  roomDetails,
}: RoomPageContentProps) => {
  const { userCode } = useParams();
  const [isUserDetailsModalOpen, setIsUserDetailsModalOpen] = useState(false);

  if (!userCode) {
    return null;
  }

  const currentUser = getCurrentUser(userCode, participants);
  const isAdmin = currentUser?.isAdmin;

  const isRandomized = !!roomDetails?.closedOn;

  const giftRecipientId = currentUser?.giftToUserId;

  const giftRecipient = giftRecipientId
    ? getParticipantInfoById(giftRecipientId, participants)
    : null;

  const giftRecipientPersonalInfo = {
    firstName: giftRecipient?.firstName ?? "",
    lastName: giftRecipient?.lastName ?? "",
    phone: giftRecipient?.phone ?? "",
    email: giftRecipient?.email ?? "",
    deliveryInfo: giftRecipient?.deliveryInfo ?? "",
  };

  const giftRecipientFullName = `${giftRecipient?.firstName} ${giftRecipient?.lastName}`;

  const giftRecipientWishlistData: WishlistProps = giftRecipient?.wantSurprise
    ? { variant: "surprise", interests: giftRecipient?.interests ?? "" }
    : { variant: "wishlist", wishList: giftRecipient?.wishList ?? [] };

  const handleReadUserDetails = () => {
    setIsUserDetailsModalOpen(true);
  };

  return (
    <div className="room-page-content">
      <div className="room-page-content-column">
        <RoomDetails
          name={roomDetails.name}
          description={roomDetails.description}
          exchangeDate={roomDetails.giftExchangeDate}
          giftBudget={roomDetails.giftMaximumBudget}
          invitationNote={roomDetails.invitationNote}
          withoutInvitationCard={!isAdmin || isRandomized}
          roomLink={generateRoomLink(roomDetails.invitationCode)}
          invitationLink={generateRoomLink(roomDetails.invitationCode)}
        />
        <ParticipantsList participants={participants} />
      </div>

      <div className="room-page-content-column">
        {isAdmin || (!isAdmin && isRandomized) ? (
          <RandomizationPanel
            isRandomized={isRandomized}
            userCount={participants.length}
            fullName={giftRecipientFullName}
            onDraw={() => {}}
            onReadUserDetails={handleReadUserDetails}
          />
        ) : null}
      </div>

      {giftRecipientPersonalInfo ? (
        <RandomizationModal
          isOpen={isUserDetailsModalOpen}
          onClose={() => setIsUserDetailsModalOpen(false)}
          personalInfoData={giftRecipientPersonalInfo}
          wishlistData={giftRecipientWishlistData}
        />
      ) : null}
    </div>
  );
};

export default RoomPageContent;
