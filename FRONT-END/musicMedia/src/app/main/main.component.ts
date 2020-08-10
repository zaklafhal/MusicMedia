import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SpotifyService } from '../services/spotify.service';
import { ArtistService } from '../services/artist.service';
import { Artist } from './../model/artist';
import { StorageService } from '../services/storage.service';

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
    private artistService: ArtistService,
    private storage: StorageService
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
    this.storeArtists();
  }
  storeArtists(): void {
    this.artistService
      .getArtists()
      .subscribe((res) => this.storage.storeArtists(res));
  }
  getToken(): void {
    // get the spotify token
    this.spotify.getToken();
  }
  addToList(): void {
    //Call the backend to add the artist to the connected user list
    this.artistService.addArtist(this.artist).subscribe((res) => {
      this.storage.storeArtists(res);
    });
  }
}
