using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pulse
{
    class GameDatabase
    {
        public GameDatabase()
        {
            images = new ImageDatabase();
        }
        
        public ImageDatabase images;
    }
}
