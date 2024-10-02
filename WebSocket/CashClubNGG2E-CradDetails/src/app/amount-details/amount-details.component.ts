import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { CardData } from '../entities/cardDetails';
import { IngenicoCommandData, IngenicoMessage } from '../entities/ingenicoRequest';
import { TransactionService } from '../services/applicationServices/transaction.service';
import { ModalPopupService } from '../services/modal-popup.service';
import { IngenicoWebsocketService } from '../services/webSocketServices/ingenico.websocket.service';
import { IngenicoCommandDataService } from '../services/webSocketServices/ingenicoCommandData.service';
import { CCAlertService } from '../services/applicationServices/cc-alert.service';
import { PatronService } from '../services/applicationServices/patron.service';
import { LoginAccountServiceService } from '../services/login-account-service.service';

@Component({
  selector: 'cc-amount-details',
  templateUrl: './amount-details.component.html',
  styleUrls: ['./amount-details.component.css']
})
export class AmountDetailsComponent implements OnInit {
  @Input() isAmountEnabled = true;
  

  payoutTypes: string[] = [];
  feeTypes: string[] = [];
  last4NotAvailable: boolean;
  payoutType: string;
  feeType: string;
  cardlast4Number: string;
  feeAmount: string;
  requestedAmount: string;
  totalAmount: string = '$00.00';
  subcription: Subscription;
  isRetrieval: boolean;
  flexChargeFLow: Boolean;
  isControlDisabled: boolean = true;
  isAmountControlDisabled: boolean = true;
  isLabelEnabled: boolean = false;
  private messageSubscription: Subscription;
  private amountSubscription:Subscription;
  private confirmAmountSubscription:Subscription;

  private signatureSubscription:Subscription;
  private finalSubscription:Subscription;

  private NoConfirmAmountSubscription:Subscription;

  showpopup:boolean=false;
  showpopups:boolean=false;
  buttonLabel="Amount"
  buttonLabels="Confirm Money"
  buttonSecondPopup="Signature"
  showpopups1:boolean=false;
  showpopups2:boolean=false;
  buttonThirdPopup="Generating Receipt..."
  showPopup4: boolean = false;
  serverSignature: string = '';
  ServerSig:string='';
  transactionSuccessful = false;
  noCarddata:boolean=false;
  popupContent:string="Patron want to close the Transaction";

  constructor(private modalPopupService: ModalPopupService,
    private transactionService: TransactionService,
    private ingenicoWebsocketService: IngenicoWebsocketService,
    private commandDataProvider: IngenicoCommandDataService,
    private ccAlertService: CCAlertService,
    private patronService: PatronService,
    private loginAccountService: LoginAccountServiceService
  ) {

    this.feeTypes.push('VIP');
    this.feeTypes.push('Standard');
    this.payoutTypes.push('Cash');
    this.payoutTypes.push('Chips');
    this.payoutTypes.push('Gaming Tickets');
    this.payoutTypes.push('Wager');
    this.payoutTypes.push('Voucher');
  }

  resetPrivateVariables() {
    this.flexChargeFLow = false;
    this.isRetrieval = false;
    this.cardlast4Number = "";
    this.feeAmount='0.0';
    this.requestedAmount = this.loginAccountService.response;
  }

