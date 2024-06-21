using System.Windows.Controls;

namespace StokTakip.Services
{
    public static class PageCache
    {
        private static readonly Dictionary<Type, Page> _pages = new Dictionary<Type, Page>();

        public static Page GetPage(Type pageType)
        {
            if (_pages.ContainsKey(pageType))
            {
                return _pages[pageType];
            }
            return null;
        }

        public static void AddPage(Type pageType, Page page)
        {
            if (!_pages.ContainsKey(pageType))
            {
                _pages[pageType] = page;
            }
        }

        public static void ClearCache()
        {
            _pages.Clear();
        }
    }
}