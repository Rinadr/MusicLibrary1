using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    class Album
    {
        public string AlbumTitle { get; set;}
        public string AlbumCover { get; set;}

        private const string TEXT_FILE_NAME = "Albums.txt";

        public static void WriteAlbum(Album album)
        {
            var albumData = $"{album.AlbumTitle}, {album.AlbumCover}";
            FileHelper.WriteTextFileAsync(TEXT_FILE_NAME, albumData);
        }

        public async static Task<ICollection<Album>> GetAllAlbumsAsync()
        {
            var albums = new List<Album>();
            var content = await FileHelper.ReadTextFileAsync(TEXT_FILE_NAME);
            if (content != null)
            {
                var lines = content.Split('\r', '\n');
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;

                    var lineParts = line.Split(',');
                    var album = new Album
                    {
                       AlbumTitle = lineParts[0],
                       AlbumCover = lineParts[1]
                    };
                    albums.Add(album);
                }
            }

            return albums;
        }

    }

}
