using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.ServiceProcess;

namespace ScreensaverService
{
    public partial class Service1 : ServiceBase
    {
        #region Fields
        private string _winpath = @"C:\Users\marku\AppData\Local\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets\";
        private string _picpath = @"C:\Users\marku\Pictures\WinBackgrounds";
        private string _picExt = "jpg";

        private readonly List<Size> _applicablePictureSizes = new List<Size>();

        private readonly FileSystemWatcher _fileWatcher = new FileSystemWatcher();

        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().FullName);
        #endregion

        public Service1()
        {
            InitializeComponent();

            _applicablePictureSizes.Add(new Size(1920, 1080));
            _applicablePictureSizes.Add(new Size(2560, 1440));
            _applicablePictureSizes.Add(new Size(3840, 2160));
        }

        protected override void OnStart(string[] args)
        {
            _log.Info("======== Starting up Service ========");

            _fileWatcher.Path = _winpath;
            _fileWatcher.Created += PossibleBackgroundCreated;
            _fileWatcher.EnableRaisingEvents = true;
        }

        private void PossibleBackgroundCreated(object sender, FileSystemEventArgs e)
        {
            _log.Info("New File arrived.");

            string sourcePath = Path.Combine(_fileWatcher.Path, e.Name);
            Image img;

            try
            {
                img = Image.FromFile(sourcePath);
            }
            catch (OutOfMemoryException)
            {
                // A new File was found but was not in the format of an image.
                _log.Warn("The new File was not in the format of an image. Name: " + e.Name);
                return;
            }
            catch (Exception ex)
            {
                _log.Error("An unknown Exception was thrown. Name: " + e.Name + " - Exception: " + ex);
                return;
            }

            _log.Info("Its an image with a size of " + img.Size.Width + "x" + img.Size.Height);

            if (_applicablePictureSizes.Contains(img.Size))
            {
                _log.Info("Matching Size. Copying picture to the users folder.");
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
