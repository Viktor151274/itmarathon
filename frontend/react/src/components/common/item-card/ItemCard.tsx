import type { ItemCardProps } from "./types";
import "./ItemCard.scss";

const ItemCard = ({ title, children, width = "100%" }: ItemCardProps) => {
  return (
    <div className="item-card" style={{ width }}>
      {title ? <p className="item-card__title">{title}</p> : null}
      {children}
    </div>
  );
};

export default ItemCard;
