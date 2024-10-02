// import { Component, DoCheck, OnChanges, OnInit, SimpleChanges } from '@angular/core';
// import { CardData } from '../entities/cardDetails';
// import { PatronService } from '../services/applicationServices/patron.service';
// import { TransactionService } from '../services/applicationServices/transaction.service';
// import { TransactionwindowComponent } from '../transactionwindow/transactionwindow.component';

// @Component({
//   selector: 'cc-transaction-detail',
//   templateUrl: './transaction-detail.component.html',
//   styleUrls: ['./transaction-detail.component.css'],
// })
// export class TransactionDetailComponent implements OnInit {

//   isTransactionDetailsAvailable: boolean = false;
//   transactionData: any;

//   transactionWindowDetails: TransactionwindowComponent;

//   constructor(private transactionService: TransactionService, private patronService: PatronService) {
//     this.transactionService.cardDetailsSub.subscribe((cardData: CardData) => {
//       this.isTransactionDetailsAvailable = true;
//       if (cardData.cardHolderName && cardData.cardHolderName.length > 0) {
//         this.cardHolderName = cardData.cardHolderName;
//       } else {
//         this.cardHolderName = this.patronService.patronData_Raw.FirstName + ' ' + this.patronService.patronData_Raw.LastName;
//       }
//       this.bankName = cardData.bankName;
//       this.cardType = cardData.cardType;
//       this.cardNumber = cardData.cardNumber;
//       this.transactionType = cardData.transactionType === "PINLESS_CREDIT" ? "Credit" : "Debit";
//       console.log("Amount Screen", this.transactionType);
//       this.transactionMode = cardData.transactionMode;
//     });
//   }

//   cardNumber: string = '-';
//   cardHolderName: string = '-';
//   bankName: string = '-';
//   cardType: string = '-';
//   transactionType: string = '-';
//   transactionMode: string = '-';

//   ngOnInit(): void {
//     this.transactionData = this.transactionService.getTransactionData();

//     // this.cardHolderName = this.defaultTransactionDetails.cardHolderName;
//     // this.bankName = this.defaultTransactionDetails.bankName;
//     // this.cardType = this.defaultTransactionDetails.cardType;
//     // this.cardNumber = this.defaultTransactionDetails.cardNumber;
//     // this.transactionMode = this.defaultTransactionDetails.transactionMode;
//     // this.transactionType = this.defaultTransactionDetails.transactionType;
//   }
// }


import { Component, OnInit } from '@angular/core';
import { TransactionService } from '../services/applicationServices/transaction.service';
import { CardData } from '../entities/cardDetails';
import { LoginAccountServiceService } from '../services/login-account-service.service';
import { Subscription } from 'rxjs';
import { PatronInformationComponent } from '../patron-information/patron-information.component';
import { ModalPopupService } from '../services/modal-popup.service';


@Component({
  selector: 'cc-transaction-detail',
  templateUrl: './transaction-detail.component.html',
  styleUrls: ['./transaction-detail.component.css']
})
export class TransactionDetailComponent implements OnInit {
  cardHolderName: string;
  bankName: string;
  cardType: string;
  cardNumber: string;
  transactionMode: string;
  transactionType: string;
  cardData: any;
  private messageSubscription: Subscription;
  Language: string




  isTransactionDetailsAvailable: boolean = false;

  constructor(private transactionService: TransactionService,
    private loginAccountService: LoginAccountServiceService,
    private modalPopupService:ModalPopupService

  ) { }

  ngOnInit(): void {

    this.loginAccountService.cardSubject.subscribe(data => {
      this.cardData = data;
      console.log('Card Data received:', this.cardData);
      this.cardHolderName=this.cardData.cardHolderName;
      this.cardNumber=this.cardData.cardNumber;
      this.bankName=this.cardData.bankName;
      this.transactionMode=this.cardData.transactionMode;
      this.transactionType=this.cardData.transactionType;
      this.cardType=this.cardData.cardType;
      // Use the card data as needed
    });



    // this.cardData = this.transactionService.getTransactionData();
    // this.isTransactionDetailsAvailable = this.cardData !== null;
  }
}
