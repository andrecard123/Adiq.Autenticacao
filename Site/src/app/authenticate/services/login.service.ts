import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICredential } from '../models/credential';
import { AppConfig } from '../../app.config';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(
    protected httpClient: HttpClient
  ) { }

  private url = AppConfig.settings.baseUrls.adiqApi;


  public authenticateCredential<T>(credential: ICredential): Observable<T> {
    return this.httpClient
      .post<T>(`${this.url}/Account/Authenticate`, credential);
  }
}