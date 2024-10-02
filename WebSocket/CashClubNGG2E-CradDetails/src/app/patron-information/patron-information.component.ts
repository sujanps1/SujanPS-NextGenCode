import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { PatronService } from '../services/applicationServices/patron.service';
import { DeviceCommand } from '../entities/Enumeration';
import { IngenicoWebsocketService } from '../services/webSocketServices/ingenico.websocket.service';
import { IngenicoCommandDataService } from '../services/webSocketServices/ingenicoCommandData.service';
import { ModalPopupService } from '../services/modal-popup.service';
import { PatronProfile } from '../entities/patronData';
import { IngenicoCommandData } from '../entities/ingenicoRequest';
import { TransactionService } from '../services/applicationServices/transaction.service';
import { CardData } from '../entities/cardDetails';
import { LoginAccountServiceService } from '../services/login-account-service.service';
import { Router } from '@angular/router';



@Component({
  selector: 'cc-patron-information',
  templateUrl: './patron-information.component.html',
  styleUrls: ['./patron-information.component.css'],
  // providers: [IngenicoWebsocketService]
})
export class PatronInformationComponent implements OnInit, OnDestroy {
  @ViewChild('patronInformation') piForm: NgForm;
  webSocketSubscription: Subscription;
  responseData:any;
  languageButtonText: string = 'Select Language';

  patronDataSubscription: Subscription;
  idTypesStaticData: string = './assets/datasets/IdTypes.json';
  idCountriesStaticData: string = './assets/datasets/Countries.json';
  idStatesStaticData: string = './assets/datasets/States.json';
  idCountries: [] = [];
  idStates: [] = [];
  selectedCountry: string;
  selectedState: string;
  formData = { name: '', email: '' };
  firstName: string;
  middleName: string;
  lastName: string;
  Country: string;
  postalCode: string;
  billingZipCOde: string;
  State: string;
  City: string;
  Street: string;
  AptSte: string;
  phoneNumber: string;
  emailAddress: string;
  Occupation: string;
  SSN: string;
  manualPlayerCard: string;
  swipedPlayerCard: string;
  Language: string
  isCaptureSSN: boolean;
  addaccountpop:boolean=false;
  isStartTran: boolean;
  sameBillingZipCode: boolean = true;
  isControlDisabled: boolean = true;
  private messageSubscription: Subscription;
  private patronSubscription: Subscription;
  private nopatroninfo:Subscription;
  popupMessage: string = '';
  isPopupVisible: boolean = false;
  deviceCommandSend: string = DeviceCommand.StartTransaction;
  showpopups : boolean=false;
  showpopup:boolean=false;
  buttonLabel="Swipe Card"
  showpopupSSN:boolean=false;
  buttonLabelSSN="SSN"
  showpopupLanguage :boolean=false;
  buttonLabelLanguage="Select Language"
  noCarddata:boolean=false;
  transactionSuccessful: boolean = false;
  popupContent: string = 'Patron Closing Transaction';
  searchobject:any={};
  onreachive:boolean=false;

  noSSNdata:boolean=false;

  popupContents: string = 'Patron Continue without SSN';


