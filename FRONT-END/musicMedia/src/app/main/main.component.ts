import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SpotifyService } from '../services/spotify.service';
import { ArtistService } from '../services/artist.service';
import { Artist } from './../model/artist';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  public form = this.formBuilder.group({
    artisteName: ['', []],
  });
  public artist: Artist;
  public error: string;
  constructor(
    private formBuilder: FormBuilder,
    private spotify: SpotifyService,
    private artistService: ArtistService
  ) {}

  search(form: FormGroup) {
    const { value: artiste } = form.controls.artisteName;
    this.spotify.searchArtist(artiste).subscribe(
      (res) => (this.artist = res),
      (error) => {
        const { error: e } = error;
        const { message } = e.error;
        this.error = message;
      }
    );
  }
  ngOnInit(): void {
    this.getToken();
  }
  getToken(): void {
    // get the spotify token
    this.spotify.getToken();
  }
  addToList(): void {
    console.log(this.artist);
    this.artistService
      .addArtist(this.artist)
      .subscribe((res) => console.log(res));
  }
}
