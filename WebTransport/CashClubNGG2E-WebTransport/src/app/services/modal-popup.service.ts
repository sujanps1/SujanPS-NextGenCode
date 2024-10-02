import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { IIngenicoModal } from "../modals/ingenico-modal/ingenico-modal.model";
import { ILoadingScreenModal } from "../modals/loading-screen-modal/loading-screen-modal.model";
import { ITransactionSummaryModal } from "../modals/transaction-summary-modal/transaction-summary-modal.model";
import { ITransactionStatusModal } from "../modals/transaction-status-modal/transaction-status-modal.model";

@Injectable({ providedIn: 'root' })
export class ModalPopupService {
    tranStatusModalSub = new Subject<ITransactionStatusModal>();
    loadingScreenModalSub = new Subject<ILoadingScreenModal>();
    ingenicoModalSub = new Subject<IIngenicoModal>();
    transactionSummaryModalSub = new Subject<ITransactionSummaryModal>();

    showIngenicoModal(headerData: string, ingenicoScreen: string) {
        this.ingenicoModalSub.next({
            ingenicoModalStatus: true,
            ingenicoModalHeader: headerData,
            ingenicoModalImageName: ingenicoScreen
        });
    }

    hideIngenicoModal() {
        this.ingenicoModalSub.next({
            ingenicoModalStatus: false,
            ingenicoModalHeader: '',
            ingenicoModalImageName: ''
        });
    }

    showLoadingScreenModal(headerData: string) {
        this.loadingScreenModalSub.next({
            loadingScreenModalStatus: true,
            loadingScreenHeaderContent: headerData
        });
    }

    hideLoadingScreenModal() {
        this.loadingScreenModalSub.next({
            loadingScreenModalStatus: false,
            loadingScreenHeaderContent: ''
        });
    }

    showTransactionSummaryModal(amount: string, payoutType: string, transactionType: string, feeTier: string, fee: string, total: string, patronReceiptSelection: string) {
        this.transactionSummaryModalSub.next({
            transactionSummaryModalStatus: true,
            amount: amount,
            payoutType: payoutType,
            transactionType: transactionType,
            feeTier: feeTier,
            fee: fee,
            total: total,
            patronReceiptSelection: patronReceiptSelection
        });
    }

    hideTransactionSummaryModal() {
        this.transactionSummaryModalSub.next({
            transactionSummaryModalStatus: false,
            amount: '',
            payoutType: '',
            transactionType: '',
            feeTier: '',
            fee: '',
            total: '',
            patronReceiptSelection: ''
        });
    }

    showTranStatusModal(tranStatus: string) {
        this.tranStatusModalSub.next({
            tarnStatusModalStatus: true,
            tarnStatusModalHeader: tranStatus,
        });
    }

    hideTranStatusModal() {
        this.tranStatusModalSub.next({
            tarnStatusModalStatus: false,
            tarnStatusModalHeader: '',
        });
    }
}