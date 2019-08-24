using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MusicLibrary
{
    public static class FileHelper
    {
        //
        public async static void WriteTextFileAsync(string filename, string content)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var textFile = await storageFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);

            var textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite);

            using (var outputStream = textStream.GetOutputStreamAt(textStream.Size))
            {
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    dataWriter.WriteString("\n" + content);
                    await dataWriter.StoreAsync();
                    await outputStream.FlushAsync();
                }
            }
            textStream.Dispose();
        }

        public async static Task<string> ReadTextFileAsync(string filename)
        {
            //the method async === then we can use await below

            var storageFolder = ApplicationData.Current.LocalFolder;

            if (File.Exists(storageFolder.Path + @"\" + filename))
            {
                var textFile = await storageFolder.GetFileAsync(filename);


                //the data now it's a stream of bits we nead to read it so we use OpenReadAsync
                var textStream = await textFile.OpenReadAsync();
                var textReader = new DataReader(textStream);

                var textLength = textStream.Size;
                // we need to load the string and the loading need to know the size so we creat the texLength
                //we used the textLength in the below functions
                await textReader.LoadAsync((uint)textLength);

                //after Loading we need to Read and it should be the same length using uint
                return textReader.ReadString((uint)textLength);
            }
            else
                return null;
        }
    }
}
