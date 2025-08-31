import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Movie } from '../../services/movie.service';

@Component({
  selector: 'app-booking-summary',
  templateUrl: './booking-summary.component.html',
  styleUrls: ['./booking-summary.component.css']
})
export class BookingSummaryComponent {
  seats: string[] = [];
  movie?: Movie;

  constructor(private router: Router) {
    const nav = this.router.getCurrentNavigation();
    const state = nav?.extras?.state as { seats: string[], movie: Movie };
    if (state) {
      this.seats = state.seats;
      this.movie = state.movie;
    }
  }
}


