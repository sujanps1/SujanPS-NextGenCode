import { Component } from '@angular/core';
import { MainScreenTempService } from '../services/applicationServices/cc-mainscreentemp.service';
import { IngenicoWebsocketService } from '../services/webSocketServices/ingenico.websocket.service';

@Component({
  selector: 'cc-home-screen',
  templateUrl: './home-screen.component.html',
  styleUrls: ['./home-screen.component.css']
})
export class HomeScreenComponent {

  constructor(private mainscreenTempService: MainScreenTempService, private WebsocketService: IngenicoWebsocketService) { }

  onCashAdvanceClick() {
    this.sendEDTRAN();
    this.mainscreenTempService.showMainScreen(false);
  }

  onNoPClick() {
    console.log('No operation linked to button click');
  }

  onCheckCashingClick() {
   this.sendEDTRAN();
  }

  sendEDTRAN(){
    this.WebsocketService.messages.next({
        commandName: 'EDTRAN',
        commandRequestData: '',
        commandResponseData: ''
    });
  
    console.log('EDTRAN sent')
  }
}