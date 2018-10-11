using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using ScreensaverService.Configuration;

namespace ScreensaverService
{
    public partial class ScreensaverService : ServiceBase
    {
        #region Fields

        private string _winpath;
        private string _picpath;
        private string _picExt;

        private readonly List<Size> _applicablePictureSizes = new List<Size>();

        private readonly FileSystemWatcher _fileWatcher = new FileSystemWatcher();

        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().FullName);
        #endregion

        public ScreensaverService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _log.Info("======== Starting up Service ========");

            try
            {
                _log.Debug("Getting WinPath");
                _winpath = ConfigurationManager.AppSettings["WindowsPath"];
                _log.Debug("Getting UserPath");
                _picpath = ConfigurationManager.AppSettings["UserPath"];
            }
            catch (Exception e)
            {
                _log.Error("Error", e);
            }

            try
            {
                _log.Debug("Getting PictureConfig");
                string sectionName = "PictureConfig";

                if (!(ConfigurationManager.GetSection(sectionName) is PictureConfigSection picSizeList))
                {
                    _log.Error($"Could not load Section \"{sectionName}\"");
                }
                else
                {
                    _log.Debug("Getting PictureSizes");
                    foreach (PictureSize size in picSizeList.PictureSizes)
                    {
                        _applicablePictureSizes.Add(new Size(size.Width, size.Height));
                        _log.Info($"Applicable Size ({size.Name}): {size.Width}x{size.Height}");
                    }

                    _log.Debug("Getting PictureExtension");
                    _picExt = picSizeList.PictureExtension.Value;
                }
            }
            catch (Exception e)
            {
                _log.Error(e);
            }

            _log.Info("WinPath  : " + _winpath);
            _log.Info("UserPath : " + _picpath);
            _log.Info("PicExt   : " + _picExt);

            _fileWatcher.Path = _winpath;
            _fileWatcher.Created += PossibleBackgroundCreated;
            _fileWatcher.EnableRaisingEvents = true;
        }

        private void PossibleBackgroundCreated(object sender, FileSystemEventArgs e)
        {
            _log.Debug("New File arrived.");

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

            _log.Debug("Its an image with a size of " + img.Size.Width + "x" + img.Size.Height);

            if (!_applicablePictureSizes.Contains(img.Size)) return;

            _log.Info("Found a picture with a matching Size. Copying picture to the users folder.");
            string destPath = Path.Combine(_picpath, e.Name) + "." + _picExt;
            File.Copy(sourcePath, destPath, true);
        }

        protected override void OnStop()
        {
            _fileWatcher.Created -= PossibleBackgroundCreated;
        }
    }
}
