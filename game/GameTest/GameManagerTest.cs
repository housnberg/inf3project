using game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest
{
    [TestClass]
    public class GameManagerTest
    {
        private String ip = "127.0.0.1";
        private UInt16 port = 1024;

        [TestMethod]
        public void sendCommandTest()
        {
            GameManager gm = new GameManager(ip, port);
            Assert.IsNotNull(gm);
        }


        [TestMethod]
        public void addPlayerTest()
        {
            GameManager gm = new GameManager(ip, port);
            gm.storePlayer(new Player("Hans",123));
            Assert.IsTrue(gm.getNumberOfPlayers() > 0);
        }

        [TestMethod]
        public void deletePlayerTest()
        {
            GameManager gm = new GameManager(ip, port);
            Token tokenOne = new Player("Hans", 123);
            gm.storePlayer(tokenOne);
            Token tokenTwo = new Player("Juergen", 124);
            gm.storePlayer(tokenTwo);
            gm.deletePlayer(tokenOne);
            Assert.IsTrue(gm.getNumberOfPlayers() == 1);
        }

        [TestMethod]
        public void deletePlayerByID()
        {
            GameManager gm = new GameManager(ip, port);
            Player playerOne = new Player("Hans", 123);
            gm.storePlayer(playerOne);
            Player playerTwo = new Player("Juergen", 124);
            gm.storePlayer(playerTwo);
            gm.deletePlayer(123);
            Assert.IsTrue(gm.getNumberOfPlayers() == 1);
        }

    }
}
