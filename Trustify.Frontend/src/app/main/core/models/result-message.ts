import { OperationStatus } from './operation-status';
export interface ResultMessage {
  isSuccess?: boolean;
  message?: null | string;
  result?: any;
  status: OperationStatus;
}
