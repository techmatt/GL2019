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
            state = new GameState(missionName, levelName, database);
            if(editor != null)
                editor.level = state.levels[0];

            sound.playSpeech("mission start");
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

            moveRunner(pad.getNormalized(), Constants.runSpeed, runner);
        }

        void moveRunner(Vec2 dir, double speedCap, Runner runner)
        {
            var remainingSpeed = 0.0;
            var targetPt = runner.center + dir * speedCap;
            if (speedCap > 0.0)
            {
                var acceptedSpeed = speedCap;
                var newCenter = moveTowardsPoint(runner.center, targetPt, speedCap);
                var closest = Util.closestStructure(state.activeLevel.structures, newCenter, database.runnerBlockingStructures);
                if (closest.Item2 <= Constants.runnerRadius)
                {
                    var speedLowBound = 0.0;
                    var speedHighBound = speedCap;
                    //System.Console.WriteLine("I: [" + speedLowBound.ToString() + ", " + speedHighBound.ToString() + "]");
                    for (int i = 0; i < 4; i++)
                    {
                        var midpointSpeed = (speedLowBound + speedHighBound) * 0.5;
                        var midpointDist = Util.closestStructure(state.activeLevel.structures, moveTowardsPoint(runner.center, targetPt, midpointSpeed), database.runnerBlockingStructures).Item2;
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
                var closestX = Util.closestStructure(state.activeLevel.structures, newCenterX, database.runnerBlockingStructures);
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
                var closestY = Util.closestStructure(state.activeLevel.structures, newCenterY, database.runnerBlockingStructures);
                if (closestY.Item2 > Constants.runnerRadius)
                {
                    runner.center = newCenterY;
                    remainingSpeed = 0.0;
                }
            }
        }

        void step()
        {
            joystick.poll();

            double deltaX = 0.0;
            state.updateViewport(deltaX);

            state.curFrameTemporaryStructures = state.nextFrameTemporaryStructures;
            if (state.activeRunnerA != null)
                state.curFrameTemporaryStructures.Add(new Structure(StructureType.RunnerA, database, state.activeRunnerA.center));
            if (state.activeRunnerB != null)
                state.curFrameTemporaryStructures.Add(new Structure(StructureType.RunnerB, database, state.activeRunnerB.center));

            state.nextFrameTemporaryStructures = new List<Structure>();

            foreach(GameLevel level in state.visibleLevels)
            {
                level.updatePermanentStructures(this);
            }

            foreach (Marker m in state.markers)
            {
                if (m.entry.type == ToolType.RunA && state.activeRunnerA != null)
                {
                    moveRunnerUsingMarker(m, state.activeRunnerA);
                }
                if (m.entry.type == ToolType.RunB && state.activeRunnerB != null)
                {
                    moveRunnerUsingMarker(m, state.activeRunnerB);
                }
            }

            if (joystick.joysticks.Count >= 1 && state.activeRunnerA != null)
            {
                moveRunnerUsingJoystick(joystick.joysticks[0].padA, state.activeRunnerA);
                state.activeRunnerA.laserDir = joystick.joysticks[0].padB;
            }
            if (joystick.joysticks.Count >= 2 && state.activeRunnerB != null)
            {
                moveRunnerUsingJoystick(joystick.joysticks[1].padA, state.activeRunnerB);
                state.activeRunnerB.laserDir = joystick.joysticks[1].padB;
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
