import type { FormWrapperContentProps } from "./types";
import { getDescriptionWrapperClass } from "./utils";

export const FormWrapperContent = ({
  title,
  description,
  subDescription,
  iconName,
  children,
}: FormWrapperContentProps) => {
  const descriptionWrapperClass = `form-wrapper__description-wrapper ${getDescriptionWrapperClass(iconName)}`;

  return (
    <div
      className={`form-wrapper__content ${
        iconName === "welcome-group" ? "form-wrapper__content--welcome" : ""
      }`}
    >
      <h3 className="form-wrapper__title">{title}</h3>
      {description ? (
        <div className={descriptionWrapperClass}>
          <p className="form-wrapper__description">{description}</p>
          {subDescription ? (
            <p className="form-wrapper__description--bold">{subDescription}</p>
          ) : null}
        </div>
      ) : null}
      {children}
    </div>
  );
};
