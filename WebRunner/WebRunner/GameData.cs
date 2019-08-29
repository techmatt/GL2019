using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WebRunner
{
    enum ToolType
    {
        Shield,
        Broom,
        Probe,
        Bomb,
        EMP,
        Distraction,
        InvalidID
    }

    class ToolData
    {
        public ToolData(ToolType _type, string _name, Color _color)
        {
            type = _type;
            name = _name;
            color = _color;
            brush = new System.Drawing.SolidBrush(color);
        }
        public ToolType type;
        public string name;
        public Color color;
        public Brush brush;
    }

    class GameData
    {
        void registerTool(ToolType _type, string _name, Color _color)
        {
            toolTypeToDataDict[_type] = new ToolData(_type, _name, _color);
        }
        public GameData()
        {
            IDToToolDict[0] = ToolType.Shield;
            IDToToolDict[1] = ToolType.Broom;
            IDToToolDict[2] = ToolType.Probe;
            IDToToolDict[3] = ToolType.Bomb;
            IDToToolDict[4] = ToolType.EMP;
            IDToToolDict[5] = ToolType.Distraction;

            registerTool(ToolType.Shield, "shield", Color.FromArgb(200, 50, 50));
            registerTool(ToolType.Broom, "broom", Color.FromArgb(50, 200, 50));
            registerTool(ToolType.Probe, "probe", Color.FromArgb(50, 50, 200));
            registerTool(ToolType.Bomb, "bomb", Color.FromArgb(200, 200, 50));
            registerTool(ToolType.EMP, "emp", Color.FromArgb(200, 50, 200));
            registerTool(ToolType.Distraction, "distraction", Color.FromArgb(50, 200, 200));

            //bmpShield = new Bitmap(Constants.imageOriginalDir + "shield.png");
            images = new ImageDatabase();
        }
        public ToolType getToolType(int id)
        {
            if (IDToToolDict.ContainsKey(id))
                return IDToToolDict[id];
            else
                return ToolType.InvalidID;
        }
        public ToolData getToolData(ToolType type)
        {
            return toolTypeToDataDict[type];
        }
        Dictionary<int, ToolType> IDToToolDict = new Dictionary<int, ToolType>();
        Dictionary<ToolType, ToolData> toolTypeToDataDict = new Dictionary<ToolType, ToolData>();

        //public Bitmap bmpShield;
        public ImageDatabase images;
    }
}
