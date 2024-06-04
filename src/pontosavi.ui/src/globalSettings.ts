import env from "./env";

export const getUserDefaultRole = (): string => "Colaborador";

export const getSuperUserRoles = (): string[] =>
  env.NODE_ENV == "development" ?
    ["Desenvolvedor", "Administrador", "Supervisor"] :
    ["Administrador", "Supervisor"];
