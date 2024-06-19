export const DateTimeToStr = ({ date }: { date?: Date }): JSX.Element => {
  if (!date) return <></>;
  date = new Date(date);
  return <>{date.toLocaleString()}</>;
};

export const DateOnlyToStr = ({ date }: { date?: Date }): JSX.Element => {
  if (!date) return <></>;
  date = new Date(date);
  return <>{date.toLocaleDateString()}</>;
};

export const TimeOnlyToStr = ({ date }: { date?: Date }): JSX.Element => {
  if (!date) return <></>;
  date = new Date(date);
  return <>{date.toLocaleTimeString()}</>;
};