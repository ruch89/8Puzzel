using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _8Puzzel
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D boxTexture;
        Box[,] boxes;
        SpriteFont boxfont;

        List<Node> output;
        Node current;
        int count;
        bool transit = false;
        private static readonly TimeSpan intervalBetweenAttack1 = TimeSpan.FromMilliseconds(2000);
        private TimeSpan lastTimeAttack;

        Box sloveButton;
        Texture2D buttonText;
        bool play;
        int[] currentNums;

        Box newButton;
        Texture2D newbuttonText;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        private void StartGame()
        {
            boxes = new Box[3, 3];
            currentNums = new int[9] { 8, 1, 3, 7, 2, 4, 0, 6, 5 };
            //currentNums = new int[9] { 1, 3, 4, 8, 6, 2, 7, 0, 5 };
            //currentNums = new int[9] { 2, 8, 1, 0, 4, 3, 7, 6, 5 };
            int fullWidth = GraphicsDevice.Viewport.Width -240;
            int fullHeight = GraphicsDevice.Viewport.Height - 80;
            sloveButton = new Box(buttonText,new Rectangle(fullWidth + 45,fullHeight-50,150,50),Color.YellowGreen);
            sloveButton.setFontColor(Color.Transparent);

            newButton = new Box(newbuttonText, new Rectangle(fullWidth + 45, fullHeight - 130, 150, 50), Color.PowderBlue);
            newButton.setFontColor(Color.Transparent);

            int wMultiplier = (int)Math.Floor(fullWidth / 3.0);
            int hMultiplier = (int)Math.Floor(fullHeight / 3.0);

            int count = 1;
            for (int y = 0; y < 3; y++)
            {
                Color tint = Color.White;
                int yy = y % 3;
                switch (yy)
                {
                    case 0:
                        tint = Color.Blue;
                        break;
                    case 1:
                        tint = Color.Red;
                        break;
                    case 2:
                        tint = Color.Yellow;
                        break;
                }

                for (int x = 0; x < 3; x++)
                {
                    boxes[y, x] = new Box(boxTexture, new Rectangle(x * wMultiplier + 9, y * hMultiplier + 9, wMultiplier, hMultiplier), tint);
                    boxes[y, x].number = count++;
                }
            }
            completed = false;
        }


        protected override void Initialize()
        {
            base.Initialize();
            StartGame();
            //Node start = new Node();
            //Node goal = new Node();
            

            //start.depth = 0;
            //goal.depth = 0;

            //start.parent = null;

            //start.tiles = new int[9] { 8, 1, 3, 7, 2, 4, 0, 6, 5 };
            //goal.tiles = new int[9] { 1, 2, 3, 8, 0, 4, 7, 6, 5 };

            //start.f = start.depth + Huristic.ManhattanDistance(start.tiles, goal.tiles);

            //AStarSolver star = new AStarSolver(start, goal);
            //output = star.solvePuzzel();
            //count = output.Count - 1;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            boxTexture = Content.Load<Texture2D>("box");
            boxfont = Content.Load<SpriteFont>("boxfont");
            buttonText = Content.Load<Texture2D>("solve");
            newbuttonText = Content.Load<Texture2D>("newgame");
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (DrawRect.IsInRect(Mouse.GetState().X, Mouse.GetState().Y, newButton.Location))
                {
                    if (!play)
                    {
                        //solvePuzzelHere(new int[9] { 8, 1, 3, 7, 2, 4, 0, 6, 5 });
                        StartGame();
                        lastTimeAttack = gameTime.TotalGameTime;
                    }
                }
            }
            int nummy = 0;
            foreach (Box item in boxes)
            {
                nummy++;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (DrawRect.IsInRect(Mouse.GetState().X, Mouse.GetState().Y, item.Location))
                    {
                        //////////////////////////////////////
                        sortTileOnClick(nummy-1);
                        setNumbers(boxes, currentNums);
                        //bug is here
                        //////////////////////////////////////
                    }
                } 
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (DrawRect.IsInRect(Mouse.GetState().X,Mouse.GetState().Y,sloveButton.Location))
                {
                    if (!play)
                    {
                        //solvePuzzelHere(new int[9] { 8, 1, 3, 7, 2, 4, 0, 6, 5 });
                        solvePuzzelHere(currentNums);
                        completed = false;
                        lastTimeAttack = gameTime.TotalGameTime;
                    } 
                }
            }

            if (play)
            {
                current = output.ElementAt<Node>(count);
                setNumbers(boxes, current.tiles);
                if (lastTimeAttack + intervalBetweenAttack1 < gameTime.TotalGameTime)
                {
                    count--;
                    if (count < 0)
                    {
                        count = 0;
                        play = false;
                        completed = true;
                    }
                    current = output.ElementAt<Node>(count);
                    lastTimeAttack = gameTime.TotalGameTime;
                }
            }
            else if(completed) 
            {
                setNumbers(boxes, new int[9] { 1, 2, 3, 8, 0, 4, 7, 6, 5 });
            }
            else 
            {
                setNumbers(boxes, currentNums );
            
            }

            base.Update(gameTime);
        }

        private void sortTileOnClick(int num)
        {
            if (num == 0)
            {
                if (currentNums[1]==0)
                {
                    currentNums[1] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[3] == 0)
                {
                    currentNums[3] = currentNums[num];
                    currentNums[num] = 0;
                }

            }
            else if (num == 1)
            {
                if (currentNums[0] == 0)
                {
                    currentNums[0] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[2] == 0)
                {
                    currentNums[2] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[4] == 0)
                {
                    currentNums[4] = currentNums[num];
                    currentNums[num] = 0;
                }

            }
            else if (num == 2)
            {
                if (currentNums[1] == 0)
                {
                    currentNums[1] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[5] == 0)
                {
                    currentNums[5] = currentNums[num];
                    currentNums[num] = 0;
                }

            }
            else if (num == 3)
            {
                if (currentNums[0] == 0)
                {
                    currentNums[0] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[4] == 0)
                {
                    currentNums[4] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[6] == 0)
                {
                    currentNums[6] = currentNums[num];
                    currentNums[num] = 0;
                }

            }
            else if (num == 4)
            {
                if (currentNums[1] == 0)
                {
                    currentNums[1] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[3] == 0)
                {
                    currentNums[3] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[5] == 0)
                {
                    currentNums[5] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[7] == 0)
                {
                    currentNums[7] = currentNums[num];
                    currentNums[num] = 0;
                }

            }
            else if (num == 5)
            {
                if (currentNums[2] == 0)
                {
                    currentNums[2] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[4] == 0)
                {
                    currentNums[4] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[8] == 0)
                {
                    currentNums[8] = currentNums[num];
                    currentNums[num] = 0;
                }

            }
            else if (num == 6)
            {
                if (currentNums[3] == 0)
                {
                    currentNums[3] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[7] == 0)
                {
                    currentNums[7] = currentNums[num];
                    currentNums[num] = 0;
                }

            }
            else if (num == 7)
            {
                if (currentNums[4] == 0)
                {
                    currentNums[4] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[6] == 0)
                {
                    currentNums[6] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[8] == 0)
                {
                    currentNums[8] = currentNums[num];
                    currentNums[num] = 0;
                }

            }
            else if (num == 8)
            {
                if (currentNums[5] == 0)
                {
                    currentNums[5] = currentNums[num];
                    currentNums[num] = 0;
                }
                else if (currentNums[7] == 0)
                {
                    currentNums[7] = currentNums[num];
                    currentNums[num] = 0;
                }

            }
        
        }

        private void setNumbers(Box[,] boxs, int[] txts)
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    boxs[i,j].number = txts[count++];
                }
                
            }
        }

        private void solvePuzzelHere(int[] inputs)
        {
            Node start = new Node();
            Node goal = new Node();


            start.depth = 0;
            goal.depth = 0;

            start.parent = null;

            //start.tiles = new int[9] { 8, 1, 3, 7, 2, 4, 0, 6, 5 };
            start.tiles = inputs;
            goal.tiles = new int[9] { 1, 2, 3, 8, 0, 4, 7, 6, 5 };

            start.f = start.depth + Huristic.ManhattanDistance(start.tiles, goal.tiles);
            //start.f = start.depth + Huristic.DistanceN(start.tiles, goal.tiles);
            AStarSolver star = new AStarSolver(start, goal);
            output = star.solvePuzzel();
            count = output.Count - 1;
            this.play = true;
            
        
        }

        private Box getBoxWithZero(Box[,] boxs)
        {
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (boxs[i, j].number == 0)
                        return boxes[i, j];
                }

            }
            return null;
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (Box item in boxes)
            {
                if (item.number != 0)
                {
                    item.Draw(spriteBatch, boxfont); 
                }
            }

            spriteBatch.DrawString(boxfont, "8Puzzel Solver", new Vector2(580, 20), Color.Azure);
            spriteBatch.DrawString(boxfont, "Algorithm:", new Vector2(580, 60), Color.SkyBlue);
            spriteBatch.DrawString(boxfont, "A Star (A*) ", new Vector2(580, 90), Color.Azure);
            spriteBatch.DrawString(boxfont, "Huristic Function:", new Vector2(580, 130), Color.SkyBlue);
            spriteBatch.DrawString(boxfont, "Manhattan Distance", new Vector2(580, 160), Color.Azure);

            if (completed)
            {
                spriteBatch.DrawString(boxfont, "Just Solved!!!", new Vector2(580, 230), Color.Yellow);
                newButton.Draw(spriteBatch, boxfont);
            }
            sloveButton.Draw(spriteBatch, boxfont);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public bool completed { get; set; }
    }
}
