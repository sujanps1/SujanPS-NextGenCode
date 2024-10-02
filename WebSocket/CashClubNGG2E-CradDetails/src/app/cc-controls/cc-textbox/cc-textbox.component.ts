import { formatDate } from '@angular/common';
import { Component, EventEmitter, Input, Output, OnInit, Provider, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { LoginAccountServiceService } from 'src/app/services/login-account-service.service';

const TEXTBOX_CONTROL_VALUE_ACCESSOR: Provider = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => CcTextboxComponent),
  multi: true,
};

@Component({
  selector: 'cc-textbox',
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => CcTextboxComponent),
    multi: true,
  },],
  templateUrl: './cc-textbox.component.html',
  styleUrls: ['./cc-textbox.component.css']
})
export class CcTextboxComponent implements OnInit, ControlValueAccessor {
  @Input() placeholder: string = 'Please enter data';
  @Input() name: string;
  @Input() id: string;
  @Input() type: string;
  @Input() maxlength: number = 160;
  @Input() style: string = '';
  @Input() pattern: string;

  showCapture: boolean = false;
  showQRCode: boolean = false;
  showDipCard: boolean = false;
  behaviorType: string;
  showpopup:boolean=false;
  showCaptures: boolean = false;
  buttonName:string;
  buttonLabel="Wait 7Sec to get Langauge"

  @Input()
  set capture(param) {
    this.showCapture = true;
  }

  @Input()
  set qrCode(param) {
    this.showQRCode = true;
  }

  @Input()
  set dipCard(param) {
    this.showDipCard = true;
  }

  @Input() qrcapture: boolean;
  @Input() dipcapture: boolean;

  isDisabled: boolean = false;

  valueExists: boolean = false;

  inputValue: string;
  movePlaceholder: boolean = false;

  @Output() valueEntered = new EventEmitter<{ value: string }>();
  @Output() captureClicked = new EventEmitter<{ capture: boolean }>();

  constructor(
    private loginAccountService: LoginAccountServiceService
  ) { }

  writeValue(obj: any): void {
    console.log();
    this.performFormatting(obj);

    if (obj == undefined) {
      this.movePlaceholder = false;
    }
    else {
      this.movePlaceholder = true;
    }

    this._onChange(this.inputValue);
  }

  private _onChange = (value: any | null) => undefined;
  registerOnChange(fn: ((value: string | null) => void)): void {
    this._onChange = fn;
  }

  private _onTouched = () => undefined;
  registerOnTouched(fn: () => void): void {
    this._onTouched = fn;
  }

  setDisabledState?(disable: boolean): void {
    this.isDisabled = disable;
    if (this.inputValue != null && this.inputValue != '') {
      this.movePlaceholder = true;
    }
  }

  //get accessor
  get value(): any {
    return this.inputValue;
  };

  set value(v: any) {
    if (v !== this.inputValue) {
      this.inputValue = v;
      this._onChange(v);
    }
  }

  ngOnInit(): void {
    this._onChange(this.inputValue);
    this._onTouched();
  }

  onInput(event: Event) { }

  onDateFocus(event: Event) {
    this.behaviorType = 'date';
    //console.log('onDateFocus ', this.inputValue, 'value: ', this.value)
  }

  onDateBlur(event: Event) {
    if (this.value == '' || this.value == undefined) {
      this.behaviorType = 'text';
      this.value = undefined;
      //console.log('IF block on blur ', 'inputValue: ', this.inputValue, 'value: ', this.value);
      this.movePlaceholder = false;
    }
    else {
      this.behaviorType = 'date';
      //console.log('else block on blur');
    }
  }

  onChange(inputValue: any) {
    console.log(this.type, inputValue);
    // if(this.name==="feeAmount")
    // {
    // this.loginAccountService.sendFees();
    // }
    this.valueEntered.emit({ value: inputValue });

    //console.log('onChange ', this.inputValue, 'value: ', this.value);
    //console.log('Behavior Type: ', this.behaviorType, 'Type: ', this.type);
  }

  onCapture() {
    console.log("form oncapture",this.name);
    if(this.name==="patronLanguage"){
         this.loginAccountService.sendLanguage();
    }
    console.log("form oncapture",this.name);
    if(this.name==="patronSSN"){
        this.loginAccountService.sendSSN();
    }
    console.log("form oncapture",this.name);
    if(this.name==="requestedamount"){
      this.loginAccountService.sendAmount();
  }
    console.log('Capture clicked');
    // this.showpopup = true; 
    // switch (this.buttonName) {
    //   case 'patronLanguage':
    //     this.imageSource = '../../../assets/images/ingenico-screens/language-selection.png';
    //     break;
    //   case 'patronSSN':
    //     this.imageSource = '../../../assets/images/ingenico-screens/ssn.png';
    //     break;
    //   case 'requestedAmount':
    //     this.imageSource = '../../../assets/images/ingenico-screens/amount-entry.png';
    //     break;
    //   default:
    //     this.imageSource = '';
    //     break;
    // }

    // this.loginAccountService.showPopup()
    // this.showpopup = true;
    // setTimeout(() => {
    //   this.showpopup = false;
    // }, 7000);
    this.captureClicked.emit({ capture: true });
  }

  // onCaptureSSN() {
  //   this.showpopup=true;

  //   setTimeout(() => {
  //     this.showpopup=false;
  //   },5000);

  //   this.loginAccountService.sendSSN();
  //   this.captureClicked.emit({ capture: true });
  // }

  performFormatting(obj: any) {
    let convertedValue: string;
    if (this.type == 'date') {
      // if(this.type == 'date'){
      //   console.log('Write log ', obj)
      // }

      if (!isNaN(Date.parse(obj))) {
        this.inputValue = formatDate(new Date(obj), 'MM/dd/yyyy', 'en-US');
        //console.log('Write log ', formatDate(new Date(obj), 'MM/dd/yyyy', 'en-US'));
      }
      else if ((obj == undefined || obj == '') && this.value != undefined) {
        this.value = undefined;
        this.behaviorType = 'text';
      }
    }
    else {
      this.inputValue = obj
    }
  }

  formatPhoneNumber(inputValue: any) {
    // Remove any non-numeric characters from the input
    this.value = this.value.replace(/\D/g, '');

    if (this.value !== '') {
      // Apply the desired formatting
      if (this.value.length > 3) {
        this.value = this.value.slice(0, 3) + '-' + this.value.slice(3);
      }
      if (this.value.length > 7) {
        this.value = this.value.slice(0, 7) + '-' + this.value.slice(7);
      }
    }

    //Placeholder to re-apply
    if (this.value === '') {
      this.value = undefined;
    }
  }
}