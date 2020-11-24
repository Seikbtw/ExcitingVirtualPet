using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcitingVirtualPet
{
    interface IAnimal
    {
        void increaseNeedCounters(); //might not need
        void increaseHunger();
        void increaseThirst();
        void increaseBoredom();
        void decreaseAffection();
        void tryToDrink();
        void tryToEat();
        void maybeTakeAway();

    }
}