  constructor(public patronService: PatronService, private WebsocketService: IngenicoWebsocketService,
    private ingenicoCommandDataProvider: IngenicoCommandDataService,
    private modalPopupService: ModalPopupService, private transactionService: TransactionService,
    private loginAccountService: LoginAccountServiceService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.webSocketSubscription = this.WebsocketService.messages.subscribe(msg => {
      this.modalPopupService.hideIngenicoModal();
      // this.OnSuccessReciveCommand(msg.commandResponseData);
    });
 
    this.messageSubscription = this.loginAccountService.languageSubject.subscribe((message) => {

      this.Language = message;
      console.log("this is from language",this.Language);
      this.showpopupLanguage  = false;
    });
//======================== Card Details from server Start=============================

    this.messageSubscription = this.loginAccountService.cardSubject.subscribe((message) => {
      this.showpopups = false;
      // this.modalPopupService.hideLoadingScreenModal();
      // this.addaccountpop=true;

    });

    this.messageSubscription = this.loginAccountService.noCardDataSubject.subscribe((message) => {
      this.noCarddata=true;
      console.log("No Carda Data found");
    });

//===============================Card Details from server End=====================================

//===============================SSN Number Start===============================================
    this.messageSubscription = this.loginAccountService.customerSubject.subscribe((message) => {

      this.SSN = message;
      console.log("this is from ssn",this.SSN);
            this.showpopupSSN = false;

    });

    this.messageSubscription = this.loginAccountService.messagess.subscribe((mess) => {
      this.SSN = mess;
      console.log(this.SSN);
    });

    this.messageSubscription=this.loginAccountService.nossnSubject.subscribe((message)=>{
      this.noSSNdata=true;
      console.log("Not Sending SSN Data");
    });


// =========================================================================================


    this.patronSubscription=this.loginAccountService.patroninfoSubject.subscribe(value => {
    this.isControlDisabled=false;
    fetch(this.idCountriesStaticData).then(res => res.json()).then(json => {
      this.idCountries = json['Country'];
    });
    this.responseData=JSON.parse(value.data);
    console.log(value.searchObject.IDCountry,"Country");
    console.log(value.searchObject.IDState,"State");
    console.log(value,"this is value");
//=================Cash Advance API=============================================


  // this.firstName = this.responseData.GivenName;
  // this.middleName = this.responseData.MiddleName;
  // this.lastName = this.responseData.LastName;
  // this.Country = value.searchObject.IDCountry === 'USA' ? 'US' : (value.searchObject.IDCountry === 'CAN' ? 'CA' : value.searchObject.IDCountry);
  // this.State = value.searchObject.IDState;
  // this.postalCode = this.responseData.PostalCode;
  // //this.billingZipCOde = responseData.BillingPostalCode;
  // this.billingZipCOde = this.responseData.PostalCode;
  // this.City = this.responseData.City;
  // this.Street = this.responseData.Street;
  // this.AptSte = this.responseData.AptSte;
  // this.phoneNumber = this.responseData.PhoneNumber;
  // this.emailAddress = this.responseData.EmailAddress;
  // this.Occupation =this.responseData.Occupation;
  // this.SSN = this.loginAccountService.response;
  // this.manualPlayerCard =this.responseData.PlayerCardNumber;
  // this.swipedPlayerCard =this.responseData.SwipedPlayerCardNumber;
  // this.Language = this.loginAccountService.response;


//===================================================================
  
  this.firstName = this.responseData.firstName;
  this.middleName = this.responseData.middleName;
  this.lastName = this.responseData.lastName;
  this.Country = value.searchObject.IDCountry === 'USA' ? 'US' : (value.searchObject.IDCountry === 'CAN' ? 'CA' : value.searchObject.IDCountry);
  this.State = value.searchObject.IDState;
  this.postalCode = this.responseData.address.zip;
  //this.billingZipCOde = responseData.BillingPostalCode;
  this.billingZipCOde = this.responseData.address.zip;
  this.City = this.responseData.address.city;
  this.Street = this.responseData.address.addressLine1;
  this.AptSte = this.responseData.AptSte;
  this.phoneNumber = this.responseData.contact.phone;
  this.emailAddress = this.responseData.contact.email;
  this.Occupation =this.responseData.available.occupation;
  this.SSN = this.loginAccountService.response;
  this.manualPlayerCard =this.responseData.playerCardNumber;
  this.swipedPlayerCard =this.responseData.available.swipedPlayerCardNumber;
  this.Language = this.loginAccountService.response;
    });

    this.nopatroninfo = this.loginAccountService.nopatrnfoundSubject.subscribe((val) => {
      this.onreachive=true;
      fetch(this.idCountriesStaticData).then(res => res.json()).then(json => {
        this.idCountries = json['Country'];
      });

      console.log(val.searchObject.IDCountry,"Country");
      console.log(val.searchObject.IDState,"State");
      console.log(val,"this is value");
      this.Country = val.searchObject.IDCountry === 'USA' ? 'US' : (val.searchObject.IDCountry === 'CAN' ? 'CA' : val.searchObject.IDCountry);
      this.State = val.searchObject.IDState;
       this.searchobject=val.searchObject
      console.log("This is no Patron");
      this.isControlDisabled=false;

    });
//=====================================================================

    this.WebsocketService.messages.next({
      commandName: 'EDTRAN',
      commandRequestData: '',
      commandResponseData: ''
    });

  }

