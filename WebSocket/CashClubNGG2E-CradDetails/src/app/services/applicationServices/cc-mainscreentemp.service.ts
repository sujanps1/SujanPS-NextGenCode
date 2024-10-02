import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable()

export class MainScreenTempService {

    updatemainscreenSubject = new Subject();

    showMainScreen(showMainScreen: boolean) {
        //console.log('Show main screen service called ', showMainScreen);
        this.updatemainscreenSubject.next(showMainScreen);
    }
}