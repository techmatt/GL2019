using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class Structure
    {
        public Structure(StructureType _type, Vec2 _worldPos)
        {
            type = _type;
            worldPos = _worldPos;
        }
        public StructureType type;
        public Vec2 worldPos;
    }

    class GameLevel
    {
        public Rect2 worldRect;
        public List<Structure> structures;
        public string backgroundName;

        public GameLevel(string filename, double xStart)
        {
            structures = new List<Structure>();
            worldRect = Rect2.fromOriginSize(new Vec2(xStart, 0), Constants.viewportSize);
            backgroundName = "brushedMetal";
            if (filename == "editor")
            {
                Structure randomCamera = new Structure(StructureType.Camera, worldRect.randomPoint());
                structures.Add(randomCamera);
                return;
            }
            if (filename == "debug")
            {
                for (int i = 0; i < 3; i++)
                {
                    Structure randomCamera = new Structure(StructureType.Camera, worldRect.randomPoint());
                    structures.Add(randomCamera);

                    Structure randomWall = new Structure(StructureType.Wall, worldRect.randomPoint());
                    structures.Add(randomWall);
                }
                return;
            }
        }

        public void render(GameScreen gameScreen, GameData data, GameState state)
        {
            Vec2 viewportOrigin = state.viewport.pMin;
            foreach (Structure structure in structures)
            {
                gameScreen.drawImage(data.images.structures[structure.type], structure.worldPos - viewportOrigin);
            }
        }

        public void removeStructure(int structureIndex)
        {
            structures.RemoveAt(structureIndex);
        }
    }
}
