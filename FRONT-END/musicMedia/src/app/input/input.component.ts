import { Component, OnInit, forwardRef, Input, Self } from '@angular/core';
import {
  ControlValueAccessor,
  NG_VALUE_ACCESSOR,
  Validator,
  AbstractControl,
  ValidationErrors,
  ValidatorFn,
  Validators,
  NG_VALIDATORS,
  NgControl,
} from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css'],
})
export class InputComponent implements OnInit, ControlValueAccessor, Validator {
  @Input() placeholder: string;
  @Input() type: string;
  @Input() error: string = this.setError();
  @Input() isRequired: boolean;
  @Input() icon : string;
  value: string;
  onChange: (event) => void;
  onTouched: () => void;
  disabled: boolean;

  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }
  setError(): string {
    const errorMsg = 'This field is required !';
      return errorMsg;
    
  }
  validate(control: AbstractControl): ValidationErrors {
    const validators: ValidatorFn[] = [];
    if (this.isRequired) {
      validators.push(Validators.required);
    }
    return validators;
  }
  writeValue(obj: any): void {
    this.value = obj ? obj : '';
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  ngOnInit(): void {
    const control = this.controlDir.control;
    const validators: ValidatorFn[] = control.validator
      ? [control.validator]
      : [];
    if (this.isRequired) {
      validators.push(Validators.required);
    }
    control.setValidators(validators);
    control.updateValueAndValidity();
  }
}
