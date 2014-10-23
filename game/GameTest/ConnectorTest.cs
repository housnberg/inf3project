using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using game.client;

namespace GameTest
{
    [TestClass]
    public class ConnectorTest
    {
        private String ip = "127.0.0.1";
        private UInt16 port = 1024;

        [TestMethod]
        public void CreateInstanceTest()
        {
            Boolean expected = true;
            Connector connection = new Connector(ip, port);
            Assert.IsNotNull(connection.getTcpClient());
            Assert.AreEqual(expected, connection.getTcpClient().Connected);
            Assert.IsNotNull(connection.getStream());
            Assert.AreEqual(expected, connection.getStream().CanRead);
            Assert.AreEqual(expected, connection.getStream().CanWrite);
            Assert.IsNotNull(connection.getReceiver());
            Assert.AreEqual(expected, connection.getReceiver().IsAlive);
            Assert.IsNotNull(connection.getBuffer());
            Assert.AreEqual(expected, connection.getBuffer().isEmpty());
        }

        [TestMethod]
        public void connectTest()
        {
            Boolean expected = true;
            Connector connection = new Connector(ip, port);
            connection.connect(ip, port);
            Assert.IsNotNull(connection.getTcpClient());
            Assert.AreEqual(expected, connection.getTcpClient().Connected);
            Assert.AreEqual(expected, connection.getReceiver().IsAlive);
        }

        [TestMethod]
        public void disconnectTest()
        {
            Boolean expected = false;
            Connector connection = new Connector(ip, port);
            connection.disconnect();
            Assert.AreEqual(expected, connection.getTcpClient().Connected);
            Assert.AreEqual(expected, connection.getReceiver().IsAlive);
            Assert.AreEqual(expected, connection.getStream().CanRead);
            Assert.AreEqual(expected, connection.getStream().CanWrite);
        }

        [TestMethod]
        public void sendServerMessageTest()
        {
            Boolean expected = true;
            Connector connection = new Connector(ip, port);
            connection.sendServerMessage("get:map");
            Assert.Equals(expected, connection.getStream().DataAvailable);
        }

        [TestMethod]
        public void receiveServerMessageTest()
        {
            Boolean expected = false;
            Connector connection = new Connector(ip, port);
            Assert.AreEqual(expected, connection.getBuffer().isEmpty());
        }


    }


}
