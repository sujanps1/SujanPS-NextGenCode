import { Component, Input, OnInit, Output, EventEmitter, Provider, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

const DROPDOWN_CONTROL_VALUE_ACCESSOR: Provider = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => CcDropdownComponent),
  multi: true,
};

@Component({
  selector: 'cc-dropdown',
  templateUrl: './cc-dropdown.component.html',
  styleUrls: ['./cc-dropdown.component.css'],
  providers: [DROPDOWN_CONTROL_VALUE_ACCESSOR]
})
export class CcDropdownComponent implements OnInit, ControlValueAccessor {

  @Input() name: string;
  @Input() id: string;

  @Input() dropdownDataPath: string;
  @Input() dropdownValues: [] = [];
  @Input() bindLabel: string;
  @Input() bindValue: string;
  @Input() placeholder: string;
  @Output() valueSelected = new EventEmitter<{ isSelected: boolean, validValue: string }>();

  isDisabled: boolean = false;

  selectedValue: string;

  valueExists: boolean = false;
  dropDownFocused: boolean = false;

  writeValue(value: string): void {
    this.selectedValue = value;
    this.onChange(this.selectedValue);
  }

  private _onChange = (value: any | null) => undefined;
  registerOnChange(fn: any): void {
    this._onChange = fn;
  }

  private _onTouched = () => undefined;
  registerOnTouched(fn: any): void {
    this._onTouched = fn;
  }
  setDisabledState?(disable: boolean): void {
    this.isDisabled = disable;
  }

  get value(): any {
    return this.selectedValue;
  };

  set value(v: any) {
    if (v !== this.selectedValue) {
      this.selectedValue = v;
      this._onChange(v);
    }
  }

  ngOnInit(): void {
    if (this.dropdownDataPath != null && this.dropdownDataPath != '') {
      fetch(this.dropdownDataPath).then(res => res.json()).then(json => {
        this.dropdownValues = json;
      });
    }
  }

  loadDropdownData() {
    fetch(this.dropdownDataPath).then(res => res.json()).then(json => {
      this.dropdownValues = json;
    });
  }

  onChange(selectedValue: string) {
    if (selectedValue != undefined && selectedValue != null && selectedValue != '') {
      this.valueExists = true;
      this.dropDownFocused = false;
    }
    else {
      this.valueExists = false;
    }

    this.valueSelected.emit({ isSelected: this.valueExists, validValue: selectedValue })
    this._onChange(this.selectedValue);
  }

  onFocus() {
    this.dropDownFocused = true;
  }

  onBlur() {
    this.dropDownFocused = false;
  }
}
