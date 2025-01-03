using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Code.Game;
using Code.Music;
using UnityEngine;


namespace Code
{
    [CreateAssetMenu(fileName = "Data", menuName = "Create Data")]
    public class Data : ScriptableObject
    {
        public SerializedDictionary<ShortSfx, AudioClip> Sfx;
        public SerializedDictionary<Music.Music, AudioClip> Music;
        
        public AudioSource SfxPrefab;
        public AudioSource MusicPrefab;
        
        public SerializedDictionary<int, int> MovementSpeed;
        public SerializedDictionary<int, int> JumpPower;
        public List<Sprite> Backgrounds;
        public Platform PlatformPrefab;
        public Stone StonePrefab;
        public Candy CandyPrefab;
        public List<Sprite> Candies;
    }
}