  //=================================================

showSadEmoji: boolean = false;
showHappyEmoji: boolean = false;

handleTransactionSuccess(): void {
  this.popupContent = 'Transaction Closed';
  this.transactionSuccessful = true;
  this.showHappyEmoji = true;
  setTimeout(() => {
    this.noCarddata = false;
    this.showpopups = false;
    this.loginAccountService.enableSearch();
    this.transactionSuccessful = false;
    this.showHappyEmoji = false;
  }, 1000);
}

handleTransactionNo(): void {
  this.sendCardDetails();
    this.showSadEmoji = true;
  setTimeout(() => {
    this.noCarddata = false;
    this.showSadEmoji = false;
  }, 1000);
}

sendCardDetails() {
  this.loginAccountService.sendCardDetails();
}

//==================================================
//==================================================
handleTransactionSuccessSSN(): void {
  this.transactionSuccessful = true;
  this.showSadEmoji = true;

  setTimeout(() => {
    this.noSSNdata = false;
    this.showpopupSSN = false;
    this.showSadEmoji = false;
    this.transactionSuccessful = false;
  }, 1000);
}

handleTransactionNoSSN(): void {
  this.sendSSN();
  this.noSSNdata = false;
}
sendSSN() {
  this.loginAccountService.sendSSN();
}

//===================================================
  ngOnDestroy(): void {
    this.webSocketSubscription.unsubscribe();
    this.patronDataSubscription.unsubscribe();
    this.messageSubscription.unsubscribe();
    this.patronSubscription.unsubscribe();

  }

  onChangeCountry(selectedCountry: string) {
    this.selectedState = null;
    //Load States to dropdown upon selection of country
    fetch(this.idStatesStaticData).then(res => res.json()).then(json => {
      this.idStates = json['State'].filter((item) => {
        return item.CountryCode == selectedCountry;
      });
    });
  }

  onChangeState(selectedStateData: { isSelected: boolean, validValue: string }) {
    this.State = selectedStateData.validValue;
  }



