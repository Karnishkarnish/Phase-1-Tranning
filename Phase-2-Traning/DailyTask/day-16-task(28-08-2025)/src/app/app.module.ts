import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; 
import { AppComponent } from './app.component';
import { AbcComponent } from './abc/abc.component';
import { MovieService  } from './movie.service';
import { HttpClientModule } from '@angular/common/http';
@NgModule({
  declarations: [
    AppComponent,
    AbcComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule 
  ],
  providers: [MovieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
