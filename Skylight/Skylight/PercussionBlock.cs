﻿namespace Skylight
{
    using System;

    public class PercussionBlock : Block
    {
        public const int
            BASE1   = 0,
            BASE2   = 1,
            SNARE1  = 2,
            SNARE2  = 3,
            CYMBAL1 = 4,
            CYMBAL2 = 5,
            CYMBAL3 = 6,
            CLAP    = 7,
            CYMBAL4 = 8,
            MARACA  = 9;

        private int percussionId = -1;

        public PercussionBlock(
            int x,
            int y,
            int id,
            int percussionId,
            Room r,
            Player placer = null) : base(x, y, id, r, placer)
        {
            this.Coords.X = x;
            this.Coords.Y = y;
            this.PercussionId = percussionId;
            this.R = r;
            this.Placer = placer;
        }

        public int PercussionId
        {
            get
            {
                return this.percussionId;
            }

            set
            {
                if (this.percussionId == -1)
                {
                    this.percussionId = value;
                }
            }
        }
    }
}
