﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcitingVirtualPet
{
    public partial class Form1 : Form
    {
        //Constants
        //pet Constants
        const int MAX_HUNGER = 10;
        const int MIN_HUNGER = 0;
        const int MAX_AFFECTION = 10;
        const int MIN_AFFECTION = 0;
        const int MAX_THIRST = 10;
        const int MIN_THIRST = 0;
        const int MAX_BOREDOM = 10;
        const int MIN_BOREDOM = 0;
        //Resource Constants
        const int MAX_FOOD = 10;
        const int MIN_FOOD = 0;
        const int MAX_WATER = 10;
        const int MIN_WATER = 0;

        //Program "globals" (really just fields of Form1)
        int catHunger;
        int catAffection;
        int catThirst;
        int catBoredom;
        int currentFood;
        int currentWater;
        int catStartEating;
        int catStartDrinking;
        bool catEating = false;
        bool catDrinking = false;

        //Time Stuff
        Timer mainLoopTimer;
        int hungerFrame;
        int thirstFrame;
        int boredomFrame;
        int affectionFrame;
        int eatFrame;
        int drinkFrame;
        int hungerCounter;
        int thirstCounter;
        int boredomCounter;
        int affectionCounter;
        int eatCounter;
        int drinkCounter;

        Random generator;
        Pet currentPet;

        public Form1()
        {
            InitializeComponent();

            //set up initial stuff
            generator = new Random();

            InitializeCat();


            InitializeFood();
            InitializeWater();

            //set up loop
            mainLoopTimer = new Timer();
            mainLoopTimer.Interval = 17; //runs every 60th of a second
            mainLoopTimer.Tick += MainLoopTimer_Tick; //run MainLoopTimer_Tick method every 60th of a second
            mainLoopTimer.Start(); //start the main loop
        }

        private void MainLoopTimer_Tick(object sender, EventArgs e)
        {
            //increase frame counters
            currentPet.increaseNeedCounters();
            
            //How Frame Counters Work
            //The frame counters "count" every tick of the main loop timer.
            //Once the counter reaches a frame (like hungerFrame), the cat
            //performs that action.  This way we can have 1 timer and multiple
            //actions that can be performed.  Once the count reaches the frame,
            //we also need to remember to reset the counter for the next time.

            //update cat needs
            if (currentPet.hungerCounter >= currentPet.hungerFrame)
            {
                currentPet.increaseHunger();
                
            }
            if(thirstCounter >= thirstFrame)
            {
                currentPet.increaseThirst();
                
            }
            if(affectionCounter >= affectionFrame)
            {
                currentPet.decreaseAffection();
                
            }
            if(boredomCounter >= boredomFrame)
            {
                currentPet.increaseBoredom();
                
            }
            //update cat need fulfillment
            if (catDrinking)
            {
                drinkCounter++;
                if(drinkCounter >= drinkFrame)
                {
                    tryToDrink();
                }
            }
            if (catEating)
            {
                eatCounter++;
                if (eatCounter >= eatFrame)
                {
                    tryToEat();
                }
            }

            //check game loss condition
            maybeTakeCatAway();

            //update view
            UpdateView();
        }

        //Set up main data
        private void InitializeCat()
        {
            Pet currentPet = new Cat();
            petPictureBox.Image = Properties.Resources.basic_cat;

        }

        private void InitializeFood()
        {
            currentFood = 1;
        }
        private void InitializeWater()
        {
            currentWater = 1;
        }

        private void tryToDrink()
        {
            if(currentWater > MIN_WATER)
            {
                currentWater--;
                catThirst--;
            }

            if (catThirst == MIN_THIRST || currentWater == MIN_WATER) catDrinking = false;

            //reset frame counter
            drinkCounter = 0;
        }
        private void tryToEat()
        {
            if (currentFood > MIN_FOOD)
            {
                currentFood--;
                catHunger--;
            }

            if (catHunger == MIN_HUNGER || currentFood == MIN_FOOD) catEating = false;

            //reset frame counter
            eatCounter = 0;
        }
        private void maybeTakeCatAway()
        {
            //if you've really not taken care of your cat...
            if(catHunger == MAX_HUNGER && catThirst == MAX_THIRST && catBoredom == MAX_BOREDOM && catAffection == MIN_AFFECTION)
            {
                //replace image with lack of cat
                petPictureBox.Image = Properties.Resources.cat_leaving;
                
                //disable buttons
                feedCatButton.Enabled = false;
                catWaterButton.Enabled = false;
                catPlayButton.Enabled = false;
                petCatButton.Enabled = false;

                //stop main loop
                mainLoopTimer.Stop();
            }
        }


        private void UpdateView()
        {
            hungerMeter.Value = catHunger;
            thirstMeter.Value = catThirst;
            boredomMeter.Value = catBoredom;
            affectionMeter.Value = catAffection;

            waterAmountBar.Value = currentWater;
            foodAmountBar.Value = currentFood;
        }

        private void feedCatButton_Click(object sender, EventArgs e)
        {
            if(currentFood < MAX_FOOD)
            {
                currentFood++;
            }
        }

        private void catWaterButton_Click(object sender, EventArgs e)
        {
            if(currentWater < MAX_WATER)
            {
                currentWater++;
            }
        }

        private void catPlayButton_Click(object sender, EventArgs e)
        {
            if(catBoredom > MIN_BOREDOM)
            {
                catBoredom--;
            }
        }

        private void petCatButton_Click(object sender, EventArgs e)
        {
            if(catAffection < MAX_AFFECTION)
            {
                catAffection++;
            }
        }


    }
}
