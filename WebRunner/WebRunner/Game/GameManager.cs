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

        public void startMission(string missionName, string levelName)
        {
            state = new GameState(missionName, levelName, database);
            if(editor != null)
                editor.level = state.levels[0];
        }

        void moveRunner(Marker m, Runner runner)
        {
            Vec2 delta = m.center - runner.center;
            double dist = delta.length();
            double speed = 0.0;
            if (dist < Constants.runMaxDistA) speed = Constants.runSpeed;
            else if (dist < Constants.runMaxDistB) speed = Util.linearMap(dist, Constants.runMaxDistA, Constants.runMaxDistB, Constants.runSpeed, 0.0);

            if (speed > 0.0)
            {
                delta = delta.getNormalized();
                var acceptedSpeed = speed;
                var closest = Util.closestStructure(state.activeLevel.structures, runner.center + delta * speed, database.runnerBlockingStructures);
                if (closest.Item2 <= Constants.runnerRadius)
                {
                    var speedLowBound = 0.0;
                    var speedHighBound = speed;
                    System.Console.WriteLine("I: [" + speedLowBound.ToString() + ", " + speedHighBound.ToString() + "]");
                    for (int i = 0; i < 4; i++)
                    {
                        var midpointSpeed = (speedLowBound + speedHighBound) * 0.5;
                        var midpointDist = Util.closestStructure(state.activeLevel.structures, runner.center + delta * midpointSpeed, database.runnerBlockingStructures).Item2;
                        if (midpointDist <= Constants.runnerRadius)
                        {
                            speedHighBound = midpointSpeed;
                        }
                        else
                        {
                            speedLowBound = midpointSpeed;
                        }
                        System.Console.WriteLine(i.ToString() + ": [" + speedLowBound.ToString() + ", " + speedHighBound.ToString() + "]");
                    }
                    acceptedSpeed = speedLowBound;
                }
                runner.center = runner.center + delta * acceptedSpeed;
            }
        }

        void step()
        {
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
                level.updatePermanentStructures(database, state);
            }

            foreach (Marker m in state.markers)
            {
                if (m.entry.type == ToolType.RunA && state.activeRunnerA != null)
                {
                    moveRunner(m, state.activeRunnerA);
                }
                if (m.entry.type == ToolType.RunB && state.activeRunnerB != null)
                {
                    moveRunner(m, state.activeRunnerB);
                }
            }
        }

        public void stepAndRender(int renderWidth, int renderHeight)
        {
            Bitmap webcamBitmap = vision.processWebcamImage(out state.markers, database);
            //image.Save("test.png");
            step();
            screen.render(webcamBitmap, state, editor, renderWidth, renderHeight);
        }
    }
}
