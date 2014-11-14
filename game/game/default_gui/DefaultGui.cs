using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Frontend
{
    /// <summary>
    /// This is a very, very basic GUI. It communicates with any IBackend, takes the map and existing entities and renders them in a generic fashion.
    /// The space will be devided evenly among the ITiles of a map. That means: bigger maps will result in very tiny cells on the GUI.
    /// No scrolling or any fancy mechanisms.
    /// Entities will also be displayed in a generic way: players are rendered in yellow, dragons are red. No IDs, names or whatsoever.
    /// It also offers basic functionality to communicate with the backend to input commands via the chat. So you can do some testing by manually entering commands like "/get:map".
    /// The GUI also consists of a chat-window, featuring incoming chat-messages and a input-box, to send messages to the IBackend (which have to be handled there!).
    /// Pressing ENTER sends the text in the input-box and also switches between chat-mode and walk-mode. In walk-mode, pressing WASD causes the frontend to send move-commands to the IBackend.
    /// As you can see, this is just rudimentary functionality and rendering and you might wish (and are invited) to extend the class, modify the existing sourcecode or even write your very own implementation to have a more appealing GUI that fits your needs.
    /// 
    /// To get this to work, you will have to let your backend implemented the provided IBackend-interface (and of course the other provided interfaces where needed). Then 
    /// </summary>
    public partial class DefaultGui : Form
    {
        private IBackend backend;
        public DefaultGui(IBackend backend) : base()
        {
            if (backend == null)
            {
                throw new ArgumentNullException("invalid value for 'backend': null");
            }
            this.backend = backend;
            InitializeComponent();
            // we usually don't have a console in a form-project. This enables us to see debug-output anyway
            AllocConsole();
            this.board.Paint += board_PaintMap;
            this.board.Paint += board_PaintEntities;
            this.chatInput.KeyPress += chat_KeyPress;
            this.board.KeyPress += board_KeyPress;
        }

        /// <summary>
        /// Handles keypresses the chatInput-field receives.
        /// Having ENTER pressed, causes the field to be emptied and have the trimmed text in the field sent to the backend.
        /// If the input starts with "/", it will be handled as command (like ask:mv:up and such).
        /// All other messages will be treated as chat-messages and have to be wrapped by the backend as such.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chat_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter) 
            {
                    string input = this.chatInput.Text.Trim();
                    this.chatInput.Text = "";
                    // ignore empty input
                    if (input != "")
                    {
                        if (input.StartsWith("/"))
                        {
                            input = input.Substring(1, input.Length-1);
                            this.backend.sendCommand(input);
                        }
                        else
                        {
                            this.backend.sendChat(input);
                        }
                    }
                    this.board.Focus();
            }
            
        }

        /// <summary>
        /// Handles keypresses when the board is focused. This is whenever the chatinput is NOT focused.
        /// Will handle WASD as triggers for the movement.
        /// A: left
        /// W: up
        /// S: down
        /// D: right
        /// Pressing ENTER causes the chatinput to be focused again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void board_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // fall-through-cases for capital letters
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    this.chatInput.Focus();
                    break;
                case 'a':
                case 'A':
                    this.backend.sendCommand("ask:mv:lft");
                    break;
                case 'd':
                case 'D':
                    this.backend.sendCommand("ask:mv:rgt");
                    break;
                case 'w':
                case 'W':
                    this.backend.sendCommand("ask:mv:up");
                    break;
                case 's':
                case 'S':
                    this.backend.sendCommand("ask:mv:dwn");
                    break;
            }
        }

        /// <summary>
        /// Handler to paint the tiles in the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void board_PaintMap(object sender, System.Windows.Forms.PaintEventArgs e) {
            Size tileSize = this.getTileSize();
            ITile[][] cells = this.backend.getMap();
            // validity checked beforehand in getTileSize
            Debug.Assert(cells != null);
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(this.board.CreateGraphics(), this.board.DisplayRectangle);
            Graphics g = this.CreateGraphics();
            for(int x = 0; x < cells.Length; x++) {
                for(int y = 0; y < cells[x].Length; y++) {
                    this.drawMapTile(buffer.Graphics, cells[x][y], x * tileSize.Width, y * tileSize.Height, tileSize.Width, tileSize.Height);
                }
            }
            buffer.Render();
        }

        /// <summary>
        /// Handler to paint all entities Dragons first, then the Players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void board_PaintEntities(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            List<IPositionable> dragons = this.backend.getDragons();
            foreach (IPositionable dragon in dragons)
            {
                this.drawDragon(e.Graphics, dragon);
            }
            List<IPositionable> players = this.backend.getPlayers();
            foreach (IPositionable player in players)
            {
                this.drawPlayer(e.Graphics, player);
            }
        }

        /// <summary>
        /// Draws a tile of the map on a graphics object.
        /// By default, it will draw a rectangle (colour will be dependent of the attributes of the tile).
        /// WATER: blue
        /// FOREST: dark green
        /// HUNTABLE: light green
        /// UNWALKABLE: grey
        /// else: peach
        /// It will have a black 1px border.
        /// </summary>
        /// <param name="g">the graphics-object to draw on</param>
        /// <param name="tile">the tile-object that implements ITile</param>
        /// <param name="absX">absolute pixel x-position of the upper left corner of the tile, relative to the upper left corner of the graphics-object</param>
        /// <param name="absY">absolute pixel y-position of the upper left corner of the tile, relative to the upper left corner of the graphics-object</param>
        /// <param name="width">width of the tile in pixels</param>
        /// <param name="height">height of the tile in pixels</param>
        protected void drawMapTile(Graphics g, ITile tile, int absX, int absY, int width, int height)
        {
            Color colour = Color.BurlyWood;
            if (tile.isForest())
            {
                if (tile.isHuntable())
                {
                    colour = Color.YellowGreen;
                }
                else
                {
                    colour = Color.Green;
                }
            }
            else if (tile.isWater())
            {
                colour = Color.Blue;
            }
            else if (!tile.isWalkable())
            {
                colour = Color.DimGray;
            }
            g.FillRectangle(new SolidBrush(colour), absX, absY, width, height);
            g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(absX, absY, width, height));
        }

        /// <summary>
        /// Draws a player on the graphics.
        /// By default, players will be represented by a centered dark-yellow rectangle that takes up half of the cells size.
        /// </summary>
        /// <param name="g">the graphics-object to draw on</param>
        /// <param name="player">the player to draw</param>
        protected void drawPlayer(Graphics g, IPositionable player)
        {
            Size tileSize = this.getTileSize();
            g.FillRectangle(new SolidBrush(Color.DarkGoldenrod),
                player.getXPosition() * tileSize.Width + tileSize.Width / 2 - tileSize.Width / 4,
                player.getYPosition() * tileSize.Height + tileSize.Height / 2 - tileSize.Height / 4, 
                tileSize.Width/2, 
                tileSize.Height/2);
        }

        /// <summary>
        /// Draws a dragon on the graphics.
        /// By default, dragons will be represented by a centered red rectangle that takes up half of the cells size.
        /// </summary>
        /// <param name="g">the graphics-object to draw on</param>
        /// <param name="dragon">the dragon to draw</param>
        protected void drawDragon(Graphics g, IPositionable dragon)
        {
            Size tileSize = this.getTileSize();
            g.FillRectangle(new SolidBrush(Color.DarkRed),
                dragon.getXPosition() * tileSize.Width + tileSize.Width / 2 - tileSize.Width / 4,
                dragon.getYPosition() * tileSize.Height + tileSize.Height / 2 - tileSize.Height / 4,
                tileSize.Width / 2,
                tileSize.Height / 2);
        }

        /// <summary>
        /// Devides the space of the board-panel equally among the tiles of the map.
        /// For intance, if the board is 100px wide and the map consists of 5 cells in y-direction
        /// each tile will have a width of 100px/5 = 20px. Same for the height.
        /// </summary>
        /// <returns>the size of one tile to fit the whole map on the board</returns>
        protected Size getTileSize()
        {
            IPositionable[][] cells = this.backend.getMap();
            if (cells == null)
            {
                throw new ArgumentNullException("backend returned null as map");
            }
            if (cells.Length == 0)
            {
                throw new IndexOutOfRangeException("outer dimension of the retrieved map has length 0");
            }
            if (cells[0].Length == 0)
            {
                throw new IndexOutOfRangeException("inner dimension of the retrieved map has length 0");
            }
            int cellWidth = this.board.Size.Width / cells.Length;
            int cellHeight = this.board.Size.Height / cells[0].Length;
            return new Size(cellWidth, cellHeight);
        }

        /// <summary>
        /// Appends a chat-message to the chat-window as a new line. Can be called from the backend or other participants to display incoming chat-messages.
        /// Messages will always be displays in the fasion of:
        /// sender: message
        /// </summary>
        /// <param name="sender">the source of the message</param>
        /// <param name="message">the message itself</param>
        public void appendChatMessage(string sender, string message)
        {
            this.chatWindow.AppendText(sender + ": " + message + "\r\n");
        }



        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

    }
}
