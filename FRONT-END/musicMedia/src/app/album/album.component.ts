import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SpotifyService } from '../services/spotify.service';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {

  constructor(private route: ActivatedRoute, private spotify: SpotifyService) { }
  private artistId = this.route.snapshot.paramMap.get('id');
  
  ngOnInit(): void {
    this.getAlbums();
  }
  getAlbums() {
    this.spotify
      .getAlbums(this.artistId)
      .subscribe((res) => console.log(res));
  }

}
