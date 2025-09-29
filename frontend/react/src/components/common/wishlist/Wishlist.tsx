import ItemCard from "../item-card/ItemCard";
import Link from "../link/Link";
import SurpriseItem from "../surprise-item/SurpriseItem";
import type { WishlistProps } from "./types";
import "./Wishlist.scss";

const Wishlist = (props: WishlistProps) => {
  const { width = "100%" } = props;

  return (
    <div className="wishlist" style={{ width }}>
      <h4 className="wishlist__title">Wishlist</h4>

      {props.variant === "wishlist" ? (
        <div className="wishlist__items-container">
          {props.wishList.map((wish, index) => (
            <ItemCard
              title={wish.name}
              key={`${index}${wish.name}${wish.infoLink}`}
            >
              {wish.infoLink ? <Link href={wish.infoLink}>Link</Link> : null}
            </ItemCard>
          ))}
        </div>
      ) : null}

      {props.variant === "surprise" ? (
        <SurpriseItem text={props.interests} />
      ) : null}
    </div>
  );
};

export default Wishlist;
