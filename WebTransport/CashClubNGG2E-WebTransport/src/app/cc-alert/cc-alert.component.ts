import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { CCAlertService } from '../services/applicationServices/cc-alert.service';
import { IAlert } from './cc-alert.model';

@Component({
  selector: 'cc-alert',
  templateUrl: './cc-alert.component.html',
  styleUrls: ['./cc-alert.component.css'],
  animations: [
    trigger('divAppear', [
      state('default', style({
        opacity: 1
      })),
      transition('void => *', [
        style({ opacity: 0 }),
        animate(400)]),
      transition('* => void', [
        animate(400, style({ opacity: 0 }))])
    ]),
  ]
})
export class CcAlertComponent implements OnInit {

  shown: boolean = false;
  messagetext: string;


  constructor(private ccAlertService: CCAlertService) { }

  ngOnInit(): void {
    this.ccAlertService.alertSubject.subscribe((alert: IAlert) => {
      this.messagetext = alert.alertMessage;
      this.shown = true;
    });
  }

  onDismiss() {
    this.shown = false;
    console.log('onDismiss alert ', this.shown)
  }

  alertShown() {
    setTimeout(() => {
      this.shown = false;
    }, 3000);
  }
}