using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WebCamera.MVVM.Models
{
    public class Capture
    {
        public BitmapSource Image { get; set; }

        public string Time { get; set; }
    }
}
