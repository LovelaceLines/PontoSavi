import env from "./env";

export const getUserDefaultRole = (): string => "Colaborador";

export const getCEOUserRoles = (): string[] =>
  env.NODE_ENV == "development" ?
    ["Desenvolvedor", "CEO"] :
    ["CEO"];

export const getAllStandardUserRoles = (): string[] =>
  env.NODE_ENV == "development" ?
    ["Desenvolvedor", "Administrador", "Supervisor", "Colaborador"] :
    ["Administrador", "Supervisor", "Colaborador"];

export const getBaseUserRoles = (): string[] =>
  env.NODE_ENV == "development" ?
    ["Desenvolvedor", "Colaborador"] :
    ["Colaborador"];

export const getAdminUserRoles = (): string[] =>
  env.NODE_ENV == "development" ?
    ["Desenvolvedor", "Administrador"] :
    ["Administrador"];

export const getSuperUserRoles = (): string[] =>
  env.NODE_ENV == "development" ?
    ["Desenvolvedor", "Administrador", "Supervisor"] :
    ["Administrador", "Supervisor"];
