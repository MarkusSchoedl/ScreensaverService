using System.Configuration;

namespace ScreensaverService.Configuration
{
    class PictureConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("ApplicableSizes", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(PictureSize),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public PictureSizeCollection PictureSizes
        {
            get
            {
                return (PictureSizeCollection)base["ApplicableSizes"];
            }
        }
        
        [ConfigurationProperty("PictureExtension", IsDefaultCollection = false)]
        public PictureExtension PictureExtension
        {
            get
            {
                return (PictureExtension)base["PictureExtension"];
            }
        }
    }
}
