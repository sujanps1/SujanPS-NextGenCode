import { Component, Input } from "@angular/core";
import { ESeekWebSocketService } from "../services/webSocketServices/eseek.websocket.service";
import { IngenicoWebsocketService } from "../services/webSocketServices/ingenico.websocket.service";
import { IngenicoCommandDataService } from "../services/webSocketServices/ingenicoCommandData.service";
import { ScreenMovementWebSocketService } from "../services/webSocketServices/screenMovement.websocket.service";
import { MainScreenTempService } from "../services/applicationServices/cc-mainscreentemp.service";

@Component({
    selector: 'cc-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})

export class HeaderComponent {
    @Input() tid: string = 'NVGCAC92';
    @Input() merchant: string = 'EVI*EVERI HQ';
    @Input() version: string = '7.0.0.1';
    @Input() showCancel: boolean = false;

    constructor(private WebsocketService: IngenicoWebsocketService,
        private ingenicoCommandDataProvider: IngenicoCommandDataService,
        private screenWebSocketService: ScreenMovementWebSocketService,
        private eseekWebSocketService: ESeekWebSocketService,
        private mainscreenTempService: MainScreenTempService) {
    }

    onCancel() {
        this.WebsocketService.messages.next({
            commandName: 'EDTRAN',
            commandRequestData: '',
            commandResponseData: ''
        });

        //this.screenWebSocketService.messages.next("Cancel");

        this.mainscreenTempService.showMainScreen(true);
    }

    onSendCommand(ingenicoCommand: string) {
        let message = {
            commandName: this.deviceCommandSend,
            commandRequestData: this.ingenicoCommandDataProvider.getCommandData(this.deviceCommandSend, new Array("true", "English")),
            commandResponseData: ''
        };

        console.log(message);
        // message.commandName = this.deviceCommandSend;
        // message.commandRequestData = this.ingenicoCommandDataProvider.getCommandData(message.commandName);

        this.WebsocketService.messages.next(message);
    }

    deviceCommandSend: string;

    onSelected(e): void {
        console.log(e.target.value)
        this.deviceCommandSend = e.target.value;
        console.log(this.deviceCommandSend);
        this.onSendCommand(this.deviceCommandSend);
    }

    public deviceCommand: string[] = ['STTRAN', 'SELLANG', , 'STAUTH', 'DCCSTA', 'CNFDCC', 'CNFFEE', 'EDTRAN'];
}