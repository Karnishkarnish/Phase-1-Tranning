import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MovieService, Movie } from '../../services/movie.service';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent {
  movieId: number;
  movie?: Movie;
  seats: string[] = ['A1','A2','A3','B1','B2','B3','C1','C2'];
  selectedSeats: string[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private movieService: MovieService
  ) {
    this.movieId = Number(this.route.snapshot.paramMap.get('id'));
    this.movie = this.movieService.getMovieById(this.movieId);
  }

  toggleSeat(seat: string) {
    if (this.selectedSeats.includes(seat)) {
      this.selectedSeats = this.selectedSeats.filter(s => s !== seat);
    } else {
      this.selectedSeats.push(seat);
    }
  }

  confirmBooking() {
    this.router.navigate(['/summary'], { state: { seats: this.selectedSeats, movie: this.movie } });
  }
}
