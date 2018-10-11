using System;
using System.Configuration;

namespace ScreensaverService.Configuration
{
    class PictureSizeCollection : ConfigurationElementCollection
    {
        public PictureSize this[int index]
        {
            get { return (PictureSize)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
        
        public void Add(PictureSize picSize)
        {
            BaseAdd(picSize);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new PictureSize();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PictureSize)element).Name;
        }

        public void Remove(PictureSize serviceConfig)
        {
            BaseRemove(serviceConfig.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }
    }
}
