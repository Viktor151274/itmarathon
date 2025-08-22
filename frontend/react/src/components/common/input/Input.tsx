import styles from "./Input.module.scss";
import type { InputProps } from "./types";

const Input = ({
  type = "text",
  placeholder,
  value,
  onChange,
  label,
  required = false,
  width = 286,
  caption,
  hasError = false,
  maxLength = 40,
  withCounter = true,
  multiline = false,
  ...restProps
}: InputProps) => {
  const inputId = `input-${label.replace(" ", "-").toLowerCase()}`;
  const Element = multiline ? "textarea" : "input";

  return (
    <div
      className={`${styles.inputWrapper} ${hasError ? styles["inputWrapper--error"] : ""}`}
      style={{ width }}
    >
      <label
        htmlFor={inputId}
        className={`${styles.inputWrapper__label} ${required ? styles["inputWrapper__label--required"] : ""}`}
      >
        {label}
      </label>

      <div className={styles.inputWrapper__inputContainer}>
        <Element
          id={inputId}
          value={value}
          onChange={onChange}
          type={!multiline ? type : undefined}
          placeholder={placeholder}
          className={`${styles.inputWrapper__input} ${withCounter ? styles["inputWrapper__input--withCounter"] : ""}`}
          {...restProps}
        />

        {withCounter && maxLength > 0 ? (
          <div className={styles.inputWrapper__counter}>
            {value.toString().length} / {maxLength}
          </div>
        ) : null}
      </div>

      {caption ? (
        <span className={styles.inputWrapper__caption}>{caption}</span>
      ) : null}
    </div>
  );
};

export default Input;
