import { Injectable, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginAccountServiceService implements OnDestroy {
  private socket: WebSocket;
  private isConnected: boolean = false;
  public SearchObject:any={};
  private messageSubject = new Subject<string>();
  private popupSubject = new Subject<{ message: string, isVisible: boolean }>
  popup$ = this.popupSubject.asObservable();
  message$ = this.messageSubject.asObservable();
  messagess = this.messageSubject.asObservable();
  public languageSubject: Subject<string> = new Subject<string>();
  public customerSubject: Subject<string> = new Subject<string>();
  public amountSubject: Subject<string> = new Subject<string>();
  public loginSubject: Subject<string> = new Subject<string>();
  public logoutSubject: Subject<string> = new Subject<string>();
  public confirmAmountSubject: Subject<string> = new Subject<string>();
  public signatureSubject: Subject<string> = new Subject<string>();
  public finalSubject: Subject<string> = new Subject<string>();
  public patroninfoSubject:Subject<any> = new Subject<string>();
  public nopatrnfoundSubject:Subject<any> = new Subject<string>();
  public noCardDataSubject:Subject<any> = new Subject<string>();
  public nossnSubject:Subject<any> = new Subject<string>();
  public noPaymentSubject:Subject<any> = new Subject<string>();
  public feesSubject: Subject<string> = new Subject<string>();
  public EnableSubject: Subject<string> = new Subject<string>();
  public ConnectionClosed: Subject<string> = new Subject<string>();


  public cardSubject: Subject<{ cardHolderName: string, bankName: string, cardType: string, cardNumber: string, transactionMode: string, transactionType: string }> = new Subject();
  response: string = '';
  private popupTimer: any;
  showConnectionClosedPopup = false;


  constructor() {
    this.socket = new WebSocket('ws://localhost:5258?token=1234');

    this.socket.onopen = () => {
      console.log('Connected to the server.');
      this.ConnectionClosed.next("false");
      this.isConnected = true;
    };

    this.socket.onmessage = (event) => {
      const message = event.data;
      this.handleIncomingMessage(message);
    };

    this.socket.onerror = (error) => {
      console.error('WebSocket error:', error);
    };

    this.socket.onclose = () => {
      console.log('Connection closed.');
      this.ConnectionClosed.next("true");
      this.isConnected = false;
    };

  }

  closePopup() {
    this.showConnectionClosedPopup = false;
  }

  private handleIncomingMessage(message: string) {
    const [type, ...rest] = message.split(':');
    const data = rest.join(':');
    console.log("maingfd",data);
    console.log("THIS IS THE DATA",type);

    switch (type) {
      case 'LOGIN':
        console.log(data);
        this.loginSubject.next(data);
        break;
      case 'LANGUAGE':
        this.languageSubject.next(data);
        break;
        case 'SSN':
        this.customerSubject.next(data);
        break;
        case 'NOSSN':
          this.nossnSubject.next(data);
          break;
      case 'PATRON':
        console.log(data,"this is body");
        this.patroninfoSubject.next({
          data: data,
          searchObject:this.SearchObject,
        });
        break;
        case 'Nopatrnfound':
          this.nopatrnfoundSubject.next({
            searchObject:this.SearchObject,
          });
          break;

        case 'CARD':
          this.parseCardData(data);
          console.log(data,"Card details");
          break;
        case 'NOCARD':
          this.noCardDataSubject.next(data);
          break;
        case 'AMOUNT':
          this.amountSubject.next(data);
          break;
        case 'CONFIRMAMOUNT':
          this.confirmAmountSubject.next(data);
          break;
        case 'NOPAYMENT':
          this.noPaymentSubject.next(data);
          break;
        case 'SIGNATURE':
          this.signatureSubject.next(data);
          break;
          case 'ERROR':
            console.log(data);
            this.signatureSubject.next(data);
            break;
        case 'FINAL':
          this.finalSubject.next(data);
        break;

      default:
        console.warn('Unknown message type:', type);
    }
  }
  private parseCardData(data: string) {
    console.log(data,"i a m nob");
    const [cardHolderName, bankName, cardType, cardNumber, transactionMode, transactionType] = data.split(',');
    const cardData = {
      cardHolderName,
      bankName,
      cardType,
      cardNumber,
      transactionMode,
      transactionType
    };
    this.cardSubject.next(cardData);
  }


  sendMessage(user: string, password: string) {
    if (this.socket && this.isConnected) {
      const dataToSend = {
        type: 'Login',
        user: user,
        password: password,
      };
      this.socket.send(JSON.stringify(dataToSend));
    }
  }

  sendLanguage() {
    if (this.socket && this.isConnected) {
      const dataToSendLanguage = {
        type: 'Language',
        message: 'Select Only one language',
      };
      this.socket.send(JSON.stringify(dataToSendLanguage));
    }
  }

  sendSSN() {
    if (this.socket && this.isConnected) {
      const dataToSendSSN = {
        type: 'SSN',
        message: 'Enter 10 Numbers :',
      };
      this.socket.send(JSON.stringify(dataToSendSSN));
    }
  }


  sendCardDetails() {
    if (this.socket && this.isConnected) {
      const dataToSendCard= {
        type: 'Card',
        message: 'Card Details !',
      };
      this.socket.send(JSON.stringify(dataToSendCard));
    }
  }

  sendAmount(){
    if (this.socket && this.isConnected) {
      const dataToSendAmount = {
        type: 'Amount',
        message: 'Enter Amount',
        };
        this.socket.send(JSON.stringify(dataToSendAmount));
        }
  }
  sendConfirmAmout(){
    if (this.socket && this.isConnected) {
      const dataToSendConfirmAmount = {
        type: 'ConfirmAmount',
        message: 'Confirm Your Amount...?',
        };
        this.socket.send(JSON.stringify(dataToSendConfirmAmount));
  }
}
  sendSignature(){
    console.log("sdfsdfdsfdsf sogmtoerm");
    if (this.socket && this.isConnected) {
      const dataToSendSignature = {
        type: 'Signature',
        message: 'Signature Required',
        };
        this.socket.send(JSON.stringify(dataToSendSignature));
        }
  }
  sendReceipt(){
    console.log("this is one time");
      const dataToSendReceipt= {
        type: 'Receipt',
        message: 'Receipt Generating..',
        };
        this.socket.send(JSON.stringify(dataToSendReceipt));
}
sendSearchinfo(patroninfo){
  this.SearchObject=patroninfo;
  console.log("search info");
  if (this.socket && this.isConnected) {
    const dataToSendSearchInfo = {
      type: 'SendSearchinfo',
      patroninformation:patroninfo,
      };
      this.socket.send(JSON.stringify(dataToSendSearchInfo));
      }
}

Addpatroninfo(patroninfo){
  console.log(patroninfo,"this is body");
  if (this.socket && this.isConnected) {
    const dataToSendSearchInfo = {
      type: 'Patroninformation',
      patroninformation:patroninfo,
      };
      this.socket.send(JSON.stringify(dataToSendSearchInfo));
      }
}

enableSearch(){
  console.log("Search Enable..!");
this.EnableSubject.next("enable");
}

  ngOnDestroy() {
    if (this.socket) {
      this.socket.close();
    }
    if (this.popupTimer) {
      clearTimeout(this.popupTimer);
    }
  }
}