  //=======================================Cash Advance API=========================================================
// const constructRequestBody = {
//   RequestInfo: {
//     VersionInfo: "6.7.0.0",
//     BuildNumber: "6.6.8.0.20230725",
//     SecurityKey: "E+gdW+ibrEM4T78WQelR7DU/p3Ul7u93dZv6SXfk9PA=",
//     TerminalID: "NVGCAC05",
//     MerchantSID: 334,
//     Region: "NorthAmerica",
//     ProductName: "CashClub",
//     Operator: "karthik",
//     SequenceNumber: "00000083",
//     RequestToken: "PS003"
//   },
//   RequestBody: {
//   IDCountry: this.searchobject.IDCountry,
//   IDStateProvince: this.searchobject.IDState,
//   IDType:  this.searchobject.IDType,
//   IDNumber:  this.searchobject.IDNumber,
//   IDDob:  this.searchobject.BirthDate,
//   IDExpirationDate: this.searchobject.IDExpiryDate,
//   ExpiryDateChecked: false,
//   IDReadMethod: 2,
//   FirstName: this.firstName,
//   MiddleName: this.middleName,
//   Lastname: this.lastName,
//   Country: this.searchobject.IDCountry,
//   ZipCode: this.postalCode,
//   BillingZipCode: this.billingZipCOde,
//   State:  this.searchobject.IDState,
//   City: this.City,
//   Street: this.Street,
//   AptNo: this.AptSte,
//   DOB: this.searchobject.BirthDate,
//   CellPhoneNo: this.phoneNumber,
//   PlayerCardNo: this.manualPlayerCard,
//   PlayerCardType: null,
//   Occupation: this.Occupation,
//   SwipedPlayerCardNo:null,
//   PlayerCardNoTrackData: "",
//   PlayerCardPin: null,
//   Email:this.emailAddress,
//   OptOut: false,
//   PatronID: 0,
//   PreferredLanguage:this.Language,
//   UnIdentifiedIDDesc: null,
//   PlayerCardBrand: 0,
//   DisplayPlayerCardNo: null,
//   SSN:this.SSN,
//   SSNLastFour: "",
//   HashedSSN: null,
//   EncryptedSSN: null,
//   ClubID: 0,
//   IdentificationId: 0,
//   PatronCardBrandID: 1,
//   PatronCardType: null,
//   PatronCardNumber: null,
//   PatronHashedCardNumber: null,
//   PatronEncryptedCardNumber: null,
//   PatronCardNumberLastFour: null,
//   PatronCardExpiryDate: this.searchobject.IDExpiryDate,
//   PatronCardHolderName: null,
//   CCUserName:"karthik",
//   PrivacyOptInChecked: null,
//   SendPrivacyTemplateToEmail: false,
//   WalletTCToBeSent: null,
//   IsFetchFeeProfiles: true,
//   RemovePlayerCard: 0,
//   SkipAddressCheck: false
//   }
// };
// this.loginAccountService.Addpatroninfo(constructRequestBody);
//====================================================================================

//===================================CAPS API =========================================

onConfirm(patronInformation: NgForm) {
  if(this.onreachive){
const constructRequestBody = {

  patronId:  this.searchobject.IDNumber,
  firstName: this.firstName,
  middleName: this.middleName,
  lastName: this.lastName,
  dateOfBirth: this.searchobject.BirthDate,
  expiryDate:this.searchobject.IDExpiryDate,
  type: this.searchobject.IDType,
  number:this.searchobject.IDNumber,
  addressLine1:this.Street,
  addressLine2: this.AptSte,
  city: this.City,
  state: this.searchobject.IDState,
  zip: this.postalCode,
  country: this.searchobject.IDCountry,
  email:this.emailAddress,
  phone:this.phoneNumber,
  occupation: this.Occupation,
  payercardno: this.manualPlayerCard,
  ssn:this.SSN,
  preferredLanguage:this.Language,

//   transactionSource: "CC",
//   playerCardNumber: this.manualPlayerCard,
//   patronId:  this.searchobject.IDNumber,
//   firstName: this.firstName,
//   middleName: this.middleName,
//   lastName: this.lastName,
//   dateOfBirth: this.searchobject.BirthDate,
//   ssn:this.SSN,
//   identity: {
//     type: this.searchobject.IDType,
//     number: this.searchobject.IDNumber,
//     state: this.searchobject.IDState,
//     country:this.searchobject.IDCountry,
//     issuedDate: "",
//     expiryDate:this.searchobject.IDExpiryDate,
//     issuedName: "",
//     idReadMethod: "",
//     idValidatedDate: "",
//     idMissionFlag: "",
//     isDefaultId: true
//   },
//   address: {
//     addressLine1:this.Street,
//     addressLine2:this.AptSte,
//     city: this.City,
//     state: this.searchobject.IDState,
//     zip:this.postalCode,
//     country:this.searchobject.IDCountry,
//     preferredAddress: true
//   },
//   skipAddressCheck: true,
//   contact: {
//     email:this.emailAddress,
//     phone:this.phoneNumber
//   },
//   performKYC: true,
//   modifyBy: "",
//   fTerminalId: "",
//   encryptionType: 0,
//   encryptedSSN: "",
//   ksn: "",
//   serialNumber: "",
//   occupation:this.Occupation,
//   idReadMethod: "",
//   swipedPlayerCardNumber: "",
//   preferredLanguage:this.Language,
//   preferredReceiptDate: "",
//   preferredReceiptSelection: "",
//   flexChargeTCVersion: "",
//   flexChargeTCAcceptedDate: "",
//   walletTCVersion: "",
//   walletTCAcceptedDate: "",
//   userSID: 0,
//   optInState: ""
// };
};
this.loginAccountService.Addpatroninfo(constructRequestBody);
}
//=====================================================================================


  this.loginAccountService.sendCardDetails();
    this.showpopups = true;

  patronInformation.form.disable();
  console.log(patronInformation);
    if (this.isCaptureSSN) {
      setTimeout(() => {
        this.onSendCommand(DeviceCommand.StartAuthorisation);
      }, 2000);
    } else {
      setTimeout(() => {
        this.onSendCommand(DeviceCommand.StartTransaction);
      }, 2000);
    }
  }
  
//=========================================================================

  onBillingToggle(event: Event) {
    if (this.sameBillingZipCode) {
      this.billingZipCOde = this.postalCode;
    }
  }

  onPostalCodeEntered(event: Event) {
    if (this.sameBillingZipCode) {
      this.billingZipCOde = this.postalCode;
    }

    if (this.postalCode == undefined || this.postalCode == '') {
      this.billingZipCOde = undefined;
    }
  }

