/* tslint:disable */
import { Credentials } from './credentials';
export interface UserWrapper {
  credentials?: null | Array<Credentials>;
  email?: null | string;
  emailVerified?: null | boolean;
  enabled?: null | boolean;
  firstName?: null | string;
  lastName?: null | string;
  requiredActions?: null | Array<string>;
  userName?: null | string;
}
