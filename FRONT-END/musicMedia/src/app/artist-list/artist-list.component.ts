import { Component, OnInit } from '@angular/core';
import { Artist } from '../model/artist';
import { StorageService } from '../services/storage.service';

@Component({
  selector: 'app-artist-list',
  templateUrl: './artist-list.component.html',
  styleUrls: ['./artist-list.component.css'],
})
export class ArtistListComponent implements OnInit {
  artists: Artist[];
  count: number;

  constructor(private storage: StorageService) {}

  getArtists(): void {
    this.artists = this.storage.getArtists();
    this.count = this.artists.length;
  }

  ngOnInit(): void {
    this.getArtists();
  }
}
