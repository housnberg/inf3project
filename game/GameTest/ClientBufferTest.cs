using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.client;

namespace GameTest
{
    [TestClass]
    class ClientBufferTest
    {
        [TestMethod]
      public void createBufferTest()
      {
          ClientBuffer buffer = ClientBuffer.getBufferInstance();
          Assert.IsNotNull(buffer);
      }

         [TestMethod]
        public void addElementTest()
        {
            ClientBuffer buffer = ClientBuffer.getBufferInstance();
            buffer.put("Test");
            Assert.IsTrue(buffer.getSize() > 0);
        }

         [TestMethod]
         public void addMultipleElements()
         {
             ClientBuffer buffer = ClientBuffer.getBufferInstance();
             buffer.put("Test1");
             buffer.put("Test2");
             buffer.put("Test3");

             Assert.IsTrue(buffer.getSize() == 3);
         }

         [TestMethod]
         public void readElementTest()
         {
             ClientBuffer buffer = ClientBuffer.getBufferInstance();
             String testString = "Test";
             buffer.put(testString);
             Assert.IsTrue(testString.Equals(buffer.getElement()));
         }

         [TestMethod]
         public void emptyBuffer()
         {
             ClientBuffer buffer = ClientBuffer.getBufferInstance();
             String testString = "Test";
             buffer.put(testString);
             buffer.getElement();
             Assert.IsTrue(buffer.isEmpty());
         }

    }
}
