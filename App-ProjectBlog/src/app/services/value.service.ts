import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ValueService {

path=environment.valuePath;
constructor(private client:HttpClient) { }

getValue():Observable<any>{
  return this.client.get<any>(this.path);  
}
}
