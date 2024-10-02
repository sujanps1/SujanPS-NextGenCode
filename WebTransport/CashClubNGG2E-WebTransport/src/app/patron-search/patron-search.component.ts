import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { PatronSearchData } from '../entities/patronSearchData';
import { PatronService } from '../services/applicationServices/patron.service';
import { ModalPopupService } from '../services/modal-popup.service';
import { PatronProfile } from '../entities/patronData';
import { LoginAccountServiceService } from '../services/login-account-service.service';

@Component({
  selector: 'cc-patron-search',
  templateUrl: './patron-search.component.html',
  styleUrls: ['./patron-search.component.css']
})

export class PatronSearchComponent implements OnInit {
  @ViewChild('patronSearch') psForm: NgForm;
  idTypesStaticData: string = './assets/datasets/IdTypes.json';
  idCountriesStaticData: string = './assets/datasets/Countries.json';
  idStatesStaticData: string = './assets/datasets/States.json';
  idTypes: [] = [];
  idCountries: [] = [];
  idStates: [] = [];
  selectedIdType: string;
  selectedCountry: string;
  selectedState: string;
  idStateStatus: boolean = true;
  isFormDisabled: boolean = false;
  dateofbirth: string;
  idexpiration: string;
  noIdExpiration: boolean;
  transactionStatus: string = "Scan the patron's ID or manually enter information.";
  isLabelEnabled: boolean = true;
  private tranStatusSub: Subscription;
  enteredIdNumber: string;
  idCardNumber: string;
  disableIdState: boolean = true;
  idValue: boolean = false;
  disable: boolean = true;
  patronServiceObs: Subscription;

  constructor(private patronService: PatronService, private modalPopupService: ModalPopupService,
    private loginAccountService: LoginAccountServiceService
  ) { }

  ngOnInit(): void {
    this.tranStatusSub = this.patronService.transactionStatus.subscribe((tarnsactionStatus: string) => {
      this.transactionStatus = tarnsactionStatus;
    });
    this.tranStatusSub = this.loginAccountService.EnableSubject.subscribe((tarnsactionStatus: string) => {
      this.isLabelEnabled=false;
    });
   
   
    
    fetch(this.idTypesStaticData).then(res => res.json()).then(json => {
      this.idTypes = json;
    });

    //Load Countries to dropdown
    fetch(this.idCountriesStaticData).then(res => res.json()).then(json => {
      this.idCountries = json['Country'];
    });

    this.patronServiceObs = this.patronService.patronSearchData.subscribe((patronData) => {
      this.loadPatronData(patronData);
    });
  }

  loadPatronData(patronData: PatronSearchData) {
    this.selectedIdType = patronData.IdType;
    this.selectedCountry = patronData.Country == 'USA' ? 'US' : (patronData.Country == 'CAN' ? 'CA' : patronData.Country);
    this.selectedState = patronData.StateProvince;
    this.idCardNumber = patronData.IdNumber;
    this.dateofbirth = patronData.Birthdate.slice(0, 2) + '/' + patronData.Birthdate.slice(2, 4) + '/' + patronData.Birthdate.slice(4, 8);
    if (patronData.ExpirationDate) {
      this.idexpiration = patronData.ExpirationDate.slice(0, 2) + '/' + patronData.ExpirationDate.slice(2, 4) + '/' + patronData.ExpirationDate.slice(4, 8);
    } else {
      this.noIdExpiration = true;
    }

    //(document.getElementById('search') as HTMLInputElement).disabled = true;

    setTimeout(() => {
      this.isLabelEnabled = false;
      this.psForm.form.disable();
      this.modalPopupService.showLoadingScreenModal('Loading Patron Data...');
    }, 500);

    setTimeout(() => {
      this.modalPopupService.hideLoadingScreenModal();
      this.transactionStatus = "Patron found. Please verify if patron details are correct.";
      this.patronService.patronData.next(this.patronService.patronData_Raw);
    }, 3000);
  }

