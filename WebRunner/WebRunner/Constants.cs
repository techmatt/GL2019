using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WebRunner
{
    class Constants
    {
        //public Font consoleFont = new Font(new FontFamily("Times New Roman"), 32, FontStyle.Regular, GraphicsUnit.Pixel);
        public Font consoleFont = new Font(new FontFamily("CONSOLAS"), 16, FontStyle.Regular, GraphicsUnit.Pixel);
        public SolidBrush consoleBackgroundBrush = new SolidBrush(Color.FromArgb(255, 40, 40, 40));
        public SolidBrush consoleFontBrush = new SolidBrush(Color.FromArgb(255, 24, 190, 24));

        static public int renderWidth = 1280;
        static public int renderHeight = 720;
    }

    enum ToolType
    {
        Shield,
        Broom,
        Probe,
        Bomb,
        EMP,
        Distraction,
    }

    class ToolData
    {
        public ToolData(ToolType _type, string _name, Color _color)
        {
            type = _type;
            name = _name;
            color = _color;
        }
        public ToolType type;
        public string name;
        public Color color;
    }

    class GameData
    {
        public GameData()
        {
            IDToToolDict[0] = ToolType.Shield;
            IDToToolDict[1] = ToolType.Broom;
            IDToToolDict[2] = ToolType.Probe;
            IDToToolDict[3] = ToolType.Bomb;
            IDToToolDict[4] = ToolType.EMP;
            IDToToolDict[5] = ToolType.Distraction;

            toolTypeToDataDict[ToolType.Shield] = new ToolData(ToolType.Shield, "shield", Color.FromArgb(200, 50, 50));
        }
        public ToolType getToolType(int id)
        {
            return IDToToolDict[id];
        }
        public ToolData getToolData(ToolType type)
        {
            return toolTypeToDataDict[type];
        }
        Dictionary<int, ToolType> IDToToolDict = new Dictionary<int, ToolType>();
        Dictionary<ToolType, ToolData> toolTypeToDataDict = new Dictionary<ToolType, ToolData>();
    }
}
