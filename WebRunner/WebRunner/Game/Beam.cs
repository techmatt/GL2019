using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class BeamPart
    {
        public BeamPart(Vec2 _p0, Vec2 _p1)
        {
            p0 = _p0;
            p1 = _p1;
        }
        Vec2 p0, p1;
    }

    enum BeamType
    {
        RunnerLaserGun,
        TurretLaser,
    }

    class Beam
    {
        public Beam(Vec2 center0, Vec2 dir0, BeamType _type, GameManager manager)
        {
            type = _type;

            var state = manager.state;
            var database = manager.database;
            var level = state.activeLevel;
            var structureLists = new List<List<Structure>> { level.structures, state.curFrameTemporaryStructures };
            var curCenter = center0;
            var curDir = dir0;

            var blockingStructures = database.runnerLaserBlockingStructures;
            if(_type == BeamType.TurretLaser)
                blockingStructures = database.turretLaserBlockingStructures;

            for (int pathIndex = 0; pathIndex < Constants.maxBeamBounces; pathIndex++)
            {
                var intersection = Util.findFirstRayStructureIntersection(structureLists, curCenter, curDir, blockingStructures);
                
                var newCenter = curCenter + curDir * intersection.Item1;
                bool continueBounce = false;

                if (intersection.Item2 != -1)
                {
                    Structure hitStructure = structureLists[intersection.Item2][intersection.Item3];
                    if (hitStructure.type == StructureType.RunnerA)
                    {
                        state.killRunnerA();
                    }
                    if (hitStructure.type == StructureType.RunnerB)
                    {
                        state.killRunnerB();
                    }
                    if(hitStructure.type == StructureType.RunnerMirror || hitStructure.type == StructureType.StationaryMirror)
                    {

                    }
                }

                parts.Add(new BeamPart(curCenter, newCenter));
                curCenter = newCenter;

                if (!continueBounce)
                    break;
            }
        }

        public BeamType type;
        public List<BeamPart> parts = new List<BeamPart>();
        
    }

}
