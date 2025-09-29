export const translateDate = (dateString: string) => {
  const date = new Date(dateString);
  const options: Intl.DateTimeFormatOptions = {
    day: "2-digit",
    month: "short",
    year: "numeric",
  };
  return date.toLocaleDateString("en-GB", options);
};

export const formatBudget = (budget: number) => {
  if (budget === 0) return "Unlimited";
  return `${budget} UAH`;
};
