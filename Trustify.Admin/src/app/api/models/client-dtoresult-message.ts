/* tslint:disable */
import { ClientDTO } from './client-dto';
import { OperationStatus } from './operation-status';

/**
 * Data class that enables sharing result, status and detailed message of some method
 */
export interface ClientDTOResultMessage {

  /**
   * Check for operation success
   */
  isSuccess?: boolean;

  /**
   * Detailed message of operation success
   */
  message?: null | string;
  result?: ClientDTO;
  status?: OperationStatus;
}
