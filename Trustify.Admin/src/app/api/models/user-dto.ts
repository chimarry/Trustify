/* tslint:disable */
import { Credentials } from './credentials';
import { FederatedIdentity } from './federated-identity';
export interface UserDTO {
  clientRoles?: null | {[key: string]: any};
  createdTimestamp?: null | number;
  credentials?: null | Array<Credentials>;
  email?: null | string;
  emailVerified?: null | boolean;
  enabled?: null | boolean;
  federatedIdentities?: null | Array<FederatedIdentity>;
  federationLink?: null | string;
  firstName?: null | string;
  groups?: null | Array<string>;
  id?: null | string;
  lastName?: null | string;
  realmRoles?: null | Array<string>;
  requiredActions?: null | Array<string>;
  userName?: null | string;
}
