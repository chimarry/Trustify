/* tslint:disable */
import { UserDTO } from './user-dto';
import { OperationStatus } from './operation-status';

/**
 * Data class that enables sharing result, status and detailed message of some method
 */
export interface UserDTOResultMessage {

  /**
   * Check for operation success
   */
  isSuccess?: boolean;

  /**
   * Detailed message of operation success
   */
  message?: null | string;
  result?: UserDTO;
  status?: OperationStatus;
}
