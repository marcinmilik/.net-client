﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Splitio.Services.Parsing;
using Splitio.Domain;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Splitio.Services.Cache.Classes;

namespace Splitio_Tests.Unit_Tests
{
    [TestClass]
    public class UserDefinedSegmentMatcherTests
    {
        [TestMethod]
        public void MatchShouldReturnTrueOnMatchingSegment()
        {
            //Arrange
            var keys = new List<string>();
            keys.Add("test1");
            keys.Add("test2");

            var segmentName = "test-segment";
            var segmentCache = new SegmentCache(new ConcurrentDictionary<string, Segment>());
            segmentCache.RegisterSegment(segmentName);
            segmentCache.AddToSegment(segmentName, keys);

            var matcher = new UserDefinedSegmentMatcher(segmentName, segmentCache);

            //Act
            var result = matcher.Match("test2");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MatchShouldReturnFalseOnNonMatchingSegment()
        {
            //Arrange
            var keys = new List<string>();
            keys.Add("test1");
            keys.Add("test2");

            var segmentName = "test-segment";
            var segmentCache = new SegmentCache(new ConcurrentDictionary<string, Segment>());
            segmentCache.RegisterSegment(segmentName);
            segmentCache.AddToSegment(segmentName, keys);

            var matcher = new UserDefinedSegmentMatcher(segmentName, segmentCache);

            //Act
            var result = matcher.Match("test3");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void MatchShouldReturnFalseIfSegmentEmpty()
        {
            //Arrange
            var segmentName = "test-segment";
            var segmentCache = new SegmentCache(new ConcurrentDictionary<string, Segment>());
            segmentCache.RegisterSegment(segmentName);

            var matcher = new UserDefinedSegmentMatcher(segmentName, segmentCache);

            //Act
            var result = matcher.Match("test2");

            //Assert
            Assert.IsFalse(result);
        }
    }
}
