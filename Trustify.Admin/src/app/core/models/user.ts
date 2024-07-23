import { DigitalCertificate } from "./digital-certificate";
import { Group } from "./group";
import { Role } from "./role";

export interface User {
    username?: string;
    email?: string;
    birthday?: Date;
    address?: string;
    phone?: string;
    firstName?: string;
    lastName?: string;
    roles?: Role[] | null;
    groups?: Group[] | null;
    comment?: null | string;
    digitalCertificate?: DigitalCertificate;
    isUserActive?: null | boolean;
    keycloakId?: null | string;
    lastActivity?: null | string;
    phoneNumber?: null | string;
    userId?: null | number;
    userType?: null | string;
}
