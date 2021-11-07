using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.MapObjects
{
    public interface IMapObject
    {
        void Interact(Player p);
    }

    public interface IOwner
    {
        int Owner { get; set; }
    }

    public interface IArmy
    {
        Army Army { get; set; }
    }

    public interface ITreasure
    {
        Treasure Treasure { get; set; }
    }


    public class Dwelling: IMapObject
    {
        public int Owner { get; set; }
        public void Interact(Player player)
        {
            Owner = player.Id;
        }
    }

    public class Mine: IMapObject, IOwner,IArmy
    {
        public int Owner { get; set; }
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
        public void Interact(Player player)
        {
            if (player.CanBeat(Army))
            {
                Owner = player.Id;
                player.Consume(Treasure);
            }
            else player.Die();
        }
    }

    public class Creeps :IMapObject, IArmy
    {
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
        public void Interact(Player player)
        {
            if (player.CanBeat(Army))
                player.Consume(Treasure);
            else
                player.Die();
        }
    }

    public class Wolves : IMapObject
    {
        public Army Army { get; set; }
        public void Interact(Player player)
        {
            if (!player.CanBeat(Army))
                player.Die();
        }
    }

    public class ResourcePile : IMapObject
    {
        public Treasure Treasure { get; set; }
        public void Interact(Player player)
        {
            player.Consume(Treasure);
        }
    }

    public static class Interaction
    {
        public static void Make(Player player, IMapObject mapObject)
        {
            mapObject.Interact(player);
        }
    }
}
