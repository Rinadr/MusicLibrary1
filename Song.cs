using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    class Song
    {
        public string AlbumTitle { get; set; }
        public string SongTitle { get; set;}
        public string MusicFile { get; set;}
        

        public static void WriteSong(Song song, string TEXT_FILE_NAME)
        {
            var songData = $"{song.AlbumTitle} , {song.SongTitle}, {song.MusicFile}";
            FileHelper.WriteTextFileAsync(TEXT_FILE_NAME, songData);
        }

        public async static Task<ICollection<Song>> GetAllSongsAsync(string TEXT_FILE_NAME)
        {
            var songs = new List<Song>();
            var content = await FileHelper.ReadTextFileAsync(TEXT_FILE_NAME);
            if (content != null)
            {
                var lines = content.Split('\r', '\n');
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;

                    var lineParts = line.Split(',');
                    var song = new Song
                    {
                        AlbumTitle = lineParts[0],
                        SongTitle = lineParts[1],
                        MusicFile = lineParts[2]
                    };
                    songs.Add(song);
                }
            }
             
            
            return songs;  
        }
    }

}
