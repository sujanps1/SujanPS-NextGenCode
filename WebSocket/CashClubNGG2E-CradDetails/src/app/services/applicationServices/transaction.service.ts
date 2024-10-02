import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { CardData } from "src/app/entities/cardDetails";
import { TransactionDetail } from "src/app/entities/transactionDetails";
import { ConfigService } from "./config.service";

@Injectable()
export class TransactionService {
    cardDetails: CardData;
    cardDetailsSub: Subject<CardData> = new Subject();
    retrieveCardNumber: string[] = ["5569..6187"];//"3742..1007";
    transactionDetail: TransactionDetail;
    approvalLimit: number;
    retrievalAmount: number;
    private transactionData: CardData;


    constructor(private configService: ConfigService) {
        this.cardDetails = new CardData();
        this.transactionDetail = new TransactionDetail();
        this.retrieveCardNumber = configService.config.retrievalCardNumber.split(';');
        this.approvalLimit = parseInt(configService.config.approvalLimit);
        this.retrievalAmount = parseInt(configService.config.retrieveAmount);
    }
    setTransactionData(data: CardData): void {
        this.transactionData = data;
      }
    
      getTransactionData(): CardData {
        return this.transactionData;
      }
    publishCardData() {
        this.cardDetailsSub.next(this.cardDetails);
        console.log("cardDetails", this.cardDetails);
    }
}