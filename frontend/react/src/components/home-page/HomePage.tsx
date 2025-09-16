import { useEffect } from "react";
import { useNavigate } from "react-router";
import Button from "../common/button/Button";
import "./HomePage.scss";
import { HOME_PAGE_TITLE } from "./utils";

const HomePage = () => {
  const navigate = useNavigate();

  const handleClick = () => navigate("/create-room");

  useEffect(() => {
    document.title = HOME_PAGE_TITLE;
  }, []);

  return (
    <main className="home-page">
      <div className="home-page__container">
        <h1 className="home-page__title">
          Make This Holiday Magical with Secret Nick
        </h1>
        <p className="home-page__description">
          It’s a secret — don’t tell who you’re matched with!
        </p>
        <p className="home-page__description">
          Use the wishlist or preferences to pick the perfect gift.
        </p>
        <p className="home-page__description">
          Be ready for the big gift exchange!
        </p>

        <Button width={281} onClick={handleClick}>
          Create Your Room
        </Button>
      </div>
    </main>
  );
};

export default HomePage;

// Do not delete: configuration console.log for DevOps team to make sure it will work on the cloud environment
// eslint-disable-next-line no-console
console.log(
  '"window.location.protocol" + "window.location.hostname" for React application',
  `${window.location.protocol}//${window.location.host}`,
);
