using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskShare.Algorithms;
using TaskShare.Models;

namespace TaskShareAlgorithmsTests
{
    [TestFixture(typeof(BipartitionAlgorithm<Task>))]
    public class TaskBinaryPartitionAlgorithmsTests<Algorithm> where Algorithm: IPartitionAlgorithm<Task>, new()
    {
        IPartitionAlgorithm<Task> _algorithm;

        [SetUp]
        public void Setup()
        {
            _algorithm = new Algorithm();
            _algorithm.Predicate = t => t.TimeCost;
        }

        [Test]
        public void NonBinaryPartitionTest([Range(3, 11)]int n)
        {
            Action setSubsetsCount = () => _algorithm.SubsetsCount = n;
            Assert.That(setSubsetsCount, 
                Throws.Exception.TypeOf<UnsupportedSubsetsCountException>());
        }

        private static IEnumerable<List<int>> SetOfThreeGenerator()
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
                yield return new()
                {
                    random.Next(-99, 99),
                    random.Next(-99, 99),
                    random.Next(-99, 99)
                };
        }

        [TestCaseSource(nameof(SetOfThreeGenerator))]
        public void NonPositiveValueTest(List<int> set)
        {
            Assume.That(set, Is.Not.All.Positive);

            Action runAlgorithm = () => _algorithm.Run(set.Select(i => new Task { TimeCost = i }).ToList());

            Assert.That(runAlgorithm, 
                Throws.Exception.TypeOf<OneOfTheWeightNonPositiveException>());
        }

        [TestCase(3)]
        [TestCase(1, 1, 3)]
        [TestCase(3, 3, 3)]
        [TestCase(1, 2, 3, 4, 5)]
        [TestCase(2, 2, 5, 7)]
        public void CannotSplitTest(params int[] set)
        {
            var taskSet = set
                .Select(i => new Task { TimeCost = i })
                .ToList();

            Action runAlgorithm = () => _algorithm.Run(set.Select(i => new Task { TimeCost = i }).ToList());

            Assert.That(runAlgorithm, Throws.Exception.TypeOf<CannotBeSplitException>());
        }

        private static List<Task> PositiveGenerator(int sum, int max)
        {
            Random random = new Random();
            List<Task> result = new List<Task>();
            for (int i = 0; i < 2; i++)
            {
                int firstSum = sum;
                while (firstSum > 0)
                {
                    int rand = random.Next(1, max);
                    if (rand > firstSum)
                        rand = firstSum;
                    firstSum -= rand;
                    result.Add(new Task { TimeCost = rand });
                }
            }
            return result
                .OrderBy(_ => random.Next())
                .ToList();
        }

        private static IEnumerable<List<Task>> Generator()
        {
            for (int i = 0; i < 50; i++)
            {
                yield return PositiveGenerator(50 + i, 25);
            }
        }

        [TestCaseSource(nameof(Generator))]
        public void PositiveTest(List<Task> set)
        {
            List<List<Task>> result = null;
            Action action = () =>
                result = _algorithm.Run(set);

            Assert.That(action, Throws.Nothing);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Sum(t => t.TimeCost), Is.EqualTo(result[1].Sum(t => t.TimeCost)));
        }
    }
}