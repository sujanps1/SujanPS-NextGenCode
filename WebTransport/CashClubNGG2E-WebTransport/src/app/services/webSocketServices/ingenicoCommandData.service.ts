import { Injectable } from "@angular/core";
import { IngenicoCommandData } from "src/app/entities/ingenicoRequest";
import { PatronService } from "../applicationServices/patron.service";
import { ModalPopupService } from "../modal-popup.service";
import { DeviceCommand } from "src/app/entities/Enumeration";
import { TransactionService } from "../applicationServices/transaction.service";

@Injectable()
export class IngenicoCommandDataService {

    constructor(private modalPopupService: ModalPopupService, private patronService: PatronService,
        private transactionService: TransactionService) { }

    getCommandData(commandName: string, data: string[]): string {
        var requestData: IngenicoCommandData[] = [];
        switch (commandName) {
            case "ETRAMT":
                this.modalPopupService.showIngenicoModal('Enter Amount', 'amount-entry');
                break;
            case "SELLANG":
                this.modalPopupService.showIngenicoModal('Select Language', 'language-selection');
                var val1 = {
                    key: 'isShowLanguage',
                    val: data[0]
                }
                var val2 = {
                    key: 'deviceLanguage',
                    val: data[1]
                }
                requestData.push(val1);
                requestData.push(val2);
                return JSON.stringify(requestData);
            case "SELATP":
                //patron-receipt
                this.modalPopupService.showIngenicoModal('Transaction Type', 'transaction-type');
                return JSON.stringify(requestData);
            case "STAUTH":
                this.modalPopupService.showIngenicoModal('Insert, Swipe or Tap Card', 'insert-card');
                return JSON.stringify(requestData);
            case "EDAUTH":
                if (data && data.length > 0) {
                    var val1 = {
                        key: 'dataValue',
                        val: data[0]
                    }
                    requestData.push(val1);
                }
                return JSON.stringify(requestData);
            case "CNFFEE":
                this.modalPopupService.showIngenicoModal('Confirm Fee', 'amount-confirmation');
                requestData.push({
                    key: 'amount',
                    val: data[0]
                });
                requestData.push({
                    key: 'fee',
                    val: data[1]
                });
                requestData.push({
                    key: 'patronName',
                    val: this.patronService.patronData_Raw.FirstName + " " + this.patronService.patronData_Raw.LastName
                });

                if (data.length > 2) {
                    requestData.push({
                        key: 'isAuthorization',
                        val: data[2]
                    });
                }

                return JSON.stringify(requestData);
            case "CNFECA":
                this.modalPopupService.hideLoadingScreenModal();
                this.modalPopupService.showIngenicoModal('Electronic Agreement', 'electronic-agreement');
                return '';
            case "SNDONL":
                requestData.push({
                    key: 'PatronID',
                    val: data[0]
                });
                requestData.push({
                    key: 'autoComplete',
                    val: data[1]
                });
                requestData.push({
                    key: 'ccUserName',
                    val: data[2]
                });
                requestData.push({
                    key: 'payOut',
                    val: data[3]
                });
                requestData.push({
                    key: 'IDnumber',
                    val: data[4]
                });
                requestData.push({
                    key: 'originalFee',
                    val: data[5]
                });
                return JSON.stringify(requestData);
            case 'CNTWFL':
                if (data) {
                    this.modalPopupService.showIngenicoModal('Tap Card', 'insert-card');
                } else {
                    this.modalPopupService.showIngenicoModal('Transaction Type', 'transaction-type');
                }
                break;
            case 'CNFFLC':
                this.modalPopupService.hideTranStatusModal();
                this.modalPopupService.showIngenicoModal('FlexCharge Offer', 'flexcharge');
                break;
            case 'ENDFLC':
                //this.modalPopupService.hideTranStatusModal();
                break;
            case DeviceCommand.CaptureSSN:
                this.modalPopupService.showIngenicoModal('Social Security Number', 'ssn');
                break;
            case 'PRINTRCP':
                requestData.push({
                    key: 'cardNumber',
                    val: '1234-5678-9012-3456' // Replace with your dummy data
                });
                requestData.push({
                    key: 'amount',
                    val: '$100' // Replace with your dummy data
                });
                requestData.push({
                    key: 'fee',
                    val: '5' // Replace with your dummy data
                });
                requestData.push({
                    key: 'total',
                    val: '$105' // Replace with your dummy data
                });
                requestData.push({
                    key: 'purchaseFrom',
                    val: 'Credit' // Replace with your dummy data
                });
                requestData.push({
                    key: 'entry',
                    val: 'Swipe' // Replace with your dummy data
                });
                requestData.push({
                    key: 'payout',
                    val: 'CASH' // Replace with your dummy data
                });
                requestData.push({
                    key: 'network',
                    val: 'VISA' // Replace with your dummy data
                });
                return JSON.stringify(requestData);
            default:
                return '';
        }
    }
}
