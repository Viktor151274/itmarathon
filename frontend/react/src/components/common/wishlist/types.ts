interface WishlistBaseProps {
  width?: string;
}

interface WishlistListProps extends WishlistBaseProps {
  variant: "wishlist";
  wishList: {
    name?: string;
    infoLink?: string;
  }[];
}

interface WishlistSurpriseProps extends WishlistBaseProps {
  variant: "surprise";
  interests: string;
}

export type WishlistProps = WishlistListProps | WishlistSurpriseProps;
