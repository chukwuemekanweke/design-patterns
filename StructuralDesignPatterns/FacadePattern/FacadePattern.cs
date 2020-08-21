using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralDesignPatterns.FacadePattern
{
    class FacadePattern
    {
    }


    public interface Hotel
    {
        Menus GetMenus();
    }


    public class NonVegRestaurant : Hotel
    {
        public Menus GetMenus()
        {
            NonVegMenu nv = new NonVegMenu();
            return nv;
        }
    }

    public class VegRestaurant : Hotel
    {
        public Menus GetMenus()
        {
            VegMenu v = new VegMenu();
            return v;
        }
    }

    public class VegNonBothRestaurant : Hotel
    {
        public Menus GetMenus()
        {
            Both b = new Both();
            return b;
        }
    }

    public class HotelKeeper
    {
        public VegMenu getVegMenu()
        {
            VegRestaurant v = new VegRestaurant();
            VegMenu vegMenu = (VegMenu)v.GetMenus();
            return vegMenu;
        }

        public NonVegMenu getNonVegMenu()
        {
            NonVegRestaurant v = new NonVegRestaurant();
            NonVegMenu NonvegMenu = (NonVegMenu)v.GetMenus();
            return NonvegMenu;
        }

        public Both getVegNonMenu()
        {
            VegNonBothRestaurant v = new VegNonBothRestaurant();
            Both bothMenu = (Both)v.GetMenus();
            return bothMenu;
        }
    }


    public class Menus
    {

    }

    public class NonVegMenu : Menus
    {

    }

    public class VegMenu : Menus
    {

    }

    public class Both : Menus
    {

    }


}
