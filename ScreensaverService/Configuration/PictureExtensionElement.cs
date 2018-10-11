using System.Configuration;

namespace ScreensaverService.Configuration
{
    class PictureExtension : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "jpg", IsRequired = true, IsKey = true)]
        public string Value
        {
            get => (string)this["value"];
            set => value = (string)this["value"];
        }
    }
}
