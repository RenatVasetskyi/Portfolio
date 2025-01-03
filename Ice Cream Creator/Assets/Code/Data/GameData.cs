using System.Collections.Generic;
using Code.Audio;
using Code.Gameplay.Candies.Data;
using Code.Gameplay.Person;
using Code.UI.Shop;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Create Game Data")]
    public class GameData : ScriptableObject
    {
        public SfxWrapper SfxWrapper;
        public MusicWrapper MusicWrapper;
        public ShopItem[] ShopItems;
        public CandyData[] CandyDatas;
        public List<Customer> Customers;
        public int MakeOrderPrize;
    }
}