  ngOnInit(): void {
    this.messageSubscription =this.loginAccountService.cardSubject.subscribe(data => {
        this.isLabelEnabled=true;
        this.isAmountControlDisabled=false;
        this.isControlDisabled=false;
    });
//=====================Amount Requested======================================================================


    this.amountSubscription = this.loginAccountService.amountSubject.subscribe((message) => {
      this.requestedAmount = message;
      console.log("Amount Requested : ", this.requestedAmount);
      this.showpopup = false;
    });

//==================================Confirm Amount============================================================

    this.confirmAmountSubscription = this.loginAccountService.confirmAmountSubject.subscribe((message) => {
      console.log("Confirm Amount..! ", message);
      this.showpopups = false;
      //trail purpose
      this.isAmountControlDisabled=true;

      setTimeout(() => {

        this.showpopups1 = true;
      }, 1000);
      console.log("Pop up for signature", this.showpopups1);
      this.loginAccountService.sendSignature();
    });

    this.NoConfirmAmountSubscription =this.loginAccountService.noPaymentSubject.subscribe((message)=>{
      console.log("No Confirm Amount");
      this.noCarddata=true;
    });

//========================================Signature ===========================================================

    this.signatureSubscription = this.loginAccountService.signatureSubject.subscribe((message) => {
      console.log("Signature: ", message);
      this.serverSignature=message;
      this.showpopups1 = false;
      setTimeout(() => {
        this.showpopups2 = true;
      }, 1000);
      this.loginAccountService.sendReceipt();
    });

//========================================Payment Succesfull ===================================================

    this.finalSubscription = this.loginAccountService.finalSubject.subscribe((message) => {
      console.log("Payment Successful", message);
      this.showpopups2 = false;
      setTimeout(()=>{
        this.showPopup4 = true;
        this.ServerSig=this.serverSignature;
      },1000);
    });

//==================================================================================================================

    this.subcription = this.transactionService.cardDetailsSub.subscribe((cardData: CardData) => {
      this.feeType = 'Standard';
      this.payoutType = 'Cash';
      this.isControlDisabled = this.isAmountControlDisabled = false;
      this.isLabelEnabled = true;
      this.patronService.transactionStatus.next("Please verify the patron's amount request");
      if (this.transactionService.retrieveCardNumber.find(x => x === cardData.cardNumber)) {
        this.initiateRetrievalFlow();
      } else {
        this.initiateNewTransactionFlow();
      }
    });
    this.ingenicoWebsocketService.messages.subscribe((data: IngenicoMessage) => {
      this.handleMessageResponse(data);
    });
  }

//==================================================================================================================

showSadEmojis: boolean = false;
showHappyEmojis: boolean = false;

handleTransactionSuccessE():void{
  this.transactionSuccessful = true;
  this.showHappyEmojis = true;

  setTimeout(() => {
    this.transactionSuccessful = false;
    this.showPopup4 = false;
    this.showHappyEmojis = false;

  }, 1000);
}

  handlePopupClose(): void {
    this.showSadEmojis = true;

    setTimeout(() => {

    this.showPopup4 = false;
this.showHappyEmojis = false;
  }, 1000);}


//=================================================================================================================
//=================================================================================================================

showSadEmoji: boolean = false;
showHappyEmoji: boolean = false;

handleTransactionSuccessS(): void {
  this.popupContent = 'Transaction Closed';
  this.transactionSuccessful = true;
  this.showHappyEmoji = true;

  setTimeout(() => {
    this.noCarddata = false;
    this.showpopups = false;
    this.transactionSuccessful = false;
    this.showHappyEmoji = false;

  }, 1000);
}

handleTransactionNo(): void {
  this.sendConfirmAmout();
  this.showSadEmoji = true;
  setTimeout(() => {
    this.noCarddata = false;
    this.showSadEmoji = false;
  }, 1000);}

sendConfirmAmout() {
  this.loginAccountService.sendConfirmAmout();
}

//======================================================================================================================

  onConfirm(amountDetails: NgForm) {
    this.loginAccountService.sendConfirmAmout();
    this.showpopups = true;
  amountDetails.form.disable();
    // if (!this.isRetrieval)
    //   this.flexChargeFLow = this.transactionService.approvalLimit < parseInt(this.requestedAmount) ? true : false;

    // if (this.isRetrieval) {
    //   this.sendConfirmFee(false);
    // } else {
    //   if (this.transactionService.cardDetails.entryMode === 'Cless') {
    //     console.log('going to send continuewithflow from onConfirm');
    //     this.sendContinueWithFlow();
    //   } else {
    //     // this.RequestedAmount = amountDetails.controls.requestedAmount.getRawValue();
    //     this.sendConfirmFee(true);
    //   }
    // }
  }

