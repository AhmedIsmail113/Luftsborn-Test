import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RouterHelper } from '../../common/helpers/router-helper';
import { NoteFilters } from './_models/note-filters';
import { NoteBasic } from './_models/note-basic';
import { environment } from '../../../environment/environment';
@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private controllerName = 'Notes';
  constructor(private httpClient: HttpClient, private routerHelper: RouterHelper) { }
  //Queries
    getNotes(filters: NoteFilters): Observable<NoteBasic[]> {
      return this.httpClient.get<NoteBasic[]>(this.routerHelper.getUrlWithQueryString(`${this.controllerName}/FilterNotes`, filters));
  }

      getNotesPaginated(filters: NoteFilters): Observable<NoteBasic[]> {
      return this.httpClient.get<NoteBasic[]>(this.routerHelper.getUrlWithQueryString(`${this.controllerName}/FilterNotesPaginated`, filters));
  }
      getNoteDetails(id: string): Observable<NoteBasic[]> {
      return this.httpClient.get<NoteBasic[]>(this.routerHelper.getUrlWithQueryString(`${this.controllerName}/GetNoteDetails`, {id}));
  }
  //Commands
    CreateNote(note: {title: string, content: string, tagId:string}): Observable<string> {
    return this.httpClient.post<string>(`${environment.resourceURL}/${this.controllerName}/CreateNote`, note);
  }
    EditTitle(obj: {id:string, title: string}): Observable<boolean>{
    return this.httpClient.put<boolean>(`${environment.resourceURL}/${this.controllerName}/EditTitle`, obj);   
  }
    EditContent(obj: {id:string, content: string}): Observable<boolean>{
    return this.httpClient.put<boolean>(`${environment.resourceURL}/${this.controllerName}/EditContnet`, obj);   
  }
    DeleteNote(obj: {id:string}): Observable<boolean>{
    return this.httpClient.put<boolean>(`${environment.resourceURL}/${this.controllerName}/DeleteNote`,obj);   
  }
    DeleteNotePhysically(obj: {id:string}): Observable<boolean>{
    return this.httpClient.put<boolean>(`${environment.resourceURL}/${this.controllerName}/DeleteNotePhysically`, obj);
  }
}
