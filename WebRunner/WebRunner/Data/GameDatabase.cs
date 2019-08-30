using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WebRunner
{
    enum ShapeType
    {
        Square,
        Circle
    }
    enum StructureType
    {
        Camera,
        Shielding,
        Firewall,
        Wall,
        SpawnPoint,
        Door,
        Objective,
    }

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
            brush = new SolidBrush(color);
        }
        public ToolType type;
        public string name;
        public Color color;
        public Brush brush;
    }

    class StructureEntry
    {
        public StructureEntry(StructureType _type, string _name, double _radius, ShapeType _shape, Vec2 _gridSize)
        {
            type = _type;
            name = _name;
            radius = _radius;
            shape = _shape;
            gridSize = _gridSize;
        }
        public StructureType type;
        public string name;
        public double radius;
        public ShapeType shape;
        public Vec2 gridSize;
    }

    class GameDatabase
    {
        void registerTool(ToolType _type, string _name, Color _color)
        {
            toolTypeToDataDict[_type] = new ToolData(_type, _name, _color);
        }
        void registerStructure(StructureType _type, string _name, double _radius, ShapeType _shape, Vec2 _gridSize)
        {
            structureTypeToDataDict[_type] = new StructureEntry(_type, _name, _radius, _shape, _gridSize);
        }
        public GameDatabase()
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

            registerStructure(StructureType.Camera, "camera", 36.0, ShapeType.Circle, new Vec2(2, 2));
            registerStructure(StructureType.Wall, "wall", 20.0, ShapeType.Square, new Vec2(1, 1));
            registerStructure(StructureType.Shielding, "shielding", 20.0, ShapeType.Square, new Vec2(1, 1));
            registerStructure(StructureType.Firewall, "firewall", 20.0, ShapeType.Square, new Vec2(1, 1));
            registerStructure(StructureType.SpawnPoint, "spawnpoint", 36.0, ShapeType.Circle, new Vec2(2, 2));
            registerStructure(StructureType.Door, "door", 36.0, ShapeType.Square, new Vec2(2, 2));
            registerStructure(StructureType.Objective, "objective", 32.0, ShapeType.Square, new Vec2(2, 2));

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
        public StructureEntry getStructureEntry(StructureType type)
        {
            return structureTypeToDataDict[type];
        }
        Dictionary<int, ToolType> IDToToolDict = new Dictionary<int, ToolType>();
        Dictionary<ToolType, ToolData> toolTypeToDataDict = new Dictionary<ToolType, ToolData>();
        Dictionary<StructureType, StructureEntry> structureTypeToDataDict = new Dictionary<StructureType, StructureEntry>();

        public Brush cameraBrushInterior = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
        public Pen cameraPenThin = new Pen(Color.FromArgb(255, 0, 0, 0), 1.5f);
        public Pen cameraPenThick = new Pen(Color.FromArgb(255, 0, 0, 0), 5.0f);
        public Pen cameraRay = new Pen(Color.FromArgb(255, 240, 240, 140), 6.0f);

        public ImageDatabase images;
    }
}
