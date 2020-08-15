export class Album {
  name: string;
  image: string;
  spotifyId: string;

  constructor(name: string, image: string, spotifyId: string) {
    this.name = name;
    this.image = image;
    this.spotifyId = spotifyId;
  }

  static parseAlbums(res: any): Album[] {
    const { items } = res;
    let albums = [];
    for (const item of items) {
      const album = new Album(item.name, item.images[0].url, item.id);
      albums.push(album);
    }
    return albums;
  }
}
