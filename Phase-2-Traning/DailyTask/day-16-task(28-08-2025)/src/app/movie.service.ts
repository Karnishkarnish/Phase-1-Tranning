import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Movie } from './model/Movie.interface';
import { AddMovie } from './model/AddMovie.interface';
@Injectable({
  providedIn: 'root'
})
export class MovieService {
private apiUrl = 'http://localhost:5128/Movie';
  constructor(private http:HttpClient) { }
 AddNew(addmovie : AddMovie)
 {
  console.log("Before add movie")
  return this.http.post(`${this.apiUrl}/AddMovie`,addmovie,{responseType:"text"})
 }
 getMyMovie(){ 
  console.log("Before fetch")
 return this.http.get<Movie[]>(`${this.apiUrl}/GetAllMovie`)
 }
deleteMovie(id: number) {
  return this.http.delete(`http://localhost:5128/Movie/Delete?id=${id}`);
}

  getFavPlaces():string{
    return "London"
  }
}
