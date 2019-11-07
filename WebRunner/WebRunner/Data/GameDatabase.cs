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
        GlassWall,
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

        //DistractionPickup,
        MirrorPickup,
        //BombPickup,
        MedpackPickup,
        //BotnetPickup,
        DysonPickup,
        CloakingFieldPickup,
        KusanagiPickup,

        // temporary structures
        RunnerA,
        RunnerB,
        RunnerMirror,
        DistractionInstance,
    }

    enum ToolType
    {
        //RunA,
        //RunB,
        Mirror,
        Medpack,
        //Bomb,
        //Botnet,
        //Distraction,
        CloakingField,
        Kusanagi,
        Dyson,
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
        public StructureEntry(StructureType _type, string _name, double _radius, ShapeType _shape, double _maxHealth, Vec2 _gridSize, ImageEntry _image)
        {
            type = _type;
            image = _image;
            name = _name;
            radius = _radius;
            shape = _shape;
            maxHealth = _maxHealth;
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
        public double maxHealth;
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
            ImageEntry image = null;
            if (_type != StructureType.Wall && _type != StructureType.Door)
                image = images.getStructureImage(_type);
            structureTypeToDataDict[_type] = new StructureEntry(_type, _name, _radius, _shape, _health, _gridSize, image);
        }
        public GameDatabase()
        {
            images = new ImageDatabase();

            IDToToolDict[0] = ToolType.Mirror; //cyan
            IDToToolDict[1] = ToolType.Medpack; //green
            IDToToolDict[2] = ToolType.Dyson; //red
            //IDToToolDict[3] = ToolType.; //missing
            IDToToolDict[4] = ToolType.Mirror; //cyan
            IDToToolDict[5] = ToolType.CloakingField; //yellow
            IDToToolDict[6] = ToolType.Kusanagi; // violet
            IDToToolDict[7] = ToolType.Mirror; //cyan
            //IDToToolDict[8] = ToolType.Bomb; //blue
            
            registerTool(ToolType.Mirror, "runnerMirror", Color.FromArgb(200, 50, 50));
            //registerTool(ToolType.RunB, "runB", Color.FromArgb(50, 200, 50));
            //registerTool(ToolType.Distraction, "distraction", Color.FromArgb(50, 200, 50));
            registerTool(ToolType.Medpack, "medpack", Color.FromArgb(50, 200, 50));
            //registerTool(ToolType.Botnet, "botnet", Color.FromArgb(50, 200, 50));
            //registerTool(ToolType.Bomb, "bomb", Color.FromArgb(50, 200, 50));
            registerTool(ToolType.CloakingField, "cloakingField", Color.FromArgb(50, 200, 50));
            registerTool(ToolType.Kusanagi, "kusanagi", Color.FromArgb(50, 200, 50));
            registerTool(ToolType.Dyson, "dyson", Color.FromArgb(50, 200, 50));

            double dh = 4.0; // default health

            registerStructure(StructureType.Camera, "camera", 36.0, ShapeType.Circle, dh, new Vec2(2, 2));
            //registerStructure(StructureType.BulletTurret, "bulletTurret", 36.0, ShapeType.Circle, dh, new Vec2(2, 2));
            registerStructure(StructureType.LaserTurret, "laserTurret", 36.0, ShapeType.Circle, dh, new Vec2(2, 2));
            registerStructure(StructureType.StationaryMirror, "stationaryMirror", 36.0, ShapeType.Mirror, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.Wall, "wall", 20.0, ShapeType.Square, 0.0, new Vec2(1, 1));
            registerStructure(StructureType.GlassWall, "glassWall", 20.0, ShapeType.Square, dh, new Vec2(1, 1));
            //registerStructure(StructureType.Firewall, "firewall", 20.0, ShapeType.Square, dh, new Vec2(1, 1));
            registerStructure(StructureType.SpawnPointA, "spawnpointA", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.SpawnPointB, "spawnpointB", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.Door, "door", 36.0, ShapeType.Square, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.Objective, "objective", 32.0, ShapeType.Square, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.Shoes, "shoes", 32.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.LaserGun, "laserGun", 32.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.RunnerMirror, "runnerMirror", Constants.runnerMirrorRadius, ShapeType.Mirror, 0.0, new Vec2(2, 2));

            //registerStructure(StructureType.BotnetPickup, "botnetPickup", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.MirrorPickup, "mirrorPickup", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            //registerStructure(StructureType.DistractionPickup, "distractionPickup", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.MedpackPickup, "medpackPickup", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            //registerStructure(StructureType.BombPickup, "bombPickup", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.CloakingFieldPickup, "cloakingFieldPickup", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.DysonPickup, "dysonPickup", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));
            registerStructure(StructureType.KusanagiPickup, "kusanagiPickup", 36.0, ShapeType.Circle, 0.0, new Vec2(2, 2));

            registerStructure(StructureType.RunnerA, "runnerA", Constants.runnerRadius, ShapeType.Circle, 0.0, null);
            registerStructure(StructureType.RunnerB, "runnerB", Constants.runnerRadius, ShapeType.Circle, 0.0, null);
            //registerStructure(StructureType.Distraction, "distraction", 40.0f, ShapeType.Circle, 0.0, null);
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
        public List<ToolType> toolList()
        {
            return new List<ToolType>(toolTypeToDataDict.Keys);
        }
        public StructureEntry getStructureEntry(StructureType type)
        {
            return structureTypeToDataDict[type];
        }
        Dictionary<int, ToolType> IDToToolDict = new Dictionary<int, ToolType>();
        Dictionary<ToolType, ToolEntry> toolTypeToDataDict = new Dictionary<ToolType, ToolEntry>();
        Dictionary<StructureType, StructureEntry> structureTypeToDataDict = new Dictionary<StructureType, StructureEntry>();

        public Brush cameraBrushInterior = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
        public Brush runnerHealthInterior = new SolidBrush(Color.FromArgb(200, 237, 40, 40));
        public Brush laserIndicatorInterior = new SolidBrush(Color.FromArgb(220, 63, 73, 204));
        public Brush structureHealth = new SolidBrush(Color.FromArgb(200, 137, 143, 224));
        public Pen cameraPenThin = new Pen(Color.FromArgb(255, 0, 0, 0), 1.5f);
        public Pen cameraPenThick = new Pen(Color.FromArgb(255, 0, 0, 0), 5.0f);

        public Pen cameraRayA = new Pen(Color.FromArgb(100, 240, 240, 140), 8.0f);
        public Pen cameraRayB = new Pen(Color.FromArgb(150, 240, 240, 140), 4.0f);

        public Pen laserTurretRayA = new Pen(Color.FromArgb(100, 200, 120, 200), 8.0f);
        public Pen laserTurretRayB = new Pen(Color.FromArgb(150, 137, 61, 137), 4.0f);
        public Pen laserGunRayA = new Pen(Color.FromArgb(100, 100, 100, 240), 8.0f);
        public Pen laserGunRayB = new Pen(Color.FromArgb(150, 100, 100, 240), 4.0f);

        public HashSet<StructureType> runnerBlockingStructures = new HashSet<StructureType>() {
            StructureType.Wall, StructureType.Firewall, StructureType.Camera,
            StructureType.GlassWall };

        public HashSet<StructureType> cameraBlockingStructures = new HashSet<StructureType>() {
            StructureType.Wall, StructureType.RunnerA, StructureType.RunnerB,
            StructureType.StationaryMirror};

        public HashSet<StructureType> laserTurretBlockingStructures = new HashSet<StructureType>() {
            StructureType.Wall, StructureType.RunnerA, StructureType.RunnerB, StructureType.Camera, StructureType.LaserTurret,
            StructureType.StationaryMirror, StructureType.RunnerMirror };

        public HashSet<StructureType> runnerLaserBlockingStructures = new HashSet<StructureType>() {
            StructureType.Wall, StructureType.RunnerMirror, StructureType.Camera, StructureType.LaserTurret,
            StructureType.StationaryMirror, StructureType.RunnerMirror };

        public ImageDatabase images;
    }
}
