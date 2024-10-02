import { Directive, ElementRef, Renderer2, OnInit, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[ccinputvalidation]'
})

export class CCInputValidationDirective implements OnInit {

  @Input() defaultValue: string = '';
  //@HostBinding('inputValue') formattedvalue: string;

  private navigationKeys = [
    'Backspace',
    'Delete',
    'Tab',
    'Escape',
    'Enter',
    'Home',
    'End',
    'ArrowLeft',
    'ArrowRight',
    'Clear',
    'Copy',
    'Paste',
  ];

  @HostListener('keydown', ['$event']) onKeyDown(e: KeyboardEvent) {

    // //this.renderer.setAttribute(this.elRef.nativeElement, 'value', 'ABDBDHBD');
    // console.log('Hostlistener focus');

    // console.log( this.defaultValue);
    // console.log( this.formattedvalue);
    // //this.formattedvalue = this.formattedvalue + '1';
    // this.renderer.setStyle(this.elRef.nativeElement, 'color', 'red');
    //console.log('Hostlistener keydown');
    if (
      // Allow: Delete, Backspace, Tab, Escape, Enter, etc
      this.navigationKeys.indexOf(e.key) > -1 ||
      (e.key === 'a' && e.ctrlKey === true) || // Allow: Ctrl+A
      (e.key === 'c' && e.ctrlKey === true) || // Allow: Ctrl+C
      (e.key === 'v' && e.ctrlKey === true) || // Allow: Ctrl+V
      (e.key === 'x' && e.ctrlKey === true) || // Allow: Ctrl+X
      (e.key === 'a' && e.metaKey === true) || // Cmd+A (Mac)
      (e.key === 'c' && e.metaKey === true) || // Cmd+C (Mac)
      (e.key === 'v' && e.metaKey === true) || // Cmd+V (Mac)
      (e.key === 'x' && e.metaKey === true) // Cmd+X (Mac)
    ) {
      return;  // let it happen, don't do anything
    }
    // Ensure that it is a number and stop the keypress
    if (e.key === ' ' || isNaN(Number(e.key))) {
      e.preventDefault();
    }
  }

  @HostListener('blur') blur(eventData: Event) {

    //this.renderer.setAttribute(this.elRef.nativeElement, 'value', 'ABDBDHBD');
    //console.log('Hostlistener focus');

    //console.log( this.defaultValue);
    //console.log( this.formattedvalue);
    //this.formattedvalue = this.formattedvalue + '1';
    //this.renderer.setStyle(this.elRef.nativeElement, 'color', 'blue');
  }


  constructor(private elRef: ElementRef, private renderer: Renderer2) {

  }
  ngOnInit(): void {
    // this.renderer.setAttribute(this.elRef.nativeElement, 'value', this.elRef.nativeElement.value + 'ABD');

    //this.renderer.setAttribute(this.elRef.nativeElement, 'value', 'ABD');
    //this.formattedvalue = this.elRef.nativeElement.getAttribute('value')
    //this.formattedvalue = this.defaultValue;
    console.log('ng on init directive focus')
    // console.log( this.defaultValue);
    // console.log( this.formattedvalue);

  }
}