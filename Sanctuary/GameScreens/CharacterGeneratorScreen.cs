using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using XRpgLibrary;
using XRpgLibrary.Controls;
using XRpgLibrary.SpriteClasses;
using XRpgLibrary.TileEngine;
using XRpgLibrary.WorldClasses;

using XRpgLibrary.ItemClasses;
using XRpgLibrary.CharacterClasses;

using Sanctuary.Components;
using XRpgLibrary.ConversationComponents;
using RpgLibrary.CharacterClasses;
using RpgLibrary.ItemClasses;
using RpgLibrary.SkillClasses;

namespace Sanctuary.GameScreens
{
    public class CharacterGeneratorScreen : BaseGameState
    {
        #region Field Region

        LeftRightSelector genderSelector;
        LeftRightSelector classSelector;
        PictureBox backgroundImage;

        PictureBox characterImage;
        Texture2D[,] characterImages;
        Texture2D containers;

        string[] genderItems = { "Male", "Female" };
        string[] classItems;

        #endregion

        #region Property Region

        public string SelectedGender
        {
            get { return genderSelector.SelectedItem; }
        }

        public string SelectedClass
        {
            get { return classSelector.SelectedItem; }
        }

        #endregion

        #region Constructor Region

        public CharacterGeneratorScreen(Game game, GameStateManager stateManager)
            : base(game, stateManager)
        {
        }

        #endregion

        #region XNA Method Region

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            classItems = new string[DataManager.EntityData.Count];

            int counter = 0;

            foreach (string className in DataManager.EntityData.Keys)
            {
                classItems[counter] = className;
                counter++;
            }

            LoadImages();
            CreateControls();
            containers = Game.Content.Load<Texture2D>(@"ObjectSprites\containers");
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            base.Draw(gameTime);

            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        #endregion

        #region Method Region

        private void CreateControls()
        {
            Texture2D leftTexture = Game.Content.Load<Texture2D>(@"GUI\leftarrowUp");
            Texture2D rightTexture = Game.Content.Load<Texture2D>(@"GUI\rightarrowUp");
            Texture2D stopTexture = Game.Content.Load<Texture2D>(@"GUI\StopBar");

            backgroundImage = new PictureBox(
                Game.Content.Load<Texture2D>(@"Backgrounds\titlescreen"),
                GameRef.ScreenRectangle);
            ControlManager.Add(backgroundImage);

            Label label1 = new Label();

            label1.Text = "Who will search for the Eyes of the Dragon?";
            label1.Size = label1.SpriteFont.MeasureString(label1.Text);
            label1.Position = new Vector2((GameRef.Window.ClientBounds.Width - label1.Size.X) / 2, 150);

            ControlManager.Add(label1);

            genderSelector = new LeftRightSelector(leftTexture, rightTexture, stopTexture);
            genderSelector.SetItems(genderItems, 125);
            genderSelector.Position = new Vector2(label1.Position.X, 200);
            genderSelector.SelectionChanged += new EventHandler(selectionChanged);

            ControlManager.Add(genderSelector);

            classSelector = new LeftRightSelector(leftTexture, rightTexture, stopTexture);
            classSelector.SetItems(classItems, 125);
            classSelector.Position = new Vector2(label1.Position.X, 250);
            classSelector.SelectionChanged += selectionChanged;

            ControlManager.Add(classSelector);

            LinkLabel linkLabel1 = new LinkLabel();
            linkLabel1.Text = "Accept this character.";
            linkLabel1.Position = new Vector2(label1.Position.X, 300);
            linkLabel1.Selected += new EventHandler(linkLabel1_Selected);

            ControlManager.Add(linkLabel1);

            characterImage = new PictureBox(
                characterImages[0, 0], 
                new Rectangle(500, 200, 96, 96), 
                new Rectangle(0, 0, 32, 32));
            ControlManager.Add(characterImage);

            ControlManager.NextControl();
        }

