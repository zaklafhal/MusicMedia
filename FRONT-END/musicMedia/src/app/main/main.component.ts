import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SpotifyService } from './../spotify.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  public form = this.formBuilder.group({
    artisteName: ['', []],
  });
  constructor(
    private formBuilder: FormBuilder,
    private spotify: SpotifyService
  ) {}

  search(form: FormGroup) {
    const { value: artiste } = form.controls.artisteName;
    this.spotify.searchArtist(artiste).subscribe((res) => console.log(res))
  }
  ngOnInit(): void {
    this.getToken();
  }
  getToken(): void {
    // get the spotify token
    this.spotify.getToken();
  }
}
