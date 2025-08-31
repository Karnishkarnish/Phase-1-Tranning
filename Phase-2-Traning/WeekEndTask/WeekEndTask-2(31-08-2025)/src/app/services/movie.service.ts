import { Injectable } from '@angular/core';

export interface Movie {
  id: number;
  title: string;
  director: string;
  year: number;
  poster: string;
}

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  private movies: Movie[] = [
    { id: 1, title: 'Coolie', director: 'Logesh Kangaraj', year: 2025, poster: 'assets/coolie.jpg' },
    { id: 2, title: 'Baasha', director: 'Suresh Krishna', year: 1995, poster: 'assets/baasha.jpg' },
    { id: 3, title: 'Muthu', director: 'K. S. Ravikumar', year: 1995, poster: 'assets/muthu.jpg' }
  ];

  getMovies(): Movie[] {
    return this.movies;
  }

  getMovieById(id: number): Movie | undefined {
    return this.movies.find(m => m.id === id);
  }
}
