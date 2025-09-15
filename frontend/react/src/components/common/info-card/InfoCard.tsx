import type { InfoCardProps } from "./types";
import "./InfoCard.scss";

const InfoCard = ({ title, description, iconName, width }: InfoCardProps) => {
  return (
    <div className="info-card" style={{ width }}>
      <p className={`info-card__title info-card__title--${iconName}`}>
        {title}
      </p>
      <p className="info-card__description">{description}</p>
    </div>
  );
};

export default InfoCard;
