export type apiException = {
  id: string;
  statusCode: number;
  message?: string;
  helpLink?: string;
};

export type badRequestException = {
  type: string;
  title: string;
  status: number;
  errors: {
    [key: string]: string[];
  };
  traceId: string;
};