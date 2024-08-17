/* tslint:disable */
import { GroupDTO } from './group-dto';
import { OperationStatus } from './operation-status';

/**
 * Data class that enables sharing result, status and detailed message of some method
 */
export interface GroupDTOResultMessage {

  /**
   * Check for operation success
   */
  isSuccess?: boolean;

  /**
   * Detailed message of operation success
   */
  message?: null | string;
  result?: GroupDTO;
  status?: OperationStatus;
}
