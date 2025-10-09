import { useParams } from "react-router";
import ParticipantCard from "@components/common/participant-card/ParticipantCard";
import {
  MAX_PARTICIPANTS_NUMBER,
  generateParticipantLink,
} from "@utils/general";
import { type ParticipantsListProps } from "./types";
import "./ParticipantsList.scss";

const ParticipantsList = ({ participants }: ParticipantsListProps) => {
  const { userCode } = useParams();
  const admin = participants?.find((participant) => participant?.isAdmin);
  const restParticipants = participants?.filter(
    (participant) => !participant?.isAdmin,
  );

  return (
    <div className="participant-list">
      <div className="participant-list-header">
        <h3 className="participant-list-header__title">Who’s Playing?</h3>

        <span className="participant-list-counter__current">
          {participants?.length ?? 0}/
        </span>

        <span className="participant-list-counter__max">
          {MAX_PARTICIPANTS_NUMBER}
        </span>
      </div>

      <div className="participant-list__cards">
        {admin ? (
          <ParticipantCard
            key={admin?.id}
            firstName={admin?.firstName}
            lastName={admin?.lastName}
            isCurrentUser={userCode === admin?.userCode}
            isAdmin={admin?.isAdmin}
            isCurrentUserAdmin={userCode === admin?.userCode}
            adminInfo={`${admin?.phone}${admin?.email ? `\n${admin?.email}` : ""}`}
            participantLink={generateParticipantLink(admin?.userCode)}
          />
        ) : null}

        {restParticipants?.map((user) => (
          <ParticipantCard
            key={user?.id}
            firstName={user?.firstName}
            lastName={user?.lastName}
            isCurrentUser={userCode === user?.userCode}
            isCurrentUserAdmin={userCode === admin?.userCode}
            participantLink={generateParticipantLink(user?.userCode)}
          />
        ))}
      </div>
    </div>
  );
};

export default ParticipantsList;
