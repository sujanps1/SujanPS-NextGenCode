import { Component, Input, OnInit, Provider, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

const CHECKBOX_CONTROL_VALUE_ACCESSOR: Provider = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => CcToggleSwitchComponent),
  multi: true,
};

@Component({
  selector: 'cc-control-toggle-switch',
  templateUrl: './cc-toggle-switch.component.html',
  styleUrls: ['./cc-toggle-switch.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => CcToggleSwitchComponent),
    multi: true,
  },]
})
export class CcToggleSwitchComponent implements OnInit, ControlValueAccessor {

  ngOnInit(): void {
    this._onChange(this.checkboxValue);
    this._onTouched();
  }

  isDisabled: boolean = false;
  checkboxValue: boolean = false;
  @Input() name: string;
  @Input() id: string;

  writeValue(obj: boolean): void {
    this.checkboxValue = obj;
  }

  private _onChange = (boolean: any | null) => undefined;
  registerOnChange(fn: ((value: boolean | null) => void)): void {
    this._onChange = fn;
  }

  private _onTouched = () => undefined;
  registerOnTouched(fn: () => void): void {
    this._onTouched = fn;
  }

  setDisabledState?(disable: boolean): void {
    console.log('Checkbox disable method called')
    this.isDisabled = disable;
  }

  @Input() toggleSwitchLabel: string = '';

  get value(): boolean {
    return this.checkboxValue;
  };

  set value(v: boolean) {
    if (v !== this.checkboxValue) {
      this.checkboxValue = v;
      this._onChange(v);
    }
  }
}
