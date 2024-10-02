import { NgModule, CUSTOM_ELEMENTS_SCHEMA, APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgSelectModule } from '@ng-select/ng-select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { TransactionwindowComponent } from './transactionwindow/transactionwindow.component';
import { TransactionDetailComponent } from './transaction-detail/transaction-detail.component';
import { PatronSearchComponent } from './patron-search/patron-search.component';
import { AmountDetailsComponent } from './amount-details/amount-details.component';
import { CcToggleSwitchComponent } from './cc-controls/cc-toggle-switch/cc-toggle-switch.component';
import { CcTextboxComponent } from './cc-controls/cc-textbox/cc-textbox.component';
import { CcDropdownComponent } from './cc-controls/cc-dropdown/cc-dropdown.component';
import { PatronInformationComponent } from './patron-information/patron-information.component';
import { TransactionSummaryModalComponent } from './modals/transaction-summary-modal/transaction-summary-modal.component';
import { IngenicoModalComponent } from './modals/ingenico-modal/ingenico-modal.component';
import { LoadingScreenModalComponent } from './modals/loading-screen-modal/loading-screen-modal.component';
import { TransactionApprovedModalComponent } from './modals/transaction-status-modal/transaction-status-modal.component';
import { IngenicoWebsocketService } from './services/webSocketServices/ingenico.websocket.service';
import { IngenicoCommandDataService } from './services/webSocketServices/ingenicoCommandData.service';
import { ScreenMovementWebSocketService } from './services/webSocketServices/screenMovement.websocket.service';
import { ESeekWebSocketService } from './services/webSocketServices/eseek.websocket.service';
import { PatronService } from './services/applicationServices/patron.service';
import { CCInputValidationDirective } from './directives/input-validation.directive';
import { NgxCurrencyDirective } from 'ngx-currency';
import { TransactionService } from './services/applicationServices/transaction.service';
import { CCAlertService } from './services/applicationServices/cc-alert.service';
import { CcAlertComponent } from './cc-alert/cc-alert.component';
import { configFactory } from './factory/config.factory';
import { ConfigService } from './services/applicationServices/config.service';
import { HomeScreenComponent } from './home-screen/home-screen.component';
import { MainScreenTempService } from './services/applicationServices/cc-mainscreentemp.service';
import { LoginScreenComponent } from './login-screen/login-screen.component';
import { UppercaseInputDirective } from './directives/upper-case.directive';
import { LoginScreenTempService } from './services/applicationServices/cc-loginscreentemp.service';
import { ConnectionClosedPopupComponent } from './connection-closed-popup/connection-closed-popup.component';

// Define the routes separately and reference them in the imports
const routes: Routes = [
  { path: 'patron-information', component: PatronInformationComponent },
  { path: 'transaction-detail', component: TransactionDetailComponent },
  // other routes
];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    TransactionwindowComponent,
    TransactionDetailComponent,
    PatronSearchComponent,
    AmountDetailsComponent,
    CcToggleSwitchComponent,
    CcTextboxComponent,
    CcDropdownComponent,
    PatronInformationComponent,
    IngenicoModalComponent,
    TransactionSummaryModalComponent,
    LoadingScreenModalComponent,
    TransactionApprovedModalComponent,
    CCInputValidationDirective,
    CcAlertComponent,
    LoginScreenComponent,
    HomeScreenComponent,
    UppercaseInputDirective,
    ConnectionClosedPopupComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    NgSelectModule,
    BrowserAnimationsModule,
    NgxCurrencyDirective,
    HttpClientModule,
    RouterModule.forRoot(routes) // Correctly include routes here
  ],
  providers: [
    IngenicoWebsocketService,
    IngenicoCommandDataService,
    ScreenMovementWebSocketService,
    ESeekWebSocketService,
    PatronService,
    TransactionService,
    CCAlertService,
    MainScreenTempService,
    LoginScreenTempService,
    {
      provide: APP_INITIALIZER,
      useFactory: configFactory,
      deps: [ConfigService],
      multi: true
    }
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule {}
