import { Component } from '@angular/core';
import { PatronService } from '../services/applicationServices/patron.service';
import { ESeekWebSocketService } from '../services/webSocketServices/eseek.websocket.service';
import { PatronSearchData } from '../entities/patronSearchData';

@Component({
  selector: 'cc-transactionwindow',
  templateUrl: './transactionwindow.component.html',
  styleUrls: ['./transactionwindow.component.css']
})
export class TransactionwindowComponent {

  enableAmount = false;

  constructor(private eseekWebSocketService: ESeekWebSocketService, private patronService: PatronService) {

    this.eseekWebSocketService.messages.subscribe((msg) => {
      let patronSearchData: PatronSearchData = JSON.parse(msg);
      this.patronService.patronData_Raw = JSON.parse(msg);
      this.patronService.patronSearchData.next(patronSearchData);
    });
  }
}
