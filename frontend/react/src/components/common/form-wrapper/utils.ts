export const FORM_CONTENT_MAP = {
  CREATE_ROOM: {
    title: "Create Your Secret Nick Room",
    description:
      "Let the holiday magic begin! Set up your gift exchange in just a few steps",
  },
  WELCOME_ROOM: {
    title: "Welcome to the Secret Squad!",
    description:
      "You've been invited to a cozy holiday gift exchange! Get ready to surprise and be surprised!",
  },
  READY_ROOM: {
    title: "Your Secret Nick Room is Ready!",
    description:
      "Share the link below with up to 20 friends to invite them — and don’t lose your personal link! Let the festive magic begin!",
  },
  JOINED_ROOM: {
    title: "You have just joined the room! 🎅",
    description:
      "Your holiday adventure is about to begin! Thank you for joining the Secret Nick room.",
  },
  ADD_DETAILS: {
    title: "Add Your Details",
    description:
      "Let your Secret Nick know what would make you smile this season",
  },
  ADD_WISHES: {
    title: "Add Your Wishes",
    description:
      "Let your Secret Nick know what would make you smile this season",
  },
} as const;

export function getFormContent(formKey: keyof typeof FORM_CONTENT_MAP) {
  return FORM_CONTENT_MAP[formKey];
}

export const ICON_NAMES = {
  STAR: "star",
  CAR: "car",
  PRESENTS: "presents",
  WREATH: "wreath",
  WELCOME_GROUP: "welcome-group",
} as const;

export const FORM_WRAPPER_CONTENT_PROPS = {
  TITLE: "title",
  ICON_NAME: "iconName",
  CHILDREN: "children",
  DESCRIPTION: "description",
  SUB_DESCRIPTION: "subDescription",
} as const;

export function getDescriptionWrapperClass(iconName?: string): string {
  const classMap: Record<string, string> = {
    [ICON_NAMES.WELCOME_GROUP]:
      "form-wrapper__description-wrapper--welcome-group",
    [ICON_NAMES.WREATH]: "form-wrapper__description-wrapper--wreath",
    [ICON_NAMES.STAR]: "form-wrapper__description-wrapper--star",
  };

  return classMap[iconName ?? ""] || "";
}
