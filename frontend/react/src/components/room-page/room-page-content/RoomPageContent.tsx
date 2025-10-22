import { useState } from "react";
import { useParams } from "react-router";
import RandomizationModal from "@components/common/modals/randomization-modal/RandomizationModal";
import ViewWishlistModal from "@components/common/modals/view-wishlist-modal/ViewWishlistModal";
import ParticipantsList from "../participants-list/ParticipantsList";
import RoomDetails from "../room-details/RoomDetails";
import WishlistPreview from "../wishlist-preview/WishlistPreview";
import RandomizationPanel from "../randomization-panel/RandomizationPanel";
import { generateRoomLink } from "@utils/general";
import { getCurrentUser, getParticipantInfoById } from "./utils";
import type { WishlistProps } from "@components/common/wishlist/types";
import type { RoomPageContentProps } from "./types";
import "./RoomPageContent.scss";

const RoomPageContent = ({
  participants,
  roomDetails,
  onDrawNames,
}: RoomPageContentProps) => {
  const { userCode } = useParams();
  const [isUserDetailsModalOpen, setIsUserDetailsModalOpen] = useState(false);
  const [isViewWishlistModalOpen, setViewWishlistModalOpen] = useState(false);

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

  const handleViewWishListModal = () => {
    setViewWishlistModalOpen(true);
  };

  return (
    <div className="room-page-content">
      <div className="room-page-content-row">
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
      </div>

      <div className="room-page-content-row">
        <ParticipantsList participants={participants} />

        <div className="room-page-content-column">
          {isAdmin || (!isAdmin && isRandomized) ? (
            <RandomizationPanel
              isRandomized={isRandomized}
              userCount={participants.length}
              fullName={giftRecipientFullName}
              onDraw={onDrawNames}
              onReadUserDetails={handleReadUserDetails}
            />
          ) : null}

          <WishlistPreview
            isWantSurprise={currentUser?.wantSurprise}
            wishListData={currentUser?.wishList}
            onViewWishlist={handleViewWishListModal}
          />
        </div>
      </div>

      {giftRecipientPersonalInfo ? (
        <RandomizationModal
          isOpen={isUserDetailsModalOpen}
          onClose={() => setIsUserDetailsModalOpen(false)}
          personalInfoData={giftRecipientPersonalInfo}
          wishlistData={giftRecipientWishlistData}
        />
      ) : null}

      {currentUser ? (
        <ViewWishlistModal
          isOpen={isViewWishlistModalOpen}
          onClose={() => setViewWishlistModalOpen(false)}
          budget={roomDetails.giftMaximumBudget}
          wantSurprise={currentUser.wantSurprise}
          interests={currentUser.interests ?? ""}
          wishlistData={currentUser.wishList ?? []}
        />
      ) : null}
    </div>
  );
};

export default RoomPageContent;
