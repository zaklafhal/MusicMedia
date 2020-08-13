import { Component, OnInit } from '@angular/core';
import { Artist } from '../model/artist';
import { StorageService } from '../services/storage.service';
import { ArtistService } from '../services/artist.service';

@Component({
  selector: 'app-artist-list',
  templateUrl: './artist-list.component.html',
  styleUrls: ['./artist-list.component.css'],
})
export class ArtistListComponent implements OnInit {
  artists: Artist[];
  count: number;

  constructor(
    private storage: StorageService,
    private artistService: ArtistService
  ) {}

  getArtists(): void {
    this.artists = this.storage.getArtists();
    this.count = this.artists.length;
  }

  ngOnInit(): void {
    this.getArtists();
  }

  removeArtist(artist: Artist) {
    this.artistService.removeArtist(artist).subscribe((res) => {
      this.storage.storeArtists(res);
      this.getArtists();
    });
  }
}
