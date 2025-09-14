export const InputNames = {
  FIRST_NAME: "firstName",
  LAST_NAME: "lastName",
  ADDRESS: "address",
  EMAIL: "email",
  PHONE: "phone",
} as const;

export interface FormData {
  firstName: string;
  lastName: string;
  address: string;
  phone: string;
  email: string;
}

export interface DetailsFormProps {
  onBack: () => void;
}

export const requiredFields: (keyof FormData)[] = [
  "firstName",
  "lastName",
  "phone",
  "address",
];
