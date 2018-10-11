using System.Configuration;

namespace ScreensaverService
{
    class PictureSize : ConfigurationElement
    {
        [ConfigurationProperty("width", DefaultValue = 1920, IsRequired = true)]
        public int Width
        {
            get => (int)this["width"];
            set => value = (int)this["width"];
        }

        [ConfigurationProperty("height", DefaultValue = 1080, IsRequired = true)]
        public int Height
        {
            get => (int)this["height"];
            set => value = (int)this["height"];
        }

        [ConfigurationProperty("name", DefaultValue = "FullHD", IsRequired = true, IsKey = true)]
        public string Name
        {
            get => (string) this["name"];
            set => value = (string) this["name"];
        }
    }
}