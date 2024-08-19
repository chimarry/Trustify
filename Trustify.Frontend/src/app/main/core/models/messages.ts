
export enum Messages {
    GENERIC_ERROR = " ⚠ Error happened. For more info, check log files or try again.",
    FILE_SYSTEM_ERROR = " ⚠ Error happened while working with the file system.",
    DATABASE_ERROR = " ⚠ Error happened while working with the database.",
    UNKNOWN_ERROR = " ⚠ Error happened while working with the Web server.",
    WRITE_ERROR = " ⚠ Error happened while writing.",
    INVALID_DATA = " ⚠ The data you entered is invalid.",
    NOT_SUPPORTED = " ⚠ This operation is not supported.",
    NOT_FOUND = " ⚠ Nothing was found.",
    ROUTE_NOT_FOUND = " ⚠ On requested route nothing was found.",
    EXISTS = " ⚠ Resource with unique attributes already exists.",
    SUCCESS = "Operation was successful.",
    INTERNAL_SERVER_ERROR = " ⚠ Error happened on the server side.",
    FORBIDDEN_ACCESS = " ⚠ Forbidden access.",
    UNAUTHROIZED = " ⚠ Not authenticated. Access denied.",
    TOO_MANY_REQUEST = " ⚠ Too many requests done.",
    ERROR = " ⚠ Error happened.",
    CLIENT_ERROR = " ⚠ Client-side network error occurred.",
    NOT_COMPATIBLE = " ⚠ Machines are not compatible. ",
    MACHINE_DISCONNECTED = " ⚠ Machine is not connected."
}
