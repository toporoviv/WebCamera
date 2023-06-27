using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WebCamera.MVVM.Core;
using WebCamera.MVVM.Models;

namespace WebCamera.MVVM.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly string _url = "https://streamer.camera.rt.ru/public/master.m3u8?sid=3093e499-e57b-4445-8189-cfca33810bfb";
        private ObservableCollection<Capture> _captures;
        private Uri _videoSource;

        public ObservableCollection<Capture> Captures
        {
            get => _captures;
            set
            {
                if (_captures != value)
                {
                    _captures = value;
                    OnPropertyChanged(nameof(Captures));
                }
            }
        }

        public Uri VideoSource
        {
            get { return _videoSource; }
            set
            {
                _videoSource = value;
                OnPropertyChanged(nameof(VideoSource));
            }
        }

        public int ImageWidth
        {
            get => 86;
        }

        public int ImageHeight
        {
            get => 86;
        }

        public int CaptureWidth
        {
            get => 760;
        }

        public int CaptureHeight
        {
            get => 450;
        }

        public MainViewModel()
        {
            _captures = new ObservableCollection<Capture>();
            VideoSource = new Uri(_url);
        }

        public ICommand AddCapture
        {
            get => new RelayCommand(async obj =>
            {
                var media = obj as MediaElement;
                var dpi = new System.Drawing.Size(96, 96);
                RenderTargetBitmap bmp = new RenderTargetBitmap(CaptureWidth, CaptureHeight, dpi.Width, dpi.Height, PixelFormats.Default);
                bmp.Render(media);

                var scaleTransform = new ScaleTransform(ImageWidth / (double)bmp.PixelWidth, ImageHeight / (double)bmp.PixelHeight);
                var transformedBitmap = new TransformedBitmap(bmp, scaleTransform);

                Captures.Add(new Capture
                {
                    Time = DateTime.Now.ToString("HH:mm:ss"),
                    Image = transformedBitmap
                });
            });
        }
    }
}
