﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class EditorManager
    {
        public EditorManager()
        {
            
        }

        public GameManager manager;
        public GameDatabase database;
        public GameLevel level;
        public EditorTool activeTool = EditorTool.Select;
        public StructureType activeStructureType;
        public Vec2 hoverPos = new Vec2();
        public bool hoverPosValidForPlacement = false;
        public int selectedStructureIndex = -1;
        
        public Vec2 lockToGrid(Vec2 coord, bool lockToMiddleOfCell)
        {
            Vec2 result = ((coord / Constants.gridSize).floor() + new Vec2(0.5, 0.5)) * Constants.gridSize;
            if (lockToMiddleOfCell)
                result += new Vec2(Constants.gridSize / 2.0, Constants.gridSize / 2.0);
            return result;
        }

        public void mouseMove(Vec2 coord)
        {
            if (activeTool == EditorTool.Select)
            {
                hoverPos = coord;
            }
            if (activeTool == EditorTool.Structure)
            {
                Vec2 size = manager.database.getStructureEntry(activeStructureType).gridSize;
                bool lockToMiddleOfCell = false;
                if ((int)size.x == 2) lockToMiddleOfCell = true;
                hoverPos = lockToGrid(coord, lockToMiddleOfCell);

                var nearestStructure = closestStructure(level.structures, hoverPos);
                StructureEntry hoverStructureData = database.getStructureEntry(activeStructureType);
                hoverPosValidForPlacement = (nearestStructure.Item2 > hoverStructureData.radius);
            }
        }

        internal void setToolStructure(StructureType structure)
        {
            activeTool = EditorTool.Structure;
            activeStructureType = structure;
            selectedStructureIndex = -1;
        }

        public double distToStructure(Structure structure, Vec2 pos)
        {
            var entry = manager.database.getStructureEntry(structure.type);
            if (entry.shape == ShapeType.Circle)
                return DistUtil.pointToCircleDist(pos, structure.center, entry.radius);
            if (entry.shape == ShapeType.Square)
                return Math.Sqrt(DistUtil.pointToSquareDistSq(pos, structure.center, entry.radius - 0.5));
            throw new Exception("invalid shape");
        }

        public Tuple<int, double> closestStructure(List<Structure> structures, Vec2 pos)
        {
            double result = double.MaxValue;
            int bestIdx = -1;
            for(int structureIdx = 0; structureIdx < structures.Count(); structureIdx++)
            {
                Structure structure = structures[structureIdx];
                double dist = distToStructure(structure, pos);
                if (dist < result)
                {
                    result = dist;
                    bestIdx = structureIdx;
                }
            }
            return new Tuple<int, double>(bestIdx, result);
        }

        public void leftMouseDown(Vec2 coord)
        {
            mouseMove(coord);
            if (activeTool == EditorTool.Select)
            {
                var nearestStructure = closestStructure(level.structures, hoverPos);
                if (nearestStructure.Item2 <= 1e-5)
                {
                    selectedStructureIndex = nearestStructure.Item1;
                }
            }
            if (activeTool == EditorTool.Structure)
            {
                if (!hoverPosValidForPlacement) return;
                Structure newStructure = new Structure(activeStructureType, database, hoverPos);
                level.structures.Add(newStructure);
            }
        }

        public void rightMouseDown(Vec2 coord)
        {
            mouseMove(coord);
            if (activeTool == EditorTool.Select)
            {
                var nearestStructure = closestStructure(level.structures, hoverPos);
                if (nearestStructure.Item2 <= 1e-5)
                {
                    level.removeStructure(nearestStructure.Item1);
                    selectedStructureIndex = -1;
                }
            }
        }

        public Structure getSelectedStructure()
        {
            if (selectedStructureIndex == -1 || selectedStructureIndex >= level.structures.Count())
                return null;
            return level.structures[selectedStructureIndex];
        }
    }
}
