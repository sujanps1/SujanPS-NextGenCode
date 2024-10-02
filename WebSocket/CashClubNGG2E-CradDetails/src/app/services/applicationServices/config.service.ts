import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IConfig } from 'src/app/entities/config.interface';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  config!: IConfig;

  constructor(private http: HttpClient) { }

  loadConfig() {
    return new Promise((resolve, reject) => {
      this.http
        .get<IConfig>('../../assets/config.json')
        .subscribe({
          next: (data: IConfig) => {
            this.config = data;
            this.config.retrievalCardNumber = `${data.retrievalCardNumber}`;
            this.config.approvalLimit = `${data.approvalLimit}`;
            this.config.retrieveAmount = `${data.retrieveAmount}`;
            resolve(true);
          }, error: (error) => {
            reject(error);
          }
        }
        );
    })
  }
}