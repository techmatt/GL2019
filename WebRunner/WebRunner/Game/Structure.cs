using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class Structure
    {
        public Structure(StructureType _type, GameDatabase _entry, Vec2 _center)
        {
            type = _type;
            entry = _entry.getStructureEntry(_type);
            center = _center;

            curHealth = entry.maxHealth;
        }

        public Structure(Dictionary<string, string> dict, GameDatabase database)
        {
            type = (StructureType)Enum.Parse(typeof(StructureType), dict["type"]);
            entry = database.getStructureEntry(type);
            center = new Vec2();
            curHealth = entry.maxHealth;
            center.x = Convert.ToDouble(dict["centerX"]);
            center.y = Convert.ToDouble(dict["centerY"]);
            sweepAngleStart = Convert.ToDouble(dict["sweepAngleStart"]);
            sweepAngleSpan = Convert.ToDouble(dict["sweepAngleSpan"]);
            sweepAngleSpeed = Convert.ToDouble(dict["sweepAngleSpeed"]);
        }

        public Vec2 curSweepDirection()
        {
            double theta = curSweepAngle * Math.PI / 180.0;
            return new Vec2(Math.Cos(theta), Math.Sin(theta));
        }

        public bool inGoodHealth()
        {
            if (entry.maxHealth <= 0.0)
                return true;
            return (curHealth / entry.maxHealth > 0.1);
        }

        public Vec2 curNormal()
        {
            double theta = (curSweepAngle + 90.0) * Math.PI / 180.0;
            return new Vec2(Math.Cos(theta), Math.Sin(theta));
        }

        public double sweepAngleEnd()
        {
            return sweepAngleStart + sweepAngleSpan;
        }

        public bool isTopLayer()
        {
            return (type == StructureType.LaserTurret || type == StructureType.Camera);
        }

        //
        // intrinsic parameters
        //
        public StructureType type;
        public StructureEntry entry;
        public Vec2 center;
        public double sweepAngleStart = 60.0, sweepAngleSpan = 150.0, sweepAngleSpeed = 5.0;

        //
        // transient parameters
        //
        public double curSweepAngle = 120.0;
        public int curSweepAngleSign = 1;
        public double curCameraViewDist = 0.0;
        public int curImgInstanceHash = Util.randInt(0, 1000000);
        public double curHealth = 0.0;
        public double disableTimeLeft = 0.0;
        public LaserPath laserPath = null;
        public bool achieved = false;
        public bool visible = true;
        public int speechRepeatDelay = 0;
        //public DateTime t;

        public Dictionary<string, string> toDict()
        {
            var result = new Dictionary<string, string>();
            result["type"] = type.ToString();
            result["centerX"] = center.x.ToString();
            result["centerY"] = center.y.ToString();
            result["sweepAngleStart"] = sweepAngleStart.ToString();
            result["sweepAngleSpan"] = sweepAngleSpan.ToString();
            result["sweepAngleSpeed"] = sweepAngleSpeed.ToString();
            return result;
        }

        
    }

}
