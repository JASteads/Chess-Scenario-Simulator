using NUnit.Framework;
using System.Collections.Generic;

    public class ScenarioTests
    {
        public void Scenario_Initialization_Test()
        {
            var scenario = new Scenario();

            Assert.IsNotNull(scenario.StartingPieces, "StartingPieces not null.");
            Assert.IsNotNull(scenario.CorrectMoves, "CorrectMoves not null.");
            Assert.IsNotNull(scenario.CounterMoves, "CounterMoves not null.");
            Assert.IsEmpty(scenario.StartingPieces, "StartingPieces empty upon initialization.");
            Assert.IsEmpty(scenario.CorrectMoves, "CorrectMoves empty upon initialization.");
            Assert.IsEmpty(scenario.CounterMoves, "CounterMoves empty upon initialization.");
        }

        public void Scenario_AddingPieces_Test()
        {
            var scenario = new Scenario();
            var piece = new Piece();

            scenario.StartingPieces.Add(piece);

            Assert.AreEqual(1, scenario.StartingPieces.Count, "StartingPieces has one piece.");
            Assert.AreSame(piece, scenario.StartingPieces[0], "The same piece as the one retrieved.");
        }

        public void Scenario_AddingCorrectMoves_Test()
        {
            var scenario = new Scenario();
            short move = 123;

            scenario.CorrectMoves.Add(move);

            Assert.AreEqual(1, scenario.CorrectMoves.Count, "CorrectMoves have one move.");
            Assert.AreEqual(move, scenario.CorrectMoves[0], "The same move as the one retrieved.");
        }

        public void Scenario_AddingCounterMoves_Test()
        {
            var scenario = new Scenario();
            short move = 456;

            scenario.CounterMoves.Add(move);

            Assert.AreEqual(1, scenario.CounterMoves.Count, "CounterMoves have one move.");
            Assert.AreEqual(move, scenario.CounterMoves[0], "The same move as the one retrieved.");
        }
    }

    public class Piece
    {
    }
