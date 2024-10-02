import { Component, ViewChild, OnInit, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import { LoginScreenTempService } from '../services/applicationServices/cc-loginscreentemp.service';
import { ModalPopupService } from '../services/modal-popup.service';
import { LoginAccountServiceService } from '../services/login-account-service.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'cc-login-screen',
  templateUrl: './login-screen.component.html',
  styleUrls: ['./login-screen.component.css']
})
export class LoginScreenComponent implements OnInit, OnDestroy {
  @ViewChild('ccuserlogin') loginForm: NgForm;
  ccuser: string;
  ccpassword: string;
  message: string = '';
  message2: string = '';
  Response:string='';
  private messageSubscription: Subscription;

  constructor(
    private loginscreenTempService: LoginScreenTempService,
    private modalPopupService: ModalPopupService,
    private loginAccountService: LoginAccountServiceService
  ) { }

  ngOnInit() {
  }

  ngOnDestroy() {
    if (this.messageSubscription) {
      this.messageSubscription.unsubscribe();
    }
  }
  onSearch(loginForm: NgForm) {
    this.message = '';
    this.message2 = '';

    this.loginAccountService.sendMessage(this.ccuser, this.ccpassword);
    
    this.loginForm.form.disable();
    this.modalPopupService.showLoadingScreenModal('Validating user...');
    this.messageSubscription = this.loginAccountService.loginSubject.subscribe((response) => {

      console.log("This is server",response);
      this.Response=response;
      if (response === "true") {
        setTimeout(() => {
          this.modalPopupService.hideLoadingScreenModal();
          this.loginscreenTempService.showMainScreen(true);
        }, 3000);
      } else if (response === "Invalid UserName") {
        this.modalPopupService.hideLoadingScreenModal();
        this.loginForm.form.enable();
        this.message = "Invalid UserName";
      } else if (response === "Invalid password") {
        this.modalPopupService.hideLoadingScreenModal();
        this.loginForm.form.enable();
        this.message2 = "Invalid Password";
      }
    });
  }
}
