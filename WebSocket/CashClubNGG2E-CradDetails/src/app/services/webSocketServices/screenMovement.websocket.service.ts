import { Injectable } from "@angular/core";
import { AnonymousSubject } from 'rxjs/internal/Subject';
import { Observable, Observer, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

const SCREEN_URL = "ws://localhost:3020/screen";

@Injectable()
export class ScreenMovementWebSocketService {
    private subject: AnonymousSubject<MessageEvent> | undefined;
    public messages: Subject<string>;

    constructor() {
        this.messages = <Subject<string>>this.connect(SCREEN_URL).pipe(
            map(
                (response: MessageEvent): string => {
                    return response.data;
                }
            )
        );
    }

    public connect(url: string): AnonymousSubject<MessageEvent> {
        if (!this.subject) {
            this.subject = this.create(url);
        }
        return this.subject;
    }

    private create(url: string): AnonymousSubject<MessageEvent> {
        let ws = new WebSocket(url);
        let observable = new Observable((obs: Observer<MessageEvent>) => {
            ws.onmessage = obs.next.bind(obs);
            ws.onerror = obs.error.bind(obs);
            ws.onclose = obs.complete.bind(obs);
            return ws.close.bind(ws);
        });

        let observer: Observer<MessageEvent<any>> = {
            error(err) {

            },
            complete() {

            },
            next(data: Object) {
                if (ws.readyState === WebSocket.OPEN) {
                    //console.log(JSON.stringify(data));
                    console.log(data.toString());
                    ws.send(data.toString());
                }
            },
        }
        return new AnonymousSubject<MessageEvent>(observer, observable);
    }
}