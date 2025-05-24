using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Controls;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using ImageEditorApp; 

namespace ImageEditorApp
{
    public partial class MainPage : ContentPage
    {
        private ImageEditor editor = new ImageEditor();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void LoadImage_Clicked(object sender, EventArgs e)
        {
            var file = await FilePicker.PickAsync();
            if (file != null)
            {
                editor.LoadImage(file.FullPath);
                UpdateImageView();
            }
        }

        private void ConvertToGrayscale_Clicked(object sender, EventArgs e)
        {
            editor.ConvertToGrayscale();
            UpdateImageView();
        }

        private void Undo_Clicked(object sender, EventArgs e)
        {
            editor.Undo();
            UpdateImageView();
        }

        private void Redo_Clicked(object sender, EventArgs e)
        {
            editor.Redo();
            UpdateImageView();
        }

        private async void SaveImage_Clicked(object sender, EventArgs e)
        {
            var file = await FilePicker.PickSaveAsync();
            if (file != null)
            {
                editor.SaveImage(file.FullPath);
            }
        }

        private void UpdateImageView()
        {
            using (var image = editor.GetCurrentImage())
            {
                if (image != null)
                {
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    using (var stream = data.AsStream())
                    {
                        ImageView.Source = ImageSource.FromStream(() => stream);
                    }
                }
            }
        }
    }
}
