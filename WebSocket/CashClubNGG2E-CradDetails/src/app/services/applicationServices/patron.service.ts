import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { PatronProfile } from "src/app/entities/patronData";
import { PatronSearchData } from "src/app/entities/patronSearchData";

@Injectable()
export class PatronService {
    patronData: Subject<PatronProfile>;
    patronSearchData: Subject<PatronSearchData>;
    transactionStatus: Subject<string>;

    patronData_Raw: PatronProfile;

    constructor() {
        this.patronData = new Subject<PatronProfile>();
        this.patronSearchData = new Subject<PatronSearchData>();
        this.patronData_Raw = new PatronProfile();
        this.transactionStatus = new Subject<string>();
    }
}