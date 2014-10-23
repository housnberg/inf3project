using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using game.client;

namespace GameTest
{
    [TestClass]
    public class FifoTest
    {

        
        [TestMethod]
        public void testPutFifo()
        {
            Fifo f = new Fifo("first");
            Assert.AreEqual("first", f.get());   
        }

        [TestMethod]
        public void testGetFifo()
        {
            Fifo f = new Fifo("first");
            f.put("two");
            Assert.AreEqual("two", f.get());
        }

       
        [TestMethod]
        public void testFifoEmpty()
        {
            Fifo f = new Fifo("first");
            f.get();
            Assert.AreEqual(true, f.isEmpty());
        }

        [TestMethod]
        public void testFifoFull()
        {
            Fifo f = new Fifo("first");
            Assert.AreEqual(false, f.isEmpty());
        }
        
 
    }
}