  handleTransactionSuccess() {
    this.transactionSuccessful = true;
}
  handleMessageResponse(data: IngenicoMessage) {
    this.modalPopupService.hideIngenicoModal();
    switch (data.commandName) {
      case 'CNFFEE':
        console.log("Transaction Type Check", this.transactionService.cardDetails.transactionType);
        if (this.isRetrieval) {
          console.log("Oinside COnfirm Fee, Rettrievel");
          this.sendEndAuthSuccess();
        } else {
          console.log("sendDccSta");
          this.sendDccSta();
        }
        break;
      case 'EDAUTH':
        if (this.flexChargeFLow) {
          this.sendFlexCharge();
        }
        else {
          this.sendECAConfirm();
        }
        break;
      case 'CNFECA':
        this.sendEndTransaction();
        break;
      case 'SELAPT':
        this.handleSelectAccountTypeResponse(data);
        break;
      case 'CNTWFL':
        this.handleContinueWithFlow(data);
        break;
      case "DCCSTA":
        this.sendOnlineAutorization();
        break;
      case "SNDONL":
        this.sendEndAutorization();
        break;
      case 'ETRAMT':
        this.handleEnterAmount(data);
        break;
      case "CNFFLC":
        this.flexChargeSuccess(data);
        break;
      case "ENDFLC":
        this.sendECAConfirm();
        break;
      default:
        break;
    }
  }

  handleEnterAmount(data: IngenicoMessage) {
    if (data) {
      console.log(JSON.stringify(data));
      if (data.commandResponseData.length > 0) {
        let ingenicoResponse: IngenicoCommandData[] = JSON.parse(data.commandResponseData);
        let amount = ingenicoResponse.find(x => x.key == 'AMOUNT').val;
        if (amount) {
          this.requestedAmount = (parseFloat(amount) / 100).toString();
          this.onAmountChanged(null);
        }
      }
    }
  }

  handleContinueWithFlow(data: IngenicoMessage) {
    if (this.transactionService.cardDetails.entryMode === 'Cless') {
      this.sendConfirmFee(true);
    } else {
      if (data) {
        console.log(JSON.stringify(data));
        if (data.commandResponseData.length > 0) {
          let ingenicoResponse: IngenicoCommandData[] = JSON.parse(data.commandResponseData);
          let transactionType = ingenicoResponse.find(x => x.key == 'PAYMENTTYPE').val;
          if (transactionType) {
            this.transactionService.cardDetails.transactionType = ingenicoResponse.find(x => x.key == 'PAYMENTTYPE').val;
            this.transactionService.publishCardData();
          }
        }
      }
    }
  }

  sendSelDac() {
    setTimeout(() => {
      let message: IngenicoMessage = {
        commandName: 'SELDAC',
        commandRequestData: this.commandDataProvider.getCommandData('SELDAC', null),
        commandResponseData: ""
      };

      console.log("Request : ", message);
      this.ingenicoWebsocketService.messages.next(message);
    }, 2000);
  }

  sendDccSta() {
    let message: IngenicoMessage = {
      commandName: 'DCCSTA',
      commandRequestData: this.commandDataProvider.getCommandData('DCCSTA', null),
      commandResponseData: ""
    };
    console.log("Request : ", message);
    this.ingenicoWebsocketService.messages.next(message);
  }

  sendFlexCharge() {
    setTimeout(() => {
      let message: IngenicoMessage = {
        commandName: 'CNFFLC',
        commandRequestData: this.commandDataProvider.getCommandData('CNFFLC', null),
        commandResponseData: ""
      };
      console.log("Request : ", message);
      this.ingenicoWebsocketService.messages.next(message);
    }, 2000);
  }

  flexChargeSuccess(responseData: IngenicoMessage) {
    console.log('At FlexChargeSuccess');
    let valueData: String;
    this.modalPopupService.hideIngenicoModal();

    if (responseData && responseData.commandResponseData.length > 0) {
      let commandResponseData: IngenicoCommandData[] = JSON.parse(responseData.commandResponseData);
      if (commandResponseData.find(x => x.key === "ResponseCode")) {
        valueData = commandResponseData.find(x => x.key === "ResponseCode").val;
      }
    }

    if (valueData != "00") {
      this.modalPopupService.showTranStatusModal("Declined");
      let message = {
        commandName: 'EDTRAN',
        commandRequestData: "",
        commandResponseData: ""
      };
      this.ingenicoWebsocketService.messages.next(message);
    } else {
      this.modalPopupService.showLoadingScreenModal('FlexCharge Autorization In Progress..');

      setTimeout(() => {
        this.modalPopupService.hideLoadingScreenModal();
        this.modalPopupService.showTranStatusModal("Approved");

        let message: IngenicoMessage;

        message = {
          commandName: 'ENDFLC',
          commandRequestData: this.commandDataProvider.getCommandData('ENDFLC', null),
          commandResponseData: ""
        };
        this.ingenicoWebsocketService.messages.next(message);
      }, 2000);
    }
  }

