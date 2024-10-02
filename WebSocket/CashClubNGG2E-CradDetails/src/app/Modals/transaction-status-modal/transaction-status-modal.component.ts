import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { ModalPopupService } from 'src/app/services/modal-popup.service';
import { ITransactionStatusModal } from './transaction-status-modal.model';

@Component({
  selector: 'cc-transaction-status-modal',
  templateUrl: './transaction-status-modal.component.html',
  styleUrls: ['./transaction-status-modal.component.css']
})
export class TransactionApprovedModalComponent {
  isDisplay: boolean = false;
  modalName: string;
  imagePath: string;
  private modalSub: Subscription;

  constructor(public modalPopupService: ModalPopupService) { }

  ngOnInit(): void {
    this.modalSub = this.modalPopupService.tranStatusModalSub.subscribe((transactionStatusModal: ITransactionStatusModal) => {
      this.isDisplay = transactionStatusModal.tarnStatusModalStatus;
      this.modalName = transactionStatusModal.tarnStatusModalHeader;
      if (transactionStatusModal.tarnStatusModalHeader.toLocaleLowerCase() === 'approved') {
        //this.imagePath = "../assets/images/transaction-approved.png";
        this.imagePath = "../assets/images/lane500_success.gif";
      } else {
        // this.imagePath = "../assets/images/transaction-declined.png";
        this.imagePath = "../assets/images/transactionDeclined.gif";
      }
    });
  }

  ngOnDestroy(): void {
    this.modalSub.unsubscribe();
  }
}