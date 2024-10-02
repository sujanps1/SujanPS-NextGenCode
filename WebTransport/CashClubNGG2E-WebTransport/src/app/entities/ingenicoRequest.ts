export interface IngenicoMessage {
    commandName: string;
    commandRequestData: string;
    commandResponseData: string;
}

export interface IngenicoCommandData {
    key: string;
    val: string;
}