using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using game.client;

namespace GameTest
{
    [TestClass]
    public class ParserTest
    {
        String partOfMessageTest; //Empty variable to fill the parameter for most methods.

        [TestMethod]
        public void getMessageTest()
        {
            ParserGate parserGate = new ParserGate();
            parserGate.getMessage();
            Assert.IsNotNull(parserGate.message);
        }

        public void parseTest()
        {
            ParserGate parserGate = new ParserGate();
            parserGate.parse(parserGate.message);
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parseMessageTest()
        {
            ParserGate parserGate = new ParserGate();
            parserGate.parseMessage(partOfMessageTest);
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parseAnswerTest()
        {
            ParserGate parserGate = new ParserGate();
            parserGate.parseAnswer(partOfMessageTest);
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parseYourIdTest()
        {
            ParserGate parserGate = new ParserGate();
            parserGate.parseYourId(partOfMessageTest);
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parseTimeTest()
        {
            ParserGate parserGate = new ParserGate();
            parserGate.parseTime(partOfMessageTest);
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parseOnlineTest()
        {
            ParserGate parserGate = new ParserGate();
            parserGate.parseOnline(partOfMessageTest);
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parseEntitiesTest()
        {
            ParserGate parserGate = new ParserGate();
            parserGate.parseEntities(partOfMessageTest);
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parsePlayersTest()
        {
            ParserGate parserGate = new ParserGate();
            parserGate.parsePlayers(partOfMessageTest);
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parseUpdateTest()
        {
            ParserUpdateDelete parserUpdateDelete = new ParserUpdateDelete();
            parserUpdateDelete.parseUpdate(parserUpdateDelete.message);
            Assert.IsTrue(parserUpdateDelete.messageIsValid);
        }

        public void parseDeleteTest()
        {
            ParserUpdateDelete parserUpdateDelete = new ParserUpdateDelete();
            parserUpdateDelete.parseDelete(parserUpdateDelete.message);
            Assert.IsTrue(parserUpdateDelete.messageIsValid);
        }

        public void parseChallengeTest()
        {
            ParserChallengeResult parserChallengeResult = new ParserChallengeResult();
            parserChallengeResult.parseChallenge(parserChallengeResult.message);
            Assert.IsTrue(parserChallengeResult.messageIsValid);
        }

        public void parseResultTest()
        {
            ParserChallengeResult parserChallengeResult = new ParserChallengeResult();
            parserChallengeResult.parseResult(parserChallengeResult.message);
            Assert.IsTrue(parserChallengeResult.messageIsValid);
        }

        public void parseOpponentTest()
        {
            ParserChallengeResult parserChallengeResult = new ParserChallengeResult();
            parserChallengeResult.parseOpponent(parserChallengeResult.message);
            Assert.IsTrue(parserChallengeResult.messageIsValid);
        }

        public void parseDragonTest()
        {
            bool messageIsValid = true;
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parsePlayerTest()
        {
            bool messageIsValid = true;
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parseMapTest()
        {
            bool messageIsValid = true;
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parseMapcellTest()
        {
            bool messageIsValid = true;
            Assert.IsTrue(parserGate.messageIsValid);
        }

        public void parsePropertyTest()
        {
            bool messageIsValid = true;
            Assert.IsTrue(parserGate.messageIsValid);
        }
    }
}
