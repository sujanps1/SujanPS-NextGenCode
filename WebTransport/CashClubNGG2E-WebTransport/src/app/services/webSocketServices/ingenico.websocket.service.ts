import { Injectable } from "@angular/core";
import { Observable, Observer } from 'rxjs';
import { AnonymousSubject } from 'rxjs/internal/Subject';
import { Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { IngenicoMessage } from "../../entities/ingenicoRequest";

const INGENICO_URL = "ws://localhost:3020/ingenico";

@Injectable()
export class IngenicoWebsocketService {
    private subject: AnonymousSubject<MessageEvent> | undefined;
    public messages: Subject<IngenicoMessage>;
    public isConnected: boolean = true;

    constructor() {
        this.messages = <Subject<IngenicoMessage>>this.connect(INGENICO_URL).pipe(
            map(
                (response: MessageEvent): IngenicoMessage => {
                    console.log(response.data);
                    let data = JSON.parse(response.data)
                    console.log("Data", data);
                    return data;
                }
            )
        );
    }

    public connect(url: string): AnonymousSubject<MessageEvent> {
        if (!this.subject) {
            this.subject = this.create(url);
            console.log("Successfully connected: " + url);
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
                console.log('Message Complete');
            },
            next(data: Object) {
                console.log('Message sent to websocket: ', data);
                if (ws.readyState === WebSocket.OPEN) {
                    ws.send(JSON.stringify(data));
                }
            },
        }
        return new AnonymousSubject<MessageEvent>(observer, observable);
    }
}