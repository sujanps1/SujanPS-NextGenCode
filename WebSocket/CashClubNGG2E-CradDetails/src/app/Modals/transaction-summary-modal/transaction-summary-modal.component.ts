import { Component, OnInit, OnDestroy } from '@angular/core';
import { ModalPopupService } from '../../services/modal-popup.service';
import { Subscription } from 'rxjs';
import { ITransactionSummaryModal } from './transaction-summary-modal.model';
import { IngenicoWebsocketService } from 'src/app/services/webSocketServices/ingenico.websocket.service';
import { ScreenMovementWebSocketService } from 'src/app/services/webSocketServices/screenMovement.websocket.service';
import { MainScreenTempService } from 'src/app/services/applicationServices/cc-mainscreentemp.service';

@Component({
  selector: 'cc-transaction-summary-modal',
  templateUrl: './transaction-summary-modal.component.html',
  styleUrls: ['./transaction-summary-modal.component.css'],
  providers: [IngenicoWebsocketService]
})
export class TransactionSummaryModalComponent implements OnInit, OnDestroy {
  private modalSub: Subscription;
  modalStatus: boolean = false;
  amount: string;
  payoutType: string;
  transactionType: string;
  feeTier: string;
  fee: string;
  total: string;
  patronReceiptSelection: string;

  constructor(private modalPopupService: ModalPopupService,
    private ingenicoWebSocket: IngenicoWebsocketService, private screenService: ScreenMovementWebSocketService,
    private mainscreenTempService: MainScreenTempService) { }

  ngOnInit(): void {
    this.modalSub = this.modalPopupService.transactionSummaryModalSub.subscribe((transactionSummaryModal: ITransactionSummaryModal) => {
      this.modalStatus = transactionSummaryModal.transactionSummaryModalStatus;
      this.amount = transactionSummaryModal.amount;
      this.payoutType = transactionSummaryModal.payoutType;
      this.transactionType = transactionSummaryModal.transactionType;
      this.feeTier = transactionSummaryModal.feeTier;
      this.fee = transactionSummaryModal.fee;
      this.total = transactionSummaryModal.total;
      this.patronReceiptSelection = transactionSummaryModal.patronReceiptSelection;
    });
  }

  ngOnDestroy(): void {
    this.modalSub.unsubscribe();
  }

  
  onReprint() { }

  onClose() {
    this.ingenicoWebSocket.messages.next({
      commandName: 'EDTRAN',
      commandRequestData: '',
      commandResponseData: ''
    });
    this.screenService.messages.next('Go');

    this.modalPopupService.hideTransactionSummaryModal();
    this.mainscreenTempService.showMainScreen(true);
  }
}