  onSendCommand(ingenicoCommand: string, dataParsing?: string[]) {
    console.log("Device Command: {0}, data: {1}", ingenicoCommand, dataParsing);
    this.deviceCommandSend = ingenicoCommand;
    let message = {
      commandName: ingenicoCommand,
      commandRequestData: this.ingenicoCommandDataProvider.getCommandData(ingenicoCommand, dataParsing),
      commandResponseData: ''
    };

    console.log(message);
    this.WebsocketService.messages.next(message);
  }

  // onFirstNameEntered(receivedData: { validValue: string }) {
  //   this.firstName = receivedData.validValue;
  // }

  // onMiddleNameEntered(receivedData: { validValue: string }) {
  //   this.middleName = receivedData.validValue;
  // }

  // OnSuccessReciveCommand(commandResponseData: string) {
  //   console.log("DeviceCommand", this.deviceCommandSend);
  //   switch (this.deviceCommandSend) {

  //     case DeviceCommand.StartTransaction:
  //       this.isStartTran = true;
  //       // this.onSendCommand(DeviceCommand.SelectLanguage, new Array("true", "English"));
  //       break;

  //     case DeviceCommand.SelectLanguage:
  //       if (this.isCaptureSSN) {
  //         this.onSendCommand(DeviceCommand.CaptureSSN)
  //       }
  //       else {
  //         console.log(commandResponseData.toString());
  //         this.onSendCommand(DeviceCommand.StartAuthorisation);
  //       }
  //       break;

  //     case DeviceCommand.StartAuthorisation:
  //       console.log(commandResponseData.toString());
  //       this.handleStartAuthResponse(commandResponseData);
  //       break;

  //     case DeviceCommand.CaptureSSN:
  //       console.log("Capture SSN response Received");
  //       this.modalPopupService.hideIngenicoModal();
  //       this.handleSSNResponse(commandResponseData);
  //       break;
  //   }
  // }

  handleSSNResponse(responseData: string) {
    if (responseData && responseData.length > 0) {
      let commandResponseData: IngenicoCommandData[] = JSON.parse(responseData);
      console.log("SSN Data", commandResponseData);
      if (commandResponseData.find(x => x.key === "SSN")) {
        this.SSN = commandResponseData.find(x => x.key === "SSN").val;
      }
    }
  }


  handleStartAuthResponse(responseData: string) {
    if (responseData && responseData.length > 0) {
      let commandResponseData: IngenicoCommandData[] = JSON.parse(responseData);
      if (commandResponseData && commandResponseData.length > 0) {
        let cardData: CardData = new CardData();
        if (commandResponseData.find(x => x.key === "BANKNAME")) {
          cardData.bankName = commandResponseData.find(x => x.key === "BANKNAME").val;
        }
        if (commandResponseData.find(x => x.key === "CARDNO")) {
          cardData.cardNumber = commandResponseData.find(x => x.key === "CARDNO").val;
        }
        if (commandResponseData.find(x => x.key === "CARDTYPE")) {
          cardData.cardType = commandResponseData.find(x => x.key === "CARDTYPE").val;
        }
        if (commandResponseData.find(x => x.key === "CHOLDERNAME")) {
          cardData.cardHolderName = commandResponseData.find(x => x.key === "CHOLDERNAME").val;
        }
        if (commandResponseData.find(x => x.key === "ENTRYMODE")) {
          cardData.entryMode = commandResponseData.find(x => x.key === "ENTRYMODE").val;
        }

        this.transactionService.cardDetails = cardData;
        this.transactionService.publishCardData();
      }
    }
  }
  onSSNCapture() {
    this.showpopupSSN = true;
    if (this.isCaptureSSN) {
      this.onSendCommand(DeviceCommand.CaptureSSN)
    }
    else {
      this.isCaptureSSN = true;
      setTimeout(() => {
        this.isCaptureSSN= false;
      }, 5000);
      this.deviceCommandSend = DeviceCommand.StartTransaction;
      this.onSendCommand(DeviceCommand.StartTransaction);
    }
  }
  onLanguageCapture(){
    this.showpopupLanguage  = true;
  }
}

interface KeyValuePairs {
  [key: string]: string;
}

function getValueByKey(key: string, data: KeyValuePairs): string | undefined {
  return data[key];
}