  onChangeCountry(validValue: string) {
    this.selectedState = null;
    //Load States to dropdown upon selection of country
    if (validValue != null && validValue != '') {
      fetch(this.idStatesStaticData).then(res => res.json()).then(json => {
        this.idStates = json['State'].filter((item) => {
          return item.CountryCode == validValue
        });
        if (this.idStates.length > 0) {
          if (this.selectedIdType === 'UD' || this.selectedIdType === 'PP' || this.selectedIdType === 'MI' || this.selectedIdType === 'NG') {
            this.disableIdState = true;
          } else {
            this.disableIdState = false;
          }
        } else {
          this.disableIdState = true;
        }
      });
    } else {
      this.disableIdState = true;
    }
    //this.selectedCountry = selectedCountryData.validValue;
  }

  // onNoExpiration(idExpirationChk: HTMLInputElement, idExpirationInput: HTMLInputElement) {
  //   if (idExpirationChk.checked) {
  //     idExpirationInput.value = '';
  //     idExpirationInput.type = 'text';
  //     idExpirationInput.placeholder = 'I.D.Expiration';
  //   }
  // }

  onChangeIdType(selectedIdType: string) {
    if (selectedIdType != null || selectedIdType != '') {
      this.idValue = true;
      this.selectedIdType = selectedIdType;
    }
    else {
      this.idValue = false;
    }
    if (this.idStates.length > 0) {
      if (this.selectedIdType === 'UD' || this.selectedIdType === 'PP' || this.selectedIdType === 'MI' || this.selectedIdType === 'NG') {
        this.disableIdState = true;
        this.selectedState = null;
      } else {
        this.disableIdState = false;
      }
    } else {
      this.disableIdState = true;
    }
  }

  onChangeState(selectedStateData: { isSelected: boolean, validValue: string }) {
    this.selectedState = selectedStateData.validValue;
  }

  onSearch(patronSearch: NgForm) {


    const patroninfo = {
      "IDCountry": this.selectedCountry,
      "IDState": this.selectedState,
      "IDType": this.selectedIdType,
      "IDNumber": this.idCardNumber,
      "IDExpiryDate": `${this.idexpiration}T00:00:00`,
      "BirthDate": this.dateofbirth,
      "ExpiryDateChecked": this.noIdExpiration,
    };
    this.loginAccountService.sendSearchinfo(patroninfo);

    console.log(patronSearch);
    console.log('idNumber');
    console.log(patronSearch.form.controls.idNumber.valid);
    console.log(patronSearch.form.controls.idNumber.value);
    console.log('dob');
    console.log(patronSearch.form.controls.dateofbirth.valid);
    console.log(patronSearch.form.controls.dateofbirth.value);
    console.log('idCountry');
    console.log(patronSearch.form.controls.idCountry.valid);
    console.log(patronSearch.form.controls.idCountry.value);
    console.log('idExpiration');
    console.log(patronSearch.form.controls.idExpiration.valid);
    console.log(patronSearch.form.controls.idExpiration.value);
    console.log('idExpirationToggle');
    console.log(patronSearch.form.controls.noexpiration.valid);
    console.log(patronSearch.form.controls.noexpiration.value);

    const patronInformation: PatronProfile = {
      FirstName: 'Loree', MiddleName: 'Doe', LastName: 'Cordes', Country: this.selectedCountry, StateProvince: this.selectedState,
      BillingPostalCode: '32810-0000', PhoneNumber: '', EmailAddress: 'loreecordes@gmail.com', Occupation: '', SSN: '', PlayerCardNo: '', CapturePlayerCardNo: '',
      Language: '', StreetAddress: { City: 'ORLANDO', Street1: '110 NINTH ST', Street2: 'Apartment 2B', PostalCode: '32810-0000' }
    };

    setTimeout(() => {
      this.isLabelEnabled = false;
      this.psForm.form.disable();
      this.modalPopupService.showLoadingScreenModal('Loading Patron Data...');
    }, 500);

    setTimeout(() => {
      this.modalPopupService.hideLoadingScreenModal();
      this.transactionStatus = "Patron found. Please verify if patron details are correct.";
      this.patronService.patronData.next(patronInformation);
    }, 3000);

    this.patronServiceObs.unsubscribe();
    patronSearch.form.disable();
  }

  onNoExpiration(event: Event) {
    this.idexpiration = undefined;
  }

  ngOnDestroy(): void {
    this.tranStatusSub.unsubscribe();
  }
}