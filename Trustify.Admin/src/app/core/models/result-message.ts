import { OperationStatus } from "./operation-status";

export interface ResultMessage {
    result: any,
    status: OperationStatus,
    message: string,
    isSuccess: boolean
}