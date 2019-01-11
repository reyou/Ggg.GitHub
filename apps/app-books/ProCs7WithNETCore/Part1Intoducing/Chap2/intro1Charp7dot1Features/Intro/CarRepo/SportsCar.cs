using System;

namespace Intro.CarRepo
{
    public class SportsCar : Car
    {
        private int introQqq;
        private string introEee;

        public event EventHandler OnActivated;
        public event EventHandler OnDeleted;

        public int Title
        {
            get => default;
            set
            {
            }
        }

        public string Name
        {
            get => default;
            set
            {
            }
        }

        public bool Amended
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Gets the title
        /// </summary>
        public string GetTitle()
        {
            throw new System.NotImplementedException();
        }
    }
}