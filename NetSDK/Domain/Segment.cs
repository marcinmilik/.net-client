﻿using Splitio.Services.Client.Classes;
using Splitio.Services.SegmentFetcher.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Splitio.Domain
{
    public class Segment: ISegment
    {
        protected string name { get; set; }
        public long changeNumber;
        private ConcurrentDictionary<string, byte> keys;

        public Segment(string name, long changeNumber = -1, ConcurrentDictionary<string, byte> keys= null)
        {
            this.name = name;
            this.changeNumber = changeNumber;
            this.keys = keys ?? new ConcurrentDictionary<string, byte>();
        }

        public void AddKeys(List<string> keys)
        {
            foreach (var key in keys)
            {
                this.keys.TryAdd(key, 0);
            }
        }

        public void RemoveKeys(List<string> keys)
        {
            foreach (var key in keys)
            {
                byte value;
                this.keys.TryRemove(key, out value);
            }
        }

        public bool Contains(string key)
        {
            byte value;
            return keys.TryGetValue(key, out value);
        }
    }
}
