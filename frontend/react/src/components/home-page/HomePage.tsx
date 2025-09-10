import { useEffect } from "react";
import { useNavigate } from "react-router";
import { HOME_PAGE_TITLE } from "./utils";
import Button from "../common/button/Button";
import "./HomePage.scss";

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
