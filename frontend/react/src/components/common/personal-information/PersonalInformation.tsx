import type { PersonalInformationProps } from "./types";
import { formatPhoneNumber } from "./utils";
import "./PersonalInformation.scss";

const PersonalInformation = ({
  firstName,
  lastName,
  phoneNumber,
  email = "-",
  deliveryAddress,
  width = "100%",
}: PersonalInformationProps) => {
  return (
    <div style={{ width }}>
      <h4 className="personal-info-title">Personal Information</h4>

      <div className="personal-info-content">
        <div className="personal-info-content__field">
          <p className="personal-info-content__label">First name</p>
          <p className="personal-info-content__value">{firstName}</p>
        </div>

        <div className="personal-info-content__field">
          <p className="personal-info-content__label">Last name</p>
          <p className="personal-info-content__value">{lastName}</p>
        </div>

        <div className="personal-info-content__field">
          <p className="personal-info-content__label">Phone number</p>
          <p className="personal-info-content__value">
            {formatPhoneNumber(phoneNumber)}
          </p>
        </div>

        <div className="personal-info-content__field">
          <p className="personal-info-content__label">Email</p>
          <p className="personal-info-content__value">{email}</p>
        </div>

        <div className="personal-info-content__field">
          <p className="personal-info-content__label">Delivery address</p>
          <p className="personal-info-content__value">{deliveryAddress}</p>
        </div>
      </div>
    </div>
  );
};

export default PersonalInformation;
