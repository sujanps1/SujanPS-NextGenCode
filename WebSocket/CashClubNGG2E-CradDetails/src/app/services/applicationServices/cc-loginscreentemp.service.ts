import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable()

export class LoginScreenTempService {

    updateloginscreenSubject = new Subject();

    showMainScreen(showMainScreen: boolean) {
        //console.log('Show main screen service called ', showMainScreen);
        this.updateloginscreenSubject.next(showMainScreen);
    }
}