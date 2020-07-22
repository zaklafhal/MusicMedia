export class Artist {
  name: string;
  image: string;
  spotifyId: string;

  constructor(name: string, image: string, spotifyId: string) {
    this.name = name;
    this.image = image;
    this.spotifyId = spotifyId;
  }

  parse(res: any): Artist {
    const { items } = res;
    const artistInfos = items[0];
    const artist = new Artist(
      artistInfos.name,
      artistInfos.images[0].url,
      artistInfos.id
    );
    return artist;
  }
}