  sendOnlineAutorization() {
    let message: IngenicoMessage = {
      commandName: 'SNDONL',
      commandRequestData: this.commandDataProvider.getCommandData('SNDONL', new Array("10234567", "true", "cv_anil"
        , "CA", "CV_123", this.requestedAmount)),
      commandResponseData: ""
    };
    this.modalPopupService.showLoadingScreenModal("Authorizing Transaction...");
    this.ingenicoWebsocketService.messages.next(message);
  }

  ngOnDestroy(): void {

    this.messageSubscription.unsubscribe();
    this.amountSubscription.unsubscribe();
    this.confirmAmountSubscription.unsubscribe();
    this.signatureSubscription.unsubscribe();
    this.finalSubscription.unsubscribe();

  }
  sendEndAutorization() {
    setTimeout(() => {
      let popValue = this.flexChargeFLow ? "Declined" : "Approved";
      this.modalPopupService.showTranStatusModal(popValue);

      let valuePush = this.flexChargeFLow ? "06" : "00";

      let message: IngenicoMessage = {
        commandName: 'EDAUTH',
        commandRequestData: this.commandDataProvider.getCommandData('EDAUTH', new Array(valuePush)),
        commandResponseData: ""
      };

      console.log("Request : ", message);
      this.ingenicoWebsocketService.messages.next(message);
      this.modalPopupService.hideLoadingScreenModal();
    }, 2000);
  }

  handleSelectAccountTypeResponse(data: IngenicoMessage) {
    if (data) {
      if (data.commandResponseData.length > 0) {
        let ingenicoResponse: IngenicoCommandData[] = JSON.parse(data.commandResponseData);
        let transactionType = ingenicoResponse.find(x => x.key == 'PAYMENTTYPE').val;
        if (transactionType) {
          this.transactionService.cardDetails.transactionType = ingenicoResponse.find(x => x.key == 'PAYMENTTYPE').val;
          this.transactionService.publishCardData();
        }
      }
    }
  }

  sendECAConfirm() {

    setTimeout(() => {
      this.modalPopupService.hideTranStatusModal();
      let message: IngenicoMessage = {
        commandName: 'CNFECA',
        commandRequestData: this.commandDataProvider.getCommandData('CNFECA', null),
        commandResponseData: ""
      };

      this.ingenicoWebsocketService.messages.next(message);
    }, 2000);
  }

  sendEndTransaction() {
    let message: IngenicoMessage = {
      commandName: 'PRINTRCP',
      commandRequestData: this.commandDataProvider.getCommandData('PRINTRCP', new Array(this.requestedAmount, this.feeAmount, this.totalAmount)),
      commandResponseData: ""
    };
    this.ingenicoWebsocketService.messages.next(message);
    this.modalPopupService.showLoadingScreenModal("Finalizing Transaction..");

    setTimeout(() => {
      this.ccAlertService.showAlert('Success', 'EC AML Posting - Success!', true);
    }, 1000);

    setTimeout(() => {
      this.modalPopupService.hideLoadingScreenModal();
     
    }, 1500);
    
    setTimeout(() => {
      let message: IngenicoMessage = {
        commandName: 'EDTRAN',
        commandRequestData: "",
        commandResponseData: ""
      };
     
      this.modalPopupService.showTransactionSummaryModal(parseFloat(this.requestedAmount).toFixed(2).toString(), 'Cash', this.transactionService.cardDetails.transactionType === 'PINLESS_CREDIT' ?
        'Credit' : 'Debit', this.feeType, this.feeAmount, this.totalAmount, 'Print');
      this.ingenicoWebsocketService.messages.next(message);
    }, 1500);
  }

  sendEndAuthSuccess() {
    this.modalPopupService.showLoadingScreenModal('Completing Transaction..');
    setTimeout(() => {
      this.modalPopupService.hideLoadingScreenModal();
      this.modalPopupService.showTranStatusModal('Approved');
      let message: IngenicoMessage = {
        commandName: 'EDAUTH',
        commandRequestData: "",
        commandResponseData: ""
      };

      this.ingenicoWebsocketService.messages.next(message);
    }, 2000);
  }

