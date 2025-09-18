import { useEffect } from "react";
import { useParams, useNavigate } from "react-router";
import { JOIN_ROOM_PAGE_TITLE } from "./utils";
import FormWrapper from "@components/common/form-wrapper/FormWrapper";
import InfoCard from "@components/common/info-card/InfoCard";
import JoinRoomSprite from "@assets/images/icons/join-room-sprite.svg";
import "./JoinRoomPage.scss";

const JoinRoomPage = () => {
  const navigate = useNavigate();
  const { roomCode } = useParams();

  const handleNavigate = () => {
    navigate(`/join/${roomCode}/details`);
  };

  useEffect(() => {
    document.title = JOIN_ROOM_PAGE_TITLE;
  }, []);

  return (
    <main className="join-room">
      <FormWrapper
        formKey="WELCOME_ROOM"
        iconName="welcome-group"
        buttonProps={{
          children: "Join the Room",
          onClick: handleNavigate,
        }}
      >
        <div className="join-room-content">
          <div className="join-room-content__cards">
            <InfoCard
              title="Exchange Date"
              description="06 Dec 2025"
              iconName="star"
              width="244px"
            />
            <InfoCard
              title="Gift Budget"
              description="1000 UAH"
              iconName="presents"
              width="244px"
            />
          </div>

          <h4 className="join-room-content__header">
            To join the fun, you&apos;ll just need to:
          </h4>

          <ul className="join-room-content__list">
            <li>
              <svg width={24} height={24}>
                <use href={`${JoinRoomSprite}#fugar`} />
              </svg>
              Fill in your details
            </li>

            <li>
              <svg width={24} height={24}>
                <use href={`${JoinRoomSprite}#gift`} />
              </svg>
              Add your wishlist (or let the magic decide!)
            </li>
          </ul>

          <p>It only takes a minute — and the joy will last all season long!</p>
        </div>
      </FormWrapper>
    </main>
  );
};

export default JoinRoomPage;
