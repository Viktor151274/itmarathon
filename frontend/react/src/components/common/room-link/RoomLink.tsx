import CopyButton from "../copy-button/CopyButton";
import type { RoomLinkProps } from "./types";
import styles from "../input/Input.module.scss";
import "./RoomLink.scss";

const RoomLink = ({ title, description, url, caption }: RoomLinkProps) => {
  return (
    <div className="room-link">
      <h2 className="room-link__title">{title}</h2>

      {description ? (
        <p className="room-link__description">{description}</p>
      ) : null}

      <div className="room-link__link-container">
        <input
          className={styles.inputWrapper__input}
          type="url"
          value={url}
          readOnly
        />
        <CopyButton textToCopy={url} />
      </div>

      {caption ? (
        <p className={`room-link__caption ${styles.inputWrapper__caption}`}>
          {caption}
        </p>
      ) : null}
    </div>
  );
};

export default RoomLink;