        private void LoadImages()
        {
            characterImages = new Texture2D[genderItems.Length, classItems.Length];

            for (int i = 0; i < genderItems.Length; i++)
            {
                for (int j = 0; j < classItems.Length; j++)
                {
                    characterImages[i, j] = Game.Content.Load<Texture2D>(@"PlayerSprites\" + genderItems[i] + classItems[j]);
                }
            }
        }

        void linkLabel1_Selected(object sender, EventArgs e)
        {
            InputHandler.Flush();

            CreatePlayer();
            CreateWorld();

            GameRef.SkillScreen.SkillPoints = 10;
            Transition(ChangeType.Change, GameRef.SkillScreen);
            GameRef.SkillScreen.SetTarget(GamePlayScreen.Player.Character);
        }

        private void CreatePlayer()
        {
            AnimatedSprite sprite = new AnimatedSprite(
                characterImages[genderSelector.SelectedIndex, classSelector.SelectedIndex],
                AnimationManager.Instance.Animations);

            EntityGender gender = EntityGender.Male;

            if (genderSelector.SelectedIndex == 1)
                gender = EntityGender.Female;

            Entity entity = new Entity(
                "Pat",
                DataManager.EntityData[classSelector.SelectedItem],
                gender,
                EntityType.Character);

            foreach (string s in DataManager.SkillData.Keys)
            {
                Skill skill = Skill.FromSkillData(DataManager.SkillData[s]);
                entity.Skills.Add(s, skill);
            }

            Character character = new Character(entity, sprite);

            GamePlayScreen.Player = new Player(GameRef, character);
        }

        private void CreateWorld()
        {
            Texture2D tilesetTexture = Game.Content.Load<Texture2D>(@"Tilesets\tileset1");
            Tileset tileset1 = new Tileset(tilesetTexture, 8, 8, 32, 32);

            tilesetTexture = Game.Content.Load<Texture2D>(@"Tilesets\tileset2");
            Tileset tileset2 = new Tileset(tilesetTexture, 8, 8, 32, 32);

            tilesetTexture = Game.Content.Load<Texture2D>(@"Tilesets\fire-tiles");
            AnimatedTileset animatedSet = new AnimatedTileset(tilesetTexture, 8, 1, 64, 64);

            MapLayer layer = new MapLayer(100, 100);

            for (int y = 0; y < layer.Height; y++)
            {
                for (int x = 0; x < layer.Width; x++)
                {
                    Tile tile = new Tile(0, 0);

                    layer.SetTile(x, y, tile);
                }
            }

            MapLayer splatter = new MapLayer(100, 100);

            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(0, 100);
                int y = random.Next(0, 100);
                int index = random.Next(2, 14);

                Tile tile = new Tile(index, 0);
                splatter.SetTile(x, y, tile);
            }

            splatter.SetTile(5, 0, new Tile(14, 0));
            splatter.SetTile(1, 0, new Tile(0, 1));
            splatter.SetTile(2, 0, new Tile(2, 1));
            splatter.SetTile(3, 0, new Tile(0, 1));

            TileMap map = new TileMap(tileset1, animatedSet, layer);
            map.AddTileset(tileset2);
            map.AddLayer(splatter);

            map.CollisionLayer.Collisions.Add(new Point(1, 0), CollisionType.Impassable);
            map.CollisionLayer.Collisions.Add(new Point(3, 0), CollisionType.Impassable);

            map.AnimatedTileLayer.AnimatedTiles.Add(new Point(5, 0), new AnimatedTile(0, 8));

            Level level = new Level(map);

            ChestData chestData = Game.Content.Load<ChestData>(@"Game\Chests\Plain Chest");

            Chest chest = new Chest(chestData);

            BaseSprite chestSprite = new BaseSprite(
                containers, 
                new Rectangle(0, 0, 32, 32), 
                new Point(10, 10));

            ItemSprite itemSprite = new ItemSprite(
                chest, 
                chestSprite);
            level.Chests.Add(itemSprite);

            World world = new World(GameRef, GameRef.ScreenRectangle);
            world.Levels.Add(level);
            world.CurrentLevel = 0;

            AnimatedSprite s = new AnimatedSprite(
                GameRef.Content.Load<Texture2D>(@"SpriteSheets\Eliza"), 
                AnimationManager.Instance.Animations);

            s.Position = new Vector2(5 * Engine.TileWidth, 5 * Engine.TileHeight);

            EntityData ed = new EntityData("Eliza", 10, 10, 10, 10, 10, 10,  "20|CON|12", "16|WIL|16", "0|0|0");
            Entity e = new Entity("Eliza", ed, EntityGender.Female, EntityType.NPC);

            NonPlayerCharacter npc = new NonPlayerCharacter(e, s);
            npc.SetConversation("eliza1");
            
            world.Levels[world.CurrentLevel].Characters.Add(npc);
            GamePlayScreen.World = world;

            CreateConversation();

            ((NonPlayerCharacter)world.Levels[world.CurrentLevel].Characters[0]).SetConversation("eliza1");
        }

