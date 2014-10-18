using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using game.client;

namespace GameTest
{
    [TestClass]
    public class ConnectorTest
    {
        [TestMethod]
        public void connectTest()
        {
            Boolean expected = true;
            Connector connection = new Connector("127.0.0.1", 1024);
            Assert.AreEqual(expected, connection.getTcpClient().Connected);
        }

        [TestMethod]
        public void disconnectTest()
        {
            Boolean expected = false;
            Connector connection = new Connector("127.0.0.1", 1024);
            connection.disconnect();
            Assert.AreEqual(expected, connection.getTcpClient().Connected);
        }

        [TestMethod]
        public void sendServerMessageTest()
        {
            Connector connection = new Connector("127.0.0.1", 1024);
            connection.sendServerMessage("HalloTest");
        }

    }
}