  initiateRetrievalFlow() {
    this.last4NotAvailable = this.isRetrieval = true;
    this.subcription.unsubscribe();
    this.transactionService.cardDetails.transactionMode = "Retrieval";
    this.transactionService.publishCardData();
    this.requestedAmount = this.transactionService.retrievalAmount.toString();
    this.onAmountChanged(null);
    this.isAmountControlDisabled = true;
  }

  initiateNewTransactionFlow() {
    this.subcription.unsubscribe();
    this.transactionService.cardDetails.transactionMode = "New Transaction";
    this.transactionService.publishCardData();
    if (this.transactionService.cardDetails.entryMode === "Cless") {
      this.sendSelectAccountType();
    } else {
      console.log('going to send continuewithflow from initiateNewTransactionFlow');
      this.sendContinueWithFlow();
    }
  }

  sendContinueWithFlow() {
    let commandData: string[] = [this.requestedAmount, this.feeAmount];
    let message: IngenicoMessage = {
      commandName: "CNTWFL",
      commandRequestData: this.commandDataProvider.getCommandData('CNTWFL',
        this.transactionService.cardDetails.entryMode === "Cless" ? commandData : null),
      commandResponseData: ""
    };

    this.ingenicoWebsocketService.messages.next(message);
  }

  sendSelectAccountType() {
    let message: IngenicoMessage = {
      commandName: "SELATP",
      commandRequestData: this.commandDataProvider.getCommandData('SELATP', null),
      commandResponseData: ""
    };

    this.ingenicoWebsocketService.messages.next(message);
  }

  sendConfirmFee(isAuthConfirmFee: boolean) {
    let commandData: string[] = [this.requestedAmount, this.feeAmount, String(isAuthConfirmFee)];
    let message: IngenicoMessage = {
      commandName: "CNFFEE",
      commandRequestData: this.commandDataProvider.getCommandData('CNFFEE', commandData),
      commandResponseData: ""
    };

    this.ingenicoWebsocketService.messages.next(message);
  }


 

  onAmountCapture() {
    this.showpopup=true;

    // setTimeout(() => {
    //   this.showpopup=false;
    // },7000);

    // let message: IngenicoMessage = {
    //   commandName: 'ETRAMT',
    //   commandRequestData: this.commandDataProvider.getCommandData('ETRAMT', null),
    //   commandResponseData: ""
    // };

    // this.ingenicoWebsocketService.messages.next(message);
  }

  onAmountChanged(event: Event) {
    //console.log('Fee type: ', this.feeType)
    if (this.feeType != null && this.feeType != ''
      && this.requestedAmount != null && this.requestedAmount != '' && this.requestedAmount != '0') {
      let feePercentage = 0.043;
      if (this.feeType == 'VIP') {
        feePercentage = 0.00;
      }
      let decimalValue = parseFloat(this.requestedAmount);
      let calculatedFee = decimalValue * feePercentage;

      this.totalAmount = decimalValue < 10 ? '$0' : '$';
      this.totalAmount = this.totalAmount + (decimalValue + calculatedFee).toFixed(2).toString();

      this.feeAmount = decimalValue < 10 ? '$0' : '$';
      this.feeAmount = this.feeAmount + (calculatedFee).toFixed(2).toString();
      //this.feeAmount = parseFloat(this.feeAmount).toFixed(2);
    }
    else if (this.feeType == null || this.requestedAmount == '0') {
      let decimalValue = parseFloat(this.requestedAmount);
      this.feeAmount = undefined;
      //console.log('Total Amount: ', this.requestedAmount)
      if (this.requestedAmount != null && this.requestedAmount != '') {
        this.totalAmount = decimalValue < 10 ? '$0' : '$';
        this.totalAmount = this.totalAmount + decimalValue.toFixed(2).toString();
        //this.totalAmount = parseFloat(this.totalAmount).toFixed(2);
      }
      else
      {
        this.totalAmount = '$00.00';
      }
    }
  }

  onNoLast4(event: Event) {
    console.log('on last 4 triggered');
    this.cardlast4Number = undefined;
  }
}