using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.ServiceProcess;

namespace ScreensaverService
{
    public partial class Service1 : ServiceBase
    {
        private string _winpath = @"C:\Users\marku\AppData\Local\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets\";
        private string _picpath = @"C:\Users\marku\Pictures\WinBackgrounds";
        private string _picExt = "jpg";
        private string[] _files;

        private List<Size> _applicablePictureSizes = new List<Size>();

        private FileSystemWatcher _fileWatcher = new FileSystemWatcher();

        public Service1()
        {
            InitializeComponent();

            _applicablePictureSizes.Add(new Size(1920, 1080));
            _applicablePictureSizes.Add(new Size(2560, 1440));
            _applicablePictureSizes.Add(new Size(3840, 2160));
        }

        protected override void OnStart(string[] args)
        {
            _fileWatcher.Path = _winpath;
            _fileWatcher.Created += PossibleBackgroundCreated;
            _fileWatcher.EnableRaisingEvents = true;
        }

        private void PossibleBackgroundCreated(object sender, FileSystemEventArgs e)
        {
            string sourcePath = Path.Combine(_fileWatcher.Path, e.Name);
            Image img;

            try
            {
                img = Image.FromFile(sourcePath);
            }
            catch (OutOfMemoryException ex)
            {
                // A new File was found but was not in the format of an image.
                return;
            }
            catch (Exception ex)
            {
                // derfuq
                return;
            }
            
            if (_applicablePictureSizes.Contains(img.Size))
            {
                string destPath = Path.Combine(_picpath, e.Name) + "." + _picExt;
                File.Copy(sourcePath, destPath, true);
            }
        }

        protected override void OnStop()
        {
            _fileWatcher.Created -= PossibleBackgroundCreated;
        }
    }
}
