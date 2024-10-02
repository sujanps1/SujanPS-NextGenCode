import { Injectable, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginAccountServiceService implements OnDestroy {
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
  private writable:any=null;

private transport:any=null;

  constructor() {
    this.connectWebTransport();
  }
  private async connectWebTransport() {
    try {
        const url = 'https://localhost:5001/webtransport';
        this.transport = new (window as any).WebTransport(url);
        await this.transport.ready;
        console.log('WebTransport connection established');
        this.ConnectionClosed.next("false"); 
        this.isConnected = true;
        const stream = await this.transport.createBidirectionalStream();
        const reader = stream.readable.getReader();
        this.writable = stream.writable.getWriter();

        this.readData(reader);

        this.transport.closed.then(() => {
            console.log('WebTransport connection closed');
            this.ConnectionClosed.next("true"); 
            this.isConnected = false;
            this.showConnectionClosedPopup = true;
        });
    } catch (error) {
        this.ConnectionClosed.next("true"); 
        console.error('WebTransport connection failed:', error);
        this.showConnectionClosedPopup = true;
    }
}

  private async readData(reader: ReadableStreamDefaultReader) {
    while (true) {
      const { value, done } = await reader.read();
      if (done) {
        break;
      }
  
      const decoder = new TextDecoder();
      const decodedString = decoder.decode(value);
     
      this.handleIncomingMessage(decodedString);
      console.log(decodedString);
    }
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
    if (this.transport) {
      const dataToSend = {
        type: 'Login',
        user: user,
        password: password,
      };
      const encoder = new TextEncoder();
      const message = encoder.encode(JSON.stringify(dataToSend));
  
      this.writable.write(message);    
    }
  }

  sendLanguage() {
    if (this.transport) {
      const dataToSendLanguage = {
        type: 'Language',
        message: 'Select Only one language',
      };
      this.sendData(dataToSendLanguage);
    }
  }

  sendSSN() {
    if (this.transport) {
      const dataToSendSSN = {
        type: 'SSN',
        message: 'Enter 10 Numbers :',
      };
      this.sendData(dataToSendSSN);
    }
  }


  sendCardDetails() {
    if (this.transport) {
      const dataToSendCard= {
        type: 'Card',
        message: 'Card Details !',
      };
      this.sendData(dataToSendCard);
    }
  }

  sendAmount(){
    if (this.transport) {
      const dataToSendAmount = {
        type: 'Amount',
        message: 'Enter Amount',
        };
        this.sendData(dataToSendAmount);
        }
  }
  sendConfirmAmout(){
    if (this.transport) {
      const dataToSendConfirmAmount = {
        type: 'ConfirmAmount',
        message: 'Confirm Your Amount...?',
        };
        this.sendData(dataToSendConfirmAmount);
  }
}
  sendSignature(){
    console.log("sdfsdfdsfdsf sogmtoerm");
    if (this.transport) {
      const dataToSendSignature = {
        type: 'Signature',
        message: 'Signature Required',
        };
        this.sendData(dataToSendSignature);
        }
  }
  sendReceipt(){
    console.log("this is one time");
    if (this.transport) {
      const dataToSendReceipt= {
        type: 'Receipt',
        message: 'Receipt Generating..',
        };
        this.sendData(dataToSendReceipt);
      }
}
sendSearchinfo(patroninfo){
  this.SearchObject=patroninfo;
  console.log("search info");
  if (this.transport) {
    const dataToSendSearchInfo = {
      type: 'SendSearchinfo',
      patroninformation:patroninfo,
      };
      this.sendData(dataToSendSearchInfo);
      }
}

Addpatroninfo(patroninfo){
  console.log(patroninfo,"this is body");
  if (this.transport) {
    const dataToSendSearchInfo = {
      type: 'Patroninformation',
      patroninformation:patroninfo,
      };
      this.sendData(dataToSendSearchInfo);
    }
}

private sendData(data: any) {
  const encoder = new TextEncoder();
  const message = encoder.encode(JSON.stringify(data));
  this.writable.write(message);
}
enableSearch(){
  console.log("Search Enable..!");
this.EnableSubject.next("enable");
}

ngOnDestroy() {
  if (this.transport) {
    this.transport.close();
  }
  if (this.popupTimer) {
    clearTimeout(this.popupTimer);
  }
}

}
