using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    internal class Cursor : EventEmitter
    {
        private EventEmitter eventEmitter;
        private int cx_;
        private int cy_;

        public int cx
        {
            get => this.cx_; set
            {
                if (value < this.boundX[0] || value > this.boundX[1]) return;
                this.cx_ = value;
            }
        }
        public int cy
        {
            get => this.cy_; set
            {
                if (value < this.boundY[0] || value > this.boundY[1]) return;
                this.cy_ = value;
            }
        }
        public int prevcy;
        public int prevcx;

        public int stepX { get; set; }
        public int stepY { get; set; }

        public int[] boundX; // left - right
        public int[] boundY;// top - bottom


        public Cursor()
        {
            this.boundX = new int[2];
            this.boundY = new int[2];
            this.cx = 0;
            this.cy = 0;

            this.eventEmitter = Service.provider.GetService<EventEmitter>()!;
            this.eventEmitter.ToRegisterScreen += (object sender, EventArgs e) =>
            {

                this.boundX = [1, 1];
                this.boundY = [0, 4];

                this.cx = 1;
                this.cy = 0;
                this.stepX = 0;
                this.stepY = 1;
            };

            this.eventEmitter.ToHomeScreen += (object sender, EventArgs e) =>
            {
                this.boundX = [3, 3];
                this.boundY = [1, 3];

                this.stepX = 0;
                this.stepY = 1;
                this.cy = 1;
                this.cx = 3;
            };

            this.eventEmitter.ToLogInScreen += (object sender, EventArgs e) =>
            {
                this.boundX = [1, 1];
                this.boundY = [0, 3];

                this.cx = 1;
                this.cy = 0;
                this.stepX = 0;
                this.stepY = 1;
            };
        }

        public void logCursorValues()
        {
            this.prevcy = this.cy;
            this.prevcx = this.cx;
        }
    }
}
