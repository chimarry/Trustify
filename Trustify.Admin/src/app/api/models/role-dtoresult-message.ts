/* tslint:disable */
import { RoleDTO } from './role-dto';
import { OperationStatus } from './operation-status';

/**
 * Data class that enables sharing result, status and detailed message of some method
 */
export interface RoleDTOResultMessage {

  /**
   * Check for operation success
   */
  isSuccess?: boolean;

  /**
   * Detailed message of operation success
   */
  message?: null | string;
  result?: RoleDTO;
  status?: OperationStatus;
}
