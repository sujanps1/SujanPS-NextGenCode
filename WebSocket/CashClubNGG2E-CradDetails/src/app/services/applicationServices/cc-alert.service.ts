import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { IAlert } from "src/app/cc-alert/cc-alert.model";

@Injectable()

export class CCAlertService {

    alertSubject = new Subject<IAlert>();

    showAlert(type: string, message: string, active: boolean) {
        console.log('Show alert service called ', type, message);
        this.alertSubject.next({ alertType: type, alertMessage: message, alertActive: active })
    }
}