using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class EditorManager
    {
        public EditorManager(GameManager _manager)
        {
            manager = _manager;
            data = manager.data;
            level = manager.state.levels[0];
        }

        public GameManager manager;
        public GameData data;
        public GameLevel level;
        public EditorTool activeTool;
        public StructureType activeStructureType;
        public Vec2 hoverPos = Vec2.Origin;
        public bool hoverPosValid = false;
        
        public Vec2 lockToGrid(Vec2 coord, bool lockToMiddleOfCell)
        {
            Vec2 result = ((coord / Constants.gridSize).floor() + new Vec2(0.5, 0.5)) * Constants.gridSize;
            if (lockToMiddleOfCell)
                result += new Vec2(Constants.gridSize / 2.0, Constants.gridSize / 2.0);
            return result;
        }

        public void mouseMove(Vec2 coord)
        {
            if (activeTool == EditorTool.Structure)
            {
                Vec2 size = manager.data.getStructureData(activeStructureType).gridSize;
                bool lockToMiddleOfCell = false;
                if ((int)size.x == 2) lockToMiddleOfCell = true;
                hoverPos = lockToGrid(coord, lockToMiddleOfCell);

                double distToNearest = closestStructureDist(level.structures, hoverPos);
                StructureData hoverStructureData = data.getStructureData(activeStructureType);
                hoverPosValid = (distToNearest > hoverStructureData.radius);
            }
        }

        internal void setToolStructure(StructureType structure)
        {
            activeTool = EditorTool.Structure;
            activeStructureType = structure;
        }

        public double distToStructure(Structure structure, Vec2 pos)
        {
            var data = manager.data.getStructureData(structure.type);
            if (data.shape == ShapeType.Circle)
                return DistUtil.pointToCircleDist(pos, structure.worldPos, data.radius);
            if (data.shape == ShapeType.Square)
                return Math.Sqrt(DistUtil.pointToSquareDistSq(pos, structure.worldPos, data.radius - 0.5));
            throw new Exception("invalid shape");
        }
        public double closestStructureDist(List<Structure> structures, Vec2 pos)
        {
            double result = double.MaxValue;
            foreach(Structure structure in structures)
            {
                result = Math.Min(result, distToStructure(structure, pos));
            }
            return result;
        }

        public void leftMouseDown(Vec2 coord)
        {
            mouseMove(coord);
            if (!hoverPosValid) return;
            if (activeTool == EditorTool.Structure)
            {
                Structure newStructure = new Structure(activeStructureType, hoverPos);
                level.structures.Add(newStructure);
            }
        }
    }
}
