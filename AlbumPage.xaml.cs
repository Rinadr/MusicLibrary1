using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlbumPage : Page
    {
        public AlbumPage()
        {
            this.InitializeComponent();
        }
        Album album = new Album();

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.DataContext = await Album.GetAllAlbumsAsync();
        }

        private void Albums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is GridView gridView)
            {
                this.Frame.Navigate(typeof(AddSong), ((Album)gridView.SelectedValue).AlbumTitle);
            }
        }

        private void Albums_ItemClick(object sender, ItemClickEventArgs e)
        {
            string txt = "You clicked " + e.ClickedItem.ToString() + ".";

        }

        private void ButtonSaveAlbum_Click(object sender, RoutedEventArgs e)
        {
            // Save into file
            album.AlbumTitle = AlbumTitle.Text;
            Album.WriteAlbum(album);
            this.Frame.Navigate(typeof(AlbumPage));
        }
        private void ButtonPickCover_Click(object sender, RoutedEventArgs e)
        {
            PickImageAsync();
        }

        private async System.Threading.Tasks.Task PickImageAsync()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                await file.CopyAsync(ApplicationData.Current.LocalFolder);
                album.AlbumCover = file.Name;
            }
            else
            {
                // OutputTextBlock.Text = "Operation cancelled.";
            }
        }

    }

}


