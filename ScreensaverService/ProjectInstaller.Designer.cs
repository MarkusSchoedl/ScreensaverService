namespace ScreensaverService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ScreensaverServiceInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ScreensaverServiceInstaller
            // 
            this.ScreensaverServiceInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ScreensaverServiceInstaller.Password = null;
            this.ScreensaverServiceInstaller.Username = null;
            // 
            // ServiceInstaller
            // 
            this.ServiceInstaller.Description = "Copies all the Windows Wallpapers to another directory if they match one of the g" +
    "iven sizes.";
            this.ServiceInstaller.DisplayName = "Screensaver Service";
            this.ServiceInstaller.ServiceName = "Screensaver Service";
            this.ServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.ServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ScreensaverServiceInstaller,
            this.ServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ScreensaverServiceInstaller;
        private System.ServiceProcess.ServiceInstaller ServiceInstaller;
    }
}