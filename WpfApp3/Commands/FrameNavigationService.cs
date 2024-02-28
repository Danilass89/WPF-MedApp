using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp3.Commands
{
    public class FrameNavigationService
    {
        private readonly Frame _mainFrame;

        public FrameNavigationService(Frame mainFrame)
        {
            _mainFrame = mainFrame ?? throw new ArgumentNullException(nameof(mainFrame));
        }

        public void Navigate(Type sourcePageType)
        {
            if (sourcePageType == null)
            {
                throw new ArgumentNullException(nameof(sourcePageType));
            }

            if (_mainFrame.Content?.GetType() != sourcePageType)
            {
                _mainFrame.Navigate(sourcePageType);
            }
        }
    }

}
