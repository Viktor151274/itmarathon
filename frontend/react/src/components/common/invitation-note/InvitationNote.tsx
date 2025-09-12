import Input from "../input/Input";
import CopyButton from "../copy-button/CopyButton";
import type { InvitationNoteProps } from "./types";
import "./InvitationNote.scss";

const InvitationNote = ({
  value,
  width,
  ...restProps
}: InvitationNoteProps) => {
  return (
    <div>
      <h2 className="note-title">Invitation Note</h2>
      <div className="note-textarea" style={{ width }}>
        <Input
          value={value}
          multiline
          readOnly
          withoutCounter
          width={width}
          variant="invitation-note"
          {...restProps}
        />
        <div className="note-copy-button">
          <CopyButton
            textToCopy={value}
            successMessage="Invitation note is copied"
            errorMessage="Invitation note was not copied. Try again."
          />
        </div>
      </div>
    </div>
  );
};

export default InvitationNote;
