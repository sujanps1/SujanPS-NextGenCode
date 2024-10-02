import { Component, Output, EventEmitter } from '@angular/core';
import { LoginAccountServiceService } from '../services/login-account-service.service';
import { ModalPopupService } from '../services/modal-popup.service';

@Component({
  selector: 'app-connection-closed-popup',
  templateUrl: './connection-closed-popup.component.html',
  styleUrls: ['./connection-closed-popup.component.css']
})
export class ConnectionClosedPopupComponent {
  @Output() closePopup = new EventEmitter<void>();
  constructor(
    private modalPopupService: ModalPopupService,
    private loginAccountService: LoginAccountServiceService,
  ) { }
  
  onClose() {
    window.location.reload();

  }
}
