import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAppConfig } from '../models/app-config';

@Injectable()
export class AppConfig {

    static settings: IAppConfig;

    constructor(private readonly http: HttpClient) { }

    load() {
        return new Promise((resolve, reject) => {
            const jsonFile = 'assets/config/config.json';
            this.http.get<IAppConfig>(jsonFile)
                .subscribe((response: IAppConfig) => {
                    AppConfig.settings = response;
                    resolve();
                }, error => {
                    reject(`Falha ao carregar arquivo de configurações '${jsonFile}': ${JSON.stringify(error)}`);
                });
        });
    }
}