        private void CreateConversation()
        {
            Conversation c = new Conversation("eliza1", "welcome");

            GameScene scene = new GameScene(
                GameRef, 
                "basic_scene", 
                "The unthinkable has happened. A thief has stolen the eyes of the village guardian." + 
                " With out his eyes the dragon will not animated if the village is attacked.", 
                new List<SceneOption>());

            SceneAction action = new SceneAction();
            action.Action = ActionType.Talk;
            action.Parameter = "none";

            SceneOption option = new SceneOption("Continue", "welcome2", action);
            scene.Options.Add(option);

            c.AddScene("welcome", scene);

            scene = new GameScene(
                GameRef, 
                "basic_scene", 
                "Will you retrieve the eyes of the dragon for us?",
                new List<SceneOption>());

            action = new SceneAction();
            action.Action = ActionType.Change;
            action.Parameter = "none";

            option = new SceneOption("Yes", "eliza2", action);
            scene.Options.Add(option);

            action = new SceneAction();
            action.Action = ActionType.Talk;
            action.Parameter = "none";

            option = new SceneOption("No", "pleasehelp", action);
            scene.Options.Add(option);

            c.AddScene("welcome2", scene);

            scene = new GameScene(
                GameRef,
                "basic_scene",
                "Please, you are the only one that can help us. If you change your mind " +
                "come back and see me.",
                new List<SceneOption>());

            action = new SceneAction();
            action.Action = ActionType.End;
            action.Parameter = "none";

            option = new SceneOption("Bye", "welcome2", action);
            scene.Options.Add(option);

            c.AddScene("pleasehelp", scene);

            ConversationManager.Instance.AddConversation("eliza1", c);

            c = new Conversation("eliza2", "thankyou");

            scene = new GameScene(
                GameRef,
                "basic_scene",
                "Thank you for agreeing to help us! Please find Faulke in the inn and ask " +
                "him what he knows about this theif.",
                new List<SceneOption>());

            action = new SceneAction();
            action.Action = ActionType.Quest;
            action.Parameter = "Faulke";

            option = new SceneOption("Continue", "thankyou2", action);
            scene.Options.Add(option);

            c.AddScene("thankyou", scene);

            scene = new GameScene(
                GameRef,
                "basic_scene",
                "Return to me once you've spoken with Faulke.",
                new List<SceneOption>());

            action = new SceneAction();
            action.Action = ActionType.End;
            action.Parameter = "none";

            option = new SceneOption("Good Bye", "thankyou2", action);
            scene.Options.Add(option);

            c.AddScene("thankyou2", scene);

            ConversationManager.Instance.AddConversation("eliza2", c);
        }

        void selectionChanged(object sender, EventArgs e)
        {
            characterImage.Image = characterImages[genderSelector.SelectedIndex, classSelector.SelectedIndex];
        }

        #endregion
    }
}
