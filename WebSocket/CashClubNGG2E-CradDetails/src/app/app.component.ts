import { Component, OnInit, HostListener } from '@angular/core';

import { MainScreenTempService } from './services/applicationServices/cc-mainscreentemp.service';
import { LoginScreenTempService } from './services/applicationServices/cc-loginscreentemp.service';
import { LoginAccountServiceService } from './services/login-account-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'CashClub-NextGen';
  // items : string[] = ['s','22'];
  showTransactionWindow: Boolean=false;
  showLoginScreen: Boolean = true;
  
  showConnectionClosedPopup:Boolean=false;

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any): void {
    $event.returnValue = 'If you refresh, you will be redirected to the login page.';
  }

  constructor(private mainscreenService: MainScreenTempService, private loginscreenService: LoginScreenTempService,private loginaccout:LoginAccountServiceService) {
  }

  ngOnInit(): void {
    this.mainscreenService.updatemainscreenSubject.subscribe((showMainScreen: Boolean) => {
      console.log(showMainScreen,"Main Screen");
      this.displayTransactionWindow(showMainScreen);
    });

    this.loginscreenService.updateloginscreenSubject.subscribe((showMainScreen: Boolean) => {
      this.displayMainScreen(showMainScreen);
    });

    this.loginaccout.ConnectionClosed.subscribe((message:string)=>{
      if(message=="true"){
        this.showConnectionClosedPopup=true;
      }
      else{
        this.showConnectionClosedPopup=false;

      }
    })
  }

  displayTransactionWindow(showMainScreen: Boolean) {
    this.showTransactionWindow = !showMainScreen;

    console.log(this.showTransactionWindow,"this is transcatin windo");

    //console.log('Subscription called in app components ts', showMainScreen);
    //console.log('show transaction window', this.showTransactionWindow);
  }

  displayMainScreen(showMainScreen: Boolean) {
    this.showLoginScreen = !showMainScreen;
    //console.log('Subscription called in app components ts', showMainScreen);
    //console.log('show transaction window', this.showTransactionWindow);
  }
}