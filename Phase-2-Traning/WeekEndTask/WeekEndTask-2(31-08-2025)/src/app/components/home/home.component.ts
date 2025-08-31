import { Component } from '@angular/core';
import { MovieService, Movie } from '../../services/movie.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  movies: Movie[] = [];

  constructor(private movieService: MovieService) {
    this.movies = this.movieService.getMovies();
  }
}
