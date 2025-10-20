import { useState } from "react";
import Input from "../input/Input";
import CopyButton from "../copy-button/CopyButton";
import IconButton from "../icon-button/IconButton";
import { NOTE_MAX_LENGTH } from "./utils";
import type { InputChangeEvent, InputChangeHandler } from "@types/general";
import type { InvitationNoteProps } from "./types";
import "./InvitationNote.scss";

const InvitationNote = ({
  value,
  invitationLink,
  width,
  ...restProps
}: InvitationNoteProps) => {
  const [noteValue, setNoteValue] = useState(value);
  const [isEditingMode, setIsEditingMode] = useState(false);

  const invitationNoteContent = `${noteValue}\n${invitationLink}`;
  const extraSymbolsCount =
    invitationNoteContent.length - (noteValue.length + invitationLink.length);
  const noteValueMaxLength =
    NOTE_MAX_LENGTH - invitationLink.length - extraSymbolsCount;

  const handleChange: InputChangeHandler = (e: InputChangeEvent) => {
    setNoteValue(e.target.value);
  };

  const toggleEditNote = () => {
    setIsEditingMode((prevIsEditingMode) => !prevIsEditingMode);
  };

  return (
    <div style={{ width }}>
      <h2 className="note-title">Invitation Note</h2>
      <div className="note-textarea">
        <Input
          value={noteValue}
          multiline
          readOnly={!isEditingMode}
          onChange={handleChange}
          withoutCounter
          width={width}
          variant="invitation-note"
          maxLength={noteValueMaxLength}
          {...restProps}
        />

        <p className="note-invitation-link">{invitationLink}</p>

        <div className="note-buttons">
          <IconButton
            iconName={isEditingMode ? "save" : "edit"}
            onClick={toggleEditNote}
          />

          <CopyButton
            textToCopy={invitationNoteContent}
            successMessage="Invitation note is copied"
            errorMessage="Invitation note was not copied. Try again."
          />
        </div>
      </div>

      <div className="note-info-container">
        {isEditingMode ? (
          <p className="note-caption">Make sure you save changes</p>
        ) : null}

        <p className="note-counter">
          {invitationNoteContent.length} / {NOTE_MAX_LENGTH}
        </p>
      </div>
    </div>
  );
};

export default InvitationNote;
