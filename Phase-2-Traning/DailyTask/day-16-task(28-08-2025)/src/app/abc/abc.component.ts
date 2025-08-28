import { Component, OnInit, AfterContentInit } from '@angular/core';
import { Place } from '../model/place.Interface';
import { MovieService } from '../movie.service';
import { Movie } from '../model/Movie.interface';

@Component({
  selector: 'app-abc',
  templateUrl: './abc.component.html', 
  styleUrls: ['./abc.component.css']
})

export class AbcComponent implements OnInit, AfterContentInit {

  ooty: Place = { Name: 'Ooty', elevation: 200 };

  my_fav: Place[] = [
    { Name: 'Conoor', elevation: 12000 }
  ];

  my_movie: Movie[] = [];

  newMovie = {
    movieId: 0,
    movieTitle: "",
    movieDirector: "",
    year: 0
  };

  addedMovie: any;
  places: string = "";

  constructor(private movieService: MovieService) {}
deleteMovie(id: number) {
  console.log("before delete ")
  this.movieService.deleteMovie(id).subscribe(() => {

    this.movieService.getMyMovie().subscribe(movies => {
      this.my_movie = movies;
    });
  });
}

  addMovie() {
    this.movieService.AddNew(this.newMovie).subscribe(value => {
      this.addedMovie = value;

      
      this.movieService.getMyMovie().subscribe(movies => {
        this.my_movie = movies;
      });

   
      this.newMovie = { movieId: 0, movieTitle: "", movieDirector: "", year: 0 };
    });
  }

  addMore() {
    console.log("Button Clicked");
    this.my_fav = [...this.my_fav, this.ooty];
  }

  ngOnInit(): void {
    console.log("before calling in the component");

    this.movieService.getMyMovie().subscribe(value => {
      console.log(`res: ${JSON.stringify(value)}`); 
      this.my_movie = value; 
    });

    console.log(`from services:- ${this.places}`);
  }

  ngAfterContentInit(): void {
    console.log("after init");
  }
}
