﻿using Core.Data;
using Core.Hints.SolvingTechniques;
using Core.Hints.TechniqueFinders;
using Core.Serializers;
using System;
using System.Linq;
using Xunit;

namespace Tests.TechniqueFinders
{
    public class HiddenTripleFinderTest
    {
        [Fact]
        public void FindOne()
        {
            const string givens = "38..59...5..3.1....91...53.1..2....446.598.179..413..6..5...62......2..5...1.5.43";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);
            grid.FillAllLegalCandidates();

            var finder = new HiddenTripleFinder();

            var results = finder.FindAll(grid).OfType<HiddenSubset>().ToList();
            Assert.Equal(2, results.Count);

            var values = new InputValue[] { 1, 3, 4 };
            Assert.Contains(results, result => values.All(value => result.Values.Contains(value)));
        }
    }
}