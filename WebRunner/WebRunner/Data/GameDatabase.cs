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
        Circle,
        Mirror
    }
    enum StructureType
    {
        // permanent structures
        Camera,
        Shielding,
        Firewall,
        Wall,
        SpawnPointA,
        SpawnPointB,
        Door,
        Objective,
        Shoes,
        LaserGun,
        StationaryMirror,
        LaserTurret,
        BulletTurret,

        // temporary structures
        RunnerA,
        RunnerB,
        Distraction,
        RunnerMirror
    }

    enum ToolType
    {
        RunA,
        RunB,
        Distraction,
        InvalidID
    }

    class ToolEntry
    {
        public ToolEntry(ToolType _type, string _name, Color _debugColor, ImageEntry _image)
        {
            type = _type;
            image = _image;
            name = _name;
            debugColor = _debugColor;
            //brush = new SolidBrush(color);
        }
        public ToolType type;
        public ImageEntry image;
        public string name;
        public Color debugColor;
        //public Brush brush;
    }

    class StructureEntry
    {
        public StructureEntry(StructureType _type, string _name, double _radius, ShapeType _shape, double _health, Vec2 _gridSize, ImageEntry _image)
        {
            type = _type;
            image = _image;
            name = _name;
            radius = _radius;
            shape = _shape;
            health = _health;
            gridSize = _gridSize;
        }
        public bool isReflective()
        {
            return (type == StructureType.RunnerMirror || type == StructureType.StationaryMirror);
        }
        public StructureType type;
        public ImageEntry image;
        public string name;
        public double radius;
        public double health;
        public ShapeType shape;
        public Vec2 gridSize;
    }

    class GameDatabase
    {
        void registerTool(ToolType _type, string _name, Color _color)
        {
            toolTypeToDataDict[_type] = new ToolEntry(_type, _name, _color, images.tools[_type]);
        }
        void registerStructure(StructureType _type, string _name, double _radius, ShapeType _shape, double _health, Vec2 _gridSize)
        {
            structureTypeToDataDict[_type] = new StructureEntry(_type, _name, _radius, _shape, _health, _gridSize, images.structures[_type]);
        }
        public GameDatabase()
        {
            images = new ImageDatabase();

            IDToToolDict[0] = ToolType.RunA;
            IDToToolDict[1] = ToolType.RunB;
            IDToToolDict[2] = ToolType.RunA;
            IDToToolDict[3] = ToolType.RunB;
            IDToToolDict[4] = ToolType.RunA;
            IDToToolDict[5] = ToolType.Distraction;

            registerTool(ToolType.RunA, "runA", Color.FromArgb(200, 50, 50));
            registerTool(ToolType.RunB, "runB", Color.FromArgb(50, 200, 50));
            registerTool(ToolType.Distraction, "distraction", Color.FromArgb(50, 200, 50));

            registerStructure(StructureType.Camera, "camera", 36.0, ShapeType.Circle, 1.0, new Vec2(2, 2));
            registerStructure(StructureType.BulletTurret, "bulletTurret", 36.0, ShapeType.Circle, 1.0, new Vec2(2, 2));
            registerStructure(StructureType.LaserTurret, "laserTurret", 36.0, ShapeType.Circle, 1.0, new Vec2(2, 2));
            registerStructure(StructureType.StationaryMirror, "stationaryMirror", 36.0, ShapeType.Mirror, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.Wall, "wall", 20.0, ShapeType.Square, 0.0, new Vec2(1, 1));
            registerStructure(StructureType.Shielding, "shielding", 20.0, ShapeType.Square, 1.0, new Vec2(1, 1));
            registerStructure(StructureType.Firewall, "firewall", 20.0, ShapeType.Square, 1.0, new Vec2(1, 1));
            registerStructure(StructureType.SpawnPointA, "spawnpointA", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.SpawnPointB, "spawnpointB", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.Door, "door", 36.0, ShapeType.Square, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.Objective, "objective", 32.0, ShapeType.Square, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.Shoes, "shoes", 32.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.LaserGun, "laserGun", 32.0, ShapeType.Circle, 0.0, new Vec2(2, 2));

            registerStructure(StructureType.RunnerA, "runnerA", Constants.runnerRadius, ShapeType.Circle, 0.0, null);
            registerStructure(StructureType.RunnerB, "runnerB", Constants.runnerRadius, ShapeType.Circle, 0.0, null);
            registerStructure(StructureType.Distraction, "distraction", 40.0f, ShapeType.Circle, 0.0, null);
        }
        public ToolType getToolType(int id)
        {
            if (IDToToolDict.ContainsKey(id))
                return IDToToolDict[id];
            else
                return ToolType.InvalidID;
        }
        public ToolEntry getToolData(ToolType type)
        {
            return toolTypeToDataDict[type];
        }
        public StructureEntry getStructureEntry(StructureType type)
        {
            return structureTypeToDataDict[type];
        }
        Dictionary<int, ToolType> IDToToolDict = new Dictionary<int, ToolType>();
        Dictionary<ToolType, ToolEntry> toolTypeToDataDict = new Dictionary<ToolType, ToolEntry>();
        Dictionary<StructureType, StructureEntry> structureTypeToDataDict = new Dictionary<StructureType, StructureEntry>();

        public Brush cameraBrushInterior = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
        public Pen cameraPenThin = new Pen(Color.FromArgb(255, 0, 0, 0), 1.5f);
        public Pen cameraPenThick = new Pen(Color.FromArgb(255, 0, 0, 0), 5.0f);
        public Pen cameraRay = new Pen(Color.FromArgb(255, 240, 240, 140), 6.0f);
        
        public Pen laserTurretRay = new Pen(Color.FromArgb(255, 163, 73, 164), 4.0f);
        public Pen laserGunRay = new Pen(Color.FromArgb(255, 63, 73, 204), 4.0f);

        public HashSet<StructureType> runnerBlockingStructures = new HashSet<StructureType>() {
            StructureType.Wall, StructureType.Firewall, StructureType.Camera,
            StructureType.Shielding };

        public HashSet<StructureType> cameraBlockingStructures = new HashSet<StructureType>() {
            StructureType.Wall, StructureType.RunnerA, StructureType.RunnerB, StructureType.Distraction,
            StructureType.StationaryMirror};

        public HashSet<StructureType> laserTurretBlockingStructures = new HashSet<StructureType>() {
            StructureType.Wall, StructureType.RunnerA, StructureType.RunnerB, StructureType.Distraction,
            StructureType.StationaryMirror, StructureType.RunnerMirror};

        public HashSet<StructureType> runnerLaserBlockingStructures = new HashSet<StructureType>() {
            StructureType.Wall, StructureType.RunnerMirror, StructureType.StationaryMirror };

        public ImageDatabase images;
    }
}
