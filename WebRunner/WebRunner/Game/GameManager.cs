using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebRunner
{
    class GameManager
    {
        public GameManager(PictureBox _targetBox, EditorManager _editor)
        {
            screen = new GameScreen(_targetBox, database);
            editor = _editor;
            if (editor != null)
            {
                editor.manager = this;
                editor.database = database;
            }
        }

        public GameDatabase database = new GameDatabase();
        public GameState state;
        GameScreen screen;
        public EditorManager editor = null;
        public VisionManager vision = new VisionManager();
        public JoystickManager joystick = new JoystickManager();
        public SoundManager sound = new SoundManager();

        public void startMission(string missionName, string levelName)
        {
            state = new GameState(this, missionName, levelName);
            if(editor != null)
                editor.level = state.allLevels[0];

            sound.playSpeech("mission start. dyson protocol acquired.");
        }

        Vec2 moveTowardsPoint(Vec2 runnerCenter, Vec2 targetPoint, double speed)
        {
            Vec2 delta = (targetPoint - runnerCenter).getNormalized();
            Vec2 newPoint = runnerCenter + delta * speed;
            bool xFlip = Math.Sign(newPoint.x - targetPoint.x) != Math.Sign(runnerCenter.x - targetPoint.x);
            bool yFlip = Math.Sign(newPoint.y - targetPoint.y) != Math.Sign(runnerCenter.y - targetPoint.y);
            if (xFlip) newPoint.x = targetPoint.x;
            if (yFlip) newPoint.y = targetPoint.y;
            return newPoint;
        }

        void moveRunnerUsingMarker(Marker m, Runner runner)
        {
            Vec2 delta = m.worldCenter - runner.center;
            double dist = delta.length();
            double speedCap = 0.0;
            if (dist < Constants.runMaxDistA) speedCap = Constants.runSpeed;
            else if (dist < Constants.runMaxDistB) speedCap = Util.linearMap(dist, Constants.runMaxDistA, Constants.runMaxDistB, Constants.runSpeed, 0.0);
            moveRunner(delta.getNormalized(), speedCap, runner);
        }

        void moveRunnerUsingJoystick(Vec2 pad, Runner runner)
        {
            double padMagnitude = pad.length();
            if (padMagnitude <= 0.01) return;

            double speedCap = Constants.runSpeed;

            if (runner.hasShoes)
                speedCap *= Constants.shoeSpeedMultiplier;

            moveRunner(pad.getNormalized(), speedCap, runner);
        }

        void moveRunner(Vec2 dir, double speedCap, Runner runner)
        {
            var remainingSpeed = 0.0;
            var targetPt = runner.center + dir * speedCap;
            if (speedCap > 0.0)
            {
                var acceptedSpeed = speedCap;
                var newCenter = moveTowardsPoint(runner.center, targetPt, speedCap);
                var closest = Util.closestStructure(state.curLevel.structures, newCenter, database.runnerBlockingStructures);
                if (closest.Item2 <= Constants.runnerRadius)
                {
                    var speedLowBound = 0.0;
                    var speedHighBound = speedCap;
                    //System.Console.WriteLine("I: [" + speedLowBound.ToString() + ", " + speedHighBound.ToString() + "]");
                    for (int i = 0; i < 4; i++)
                    {
                        var midpointSpeed = (speedLowBound + speedHighBound) * 0.5;
                        var midpointDist = Util.closestStructure(state.curLevel.structures, moveTowardsPoint(runner.center, targetPt, midpointSpeed), database.runnerBlockingStructures).Item2;
                        if (midpointDist <= Constants.runnerRadius)
                        {
                            speedHighBound = midpointSpeed;
                        }
                        else
                        {
                            speedLowBound = midpointSpeed;
                        }
                        //System.Console.WriteLine(i.ToString() + ": [" + speedLowBound.ToString() + ", " + speedHighBound.ToString() + "]");
                    }
                    acceptedSpeed = speedLowBound;
                    remainingSpeed = speedCap - acceptedSpeed;
                }
                runner.center = moveTowardsPoint(runner.center, targetPt, acceptedSpeed);
            }
            if (remainingSpeed > 0.0)
            {
                Vec2 targetPtX = new Vec2(targetPt.x, runner.center.y);
                Vec2 newCenterX = moveTowardsPoint(runner.center, targetPtX, remainingSpeed);
                var closestX = Util.closestStructure(state.curLevel.structures, newCenterX, database.runnerBlockingStructures);
                if (closestX.Item2 > Constants.runnerRadius)
                {
                    runner.center = newCenterX;
                    remainingSpeed = 0.0;
                }
            }
            if (remainingSpeed > 0.0)
            {
                Vec2 targetPtY = new Vec2(runner.center.x, targetPt.y);
                Vec2 newCenterY = moveTowardsPoint(runner.center, targetPtY, remainingSpeed);
                var closestY = Util.closestStructure(state.curLevel.structures, newCenterY, database.runnerBlockingStructures);
                if (closestY.Item2 > Constants.runnerRadius)
                {
                    runner.center = newCenterY;
                    remainingSpeed = 0.0;
                }
            }
        }

        void processRunnerJoystick(RunnerJoystickState joystick, Runner runner)
        {
            moveRunnerUsingJoystick(joystick.padA, runner);
            if (joystick.padB.lengthSq() > 0.4 * 0.4)
            {
                Vec2 newDir = joystick.padB.getNormalized();
                runner.laserDir = (runner.laserDir * 0.8 + newDir * 0.2).getNormalized();

            }

            if(runner.hasLaser && (joystick.buttonStates[GamepadButton.LB] || joystick.buttonStates[GamepadButton.RB]))
            {
                var structureLists = new List<List<Structure>> { state.curLevel.structures, state.curFrameTemporaryStructures };
                runner.laserPath = Util.traceLaser(structureLists, runner.laserOrigin(), runner.laserDir, database.runnerLaserBlockingStructures, -1, -1);
                state.curLevel.damageStructure(state, structureLists, runner.laserPath.finalObject.Item1, runner.laserPath.finalObject.Item2, Constants.laserGunDamage, false);
            }
        }

        void step()
        {
            state.frameCount++;
            joystick.poll();

            state.curFrameTemporaryStructures = state.nextFrameTemporaryStructures;
            if (state.activeRunners[0] != null)
                state.curFrameTemporaryStructures.Add(new Structure(StructureType.RunnerA, database, state.activeRunners[0].center));
            if (state.activeRunners[1] != null)
                state.curFrameTemporaryStructures.Add(new Structure(StructureType.RunnerB, database, state.activeRunners[1].center));

            state.nextFrameTemporaryStructures = new List<Structure>();

            foreach(Runner r in state.activeRunners)
            {
                if (r == null)
                    continue;
                r.laserPath = null;
            }

            foreach (Marker m in state.markers)
            {
                bool hasTool = state.curLevel.toolsAcquired[m.entry.type];
                if (!hasTool)
                {
                    m.available = false;
                    if ((DateTime.Now - state.lastInstruction).TotalSeconds > 3.0)
                    {
                        sound.playSpeech(m.entry.name + " protocol not available");
                        state.lastInstruction = DateTime.Now;
                    }
                    continue;
                }
                if(m.entry.type == ToolType.Mirror)
                {
                    Structure mirror = new Structure(StructureType.RunnerMirror, database, m.worldCenter);
                    mirror.curSweepAngle = Math.Atan2(m.orientation.y, m.orientation.x) * 180.0 / Math.PI;
                    state.curFrameTemporaryStructures.Add(mirror);
                }
                if(m.entry.type == ToolType.Medpack)
                {
                    foreach(Runner r in state.activeRunners)
                    {
                        if (r == null)
                            continue;
                        if(Vec2.distSq(r.center, m.screenCenter) < Constants.medPackRadius * Constants.medPackRadius)
                        {
                            r.curHealth = Math.Min(Constants.runnerMaxHealth, r.curHealth + 0.02);
                        }
                    }
                }
                if (m.entry.type == ToolType.Dyson)
                {
                    List<Miasma> newMiasma = new List<Miasma>();
                    foreach(Miasma miasma in state.curLevel.allMiasma)
                    { 
                        if (Vec2.distSq(miasma.center, m.screenCenter) < Constants.dysonRadius * Constants.dysonRadius)
                        {
                            miasma.radius -= Constants.dysonDamageRate;
                        }
                        if (miasma.radius > 1.0)
                            newMiasma.Add(miasma);
                    }
                    state.curLevel.allMiasma = newMiasma;
                }
                if (m.entry.type == ToolType.Kusanagi)
                {
                    foreach (Runner r in state.activeRunners)
                    {
                        if (r == null)
                            continue;
                        if (Vec2.distSq(r.center, m.screenCenter) < Constants.kusanagiRadius * Constants.kusanagiRadius)
                        {
                            var structureLists = new List<List<Structure>> { state.curLevel.structures };
                            r.laserPath = Util.traceLaser(structureLists, r.center, m.screenCenter - r.center, database.runnerLaserBlockingStructures, -1, -1);
                            state.curLevel.damageStructure(state, structureLists, r.laserPath.finalObject.Item1, r.laserPath.finalObject.Item2, Constants.kusanagiGunDamage, true);
                        }
                    }
                }
            }

            if (joystick.joysticks.Count >= 1 && state.activeRunners[0] != null)
            {
                processRunnerJoystick(joystick.joysticks[0], state.activeRunners[0]);
            }
            if (joystick.joysticks.Count >= 2 && state.activeRunners[1] != null)
            {
                processRunnerJoystick(joystick.joysticks[1], state.activeRunners[1]);
            }

            state.curLevel.updatePermanentStructures(this);
            state.curLevel.updateMiasma(this);

            if (state.curLevel.completed())
            {
                if(editor == null)
                {
                    state.advanceToNextLevel();
                }
            }
        }

        public void stepAndRender(int renderWidth, int renderHeight)
        {
            Bitmap webcamBitmap = vision.processWebcamImage(out state.markers, database, state.viewport.pMin);
            //image.Save("test.png");
            step();
            screen.render(webcamBitmap, state, editor, renderWidth, renderHeight);
        }
    }
}
