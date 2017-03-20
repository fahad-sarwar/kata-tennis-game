using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TennisGameKata
{
    [TestClass]
    public class TennisGameSpec
    {
        [TestMethod]
        public void Return_Default_Game()
        {
            Assert.AreEqual("0:0", new TennisGame().CurrentScore());
        }

        [TestMethod]
        public void Given_the_score_is_0_0_when_the_server_wins_a_point_return_15_0()
        {
            Assert.AreEqual("15:0", new TennisGame()
                .PlayerOneScores()
                .CurrentScore());
        }

        [TestMethod]
        public void Given_the_score_is_0_0_when_the_receiver_wins_a_point_return_0_15()
        {
            Assert.AreEqual("0:15", new TennisGame()
                .PlayerTwoScores()
                .CurrentScore());
        }

        [TestMethod]
        public void Given_the_score_is_15_15_when_the_receiver_wins_a_point_return_15_30()
        {
            Assert.AreEqual("15:30", new TennisGame()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerTwoScores()
                .CurrentScore());
        }

        [TestMethod]
        public void Given_the_score_is_30_30_when_the_server_wins_a_point_return_40_30()
        {
            Assert.AreEqual("40:30", new TennisGame()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerOneScores()
                .CurrentScore());
        }

        [TestMethod]
        public void Given_the_score_is_deuce_when_the_receiver_wins_a_point_return_advantage_player_two()
        {
            Assert.AreEqual("Advantage Player Two", new TennisGame()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerTwoScores()
                .CurrentScore());
        }

        [TestMethod]
        public void Given_the_score_is_advantage_player_one_when_the_receiver_wins_a_point_return_deuce()
        {
            Assert.AreEqual("Deuce", new TennisGame()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerOneScores()
                .PlayerTwoScores()
                .CurrentScore());
        }

        [TestMethod]
        public void Given_the_score_is_40_30_when_the_server_wins_a_point_return_player_one_wins()
        {
            Assert.AreEqual("Player One Wins", new TennisGame()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerOneScores()
                .PlayerOneScores()
                .CurrentScore());
        }

        [TestMethod]
        public void Given_the_score_is_advantage_player_two_when_the_receiver_wins_a_point_return_player_two_wins()
        {
            Assert.AreEqual("Player Two Wins", new TennisGame()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerOneScores()
                .PlayerTwoScores()
                .PlayerTwoScores()
                .PlayerTwoScores()
                .CurrentScore());
        }
    }

    public class TennisGame
    {
        private readonly string _score;

        private static readonly List<Outcome> _outcomeList = new List<Outcome>
        {
            new Outcome(new TennisGame(), new TennisGame("15:0"), new TennisGame("0:15")),
            new Outcome(new TennisGame("15:0"), new TennisGame("30:0"), new TennisGame("15:15")),
            new Outcome(new TennisGame("30:0"), new TennisGame("40:0"), new TennisGame("30:15")),
            new Outcome(new TennisGame("40:0"), new TennisGame("Player One Wins"), new TennisGame("40:15")),
            new Outcome(new TennisGame("Player One Wins"), new TennisGame("Player One Wins"), new TennisGame("Player One Wins")),
            new Outcome(new TennisGame("0:15"), new TennisGame("15:15"), new TennisGame("0:30")),
            new Outcome(new TennisGame("0:30"), new TennisGame("15:30"), new TennisGame("0:40")),
            new Outcome(new TennisGame("0:40"), new TennisGame("15:40"), new TennisGame("Player Two Wins")),
            new Outcome(new TennisGame("Player Two Wins"), new TennisGame("Player Two Wins"), new TennisGame("Player Two Wins")),
            new Outcome(new TennisGame("15:15"), new TennisGame("30:15"), new TennisGame("15:30")),
            new Outcome(new TennisGame("30:30"), new TennisGame("40:30"), new TennisGame("30:40")),
            new Outcome(new TennisGame("Deuce"), new TennisGame("Advantage Player One"), new TennisGame("Advantage Player Two")),
            new Outcome(new TennisGame("Advantage Player One"), new TennisGame("Player One Wins"), new TennisGame("Deuce")),
            new Outcome(new TennisGame("Advantage Player Two"), new TennisGame("Deuce"), new TennisGame("Player Two Wins")),
            new Outcome(new TennisGame("30:15"), new TennisGame("40:15"), new TennisGame("30:30")),
            new Outcome(new TennisGame("15:30"), new TennisGame("30:30"), new TennisGame("15:40")),
            new Outcome(new TennisGame("40:15"), new TennisGame("Player One Wins"), new TennisGame("40:30")),
            new Outcome(new TennisGame("15:40"), new TennisGame("30:40"), new TennisGame("Player Two Wins")),
            new Outcome(new TennisGame("40:30"), new TennisGame("Player One Wins"), new TennisGame("Deuce")),
            new Outcome(new TennisGame("30:40"), new TennisGame("Deuce"), new TennisGame("Player Two Wins")),
        };

        public TennisGame(string score = "0:0")
        {
            _score = score;
        }

        public TennisGame PlayerOneScores()
        {
            return _outcomeList.First(o => o.CurrentScore.Equals(this)).PlayerOneScore;
        }

        public TennisGame PlayerTwoScores()
        {
            return _outcomeList.First(o => o.CurrentScore.Equals(this)).PlayerTwoScore;
        }

        public string CurrentScore()
        {
            return _score;
        }

        public override bool Equals(object obj)
        {
            return ((TennisGame)obj)._score == _score;
        }

        public override int GetHashCode()
        {
            return _score.GetHashCode();
        }
    }

    public class Outcome
    {
        public Outcome(TennisGame currentScore, TennisGame playerOneScore, TennisGame playerTwoScore)
        {
            CurrentScore = currentScore;
            PlayerOneScore = playerOneScore;
            PlayerTwoScore = playerTwoScore;
        }

        public TennisGame CurrentScore { get; private set; }
        public TennisGame PlayerOneScore { get; private set; }
        public TennisGame PlayerTwoScore { get; private set; }
    }
}
