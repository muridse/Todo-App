import { Component } from '@angular/core';
import {HttpClient} from '@angular/common/http'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'todoapp';
  readonly ApiURL = "http://localhost:5239/api/todoapp/";
  
  constructor(private http:HttpClient){
  }
  notes:any=[];

  refreshNotes(){
    this.http.get(this.ApiURL+'GetNotes').subscribe(data=>{
      this.notes = data;
    })
  }
  addNotes(){
    var newNotes = (<HTMLInputElement>document.getElementById("newNotes")).value;
    var formData = new FormData();
    formData.append("newNotes", newNotes);
    this.http.post(this.ApiURL + 'AddNote', formData).subscribe(data=>{
      //alert(data);
      this.refreshNotes();
      (<HTMLInputElement>document.getElementById("newNotes")).value = '';
    });
  }
  deleteNotes(id:any){
    var newNotes = id;
    var formData = new FormData();
    formData.append("newNotes", newNotes);
    this.http.delete(this.ApiURL + 'DeleteNote?id=' + id).subscribe(data=>{
      //alert(data);
      this.refreshNotes();
    });
  }
  ngOnInit(){
    this.refreshNotes();
  }
  
}
