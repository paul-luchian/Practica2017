using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Librarie.Data.Entities;

namespace Library.Test
{
    [TestClass]//atribute
    public class ScoreServiceTests
    {
        private IScoreService _scoreService;
            
        [TestInitialize]
        public void Init()
        {
            _scoreService = new ScoreService();
        }

        [TestMethod]

        public void ComputeScore_BookTypeSFScore5UserScore1_ShouldBe51()
        {
            //Arrange
            var book = new Book()
            {
                Type = "SF",
                Score = 5
            };
            const int userScore = 1;
            //Act
            int result = _scoreService.ComputeScore(book,userScore);
            //Assert
            Assert.AreEqual(51, result);

        }

        [TestMethod]

        public void ComputeScore_BookTypeSFScore2UserScore0_ShouldBe20()
        {
            //Arrange
            var book = new Book()
            {
                Type = "SF",
                Score = 2
            };
            const int userScore = 0;
            //Act
            int result = _scoreService.ComputeScore(book, userScore);
            //Assert
            Assert.AreEqual(20, result);

        }
        [TestMethod]

        public void ComputeScore_BookTypeSFScore2UserScore1_ShouldBe3()
        {
            //Arrange
            var book = new Book()
            {
                Type = "*",
                Score = 2
            };
            const int userScore = 1;
            //Act
            int result = _scoreService.ComputeScore(book, userScore);
            //Assert
            Assert.AreEqual(3, result);

        }
    }
    public class ScoreService : IScoreService
    {
        public int ComputeScore(Book book, int userScore)
        {
            if(book.Type=="SF")
            {
                return 10 * book.Score+userScore;
            }
            
            return book.Score + userScore;
        }
    }
    public interface IScoreService
    {
        int ComputeScore(Book book, int userScore);
    }
}
