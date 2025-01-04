using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AYellowpaper.SerializedCollections;
using Game.UI.Inspector;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SlotWinCombinations", menuName = "Create Slot Win Combinations")]
    public class SlotWinCombinations : ScriptableObject
    {
        [NonSerialized] public List<Combination> Combinations;
        [HideInInspector] public SerializedDictionary<int, Cell> Cells;
        
        public int Columns;
        public int Rows;

        public int CombinationsCount;

        [SerializeField] private string _fileName;
    
        public void SaveCombinations()
        {
            SaveDictionaryToJson(_fileName + ".json", Cells);
        }

        public void Load()
        {
            Cells = LoadDictionaryFromJson<int, Cell>(_fileName + ".json");
            SortCellsByCombination();
        }

        public void RedrawCombinations()
        {
#if UNITY_EDITOR
            Cells = new ();
            
            for (int i = 0; i < CombinationsCount; i++)
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        Cells.Add(new Position(i, c, r).GetHashCode(), new Cell());
                    }
                }
            }

            SortCellsByCombination();
#endif
        }

        private void OnEnable()
        {
            RedrawCombinations();
        }
        
        private async void SaveDictionaryToJson<T, U>(string fileName, SerializedDictionary<T, U> dictionary)
        {
            string json = JsonUtility.ToJson(dictionary);

            string resourcesPath = Application.dataPath + "/Resources/";
            
            string path = resourcesPath + fileName;

            await WriteAsync(path, json);
            
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
        
        private SerializedDictionary<T, U> LoadDictionaryFromJson<T, U>(string fileName)
        {
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
            
            fileName = Path.GetFileNameWithoutExtension(fileName);

            TextAsset textAsset = Resources.Load<TextAsset>(fileName);

            if (textAsset != null)
            {
                return JsonUtility.FromJson<SerializedDictionary<T, U>>(textAsset.text);
            }

            return null;
        }
        
        private async Task WriteAsync(string filePath, string json)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                await streamWriter.WriteAsync(json);
                await streamWriter.FlushAsync(); 
            }
        }

        private void SortCellsByCombination()
        {
            Combinations = new();

            for (int i = 0; i < CombinationsCount; i++)
            {
                List<Cell> cells = Cells.Values.Where(cell => cell.Combination == i).ToList();
                
                Combination combination = new();
                combination.Cells.AddRange(cells);
                
                Combinations.Add(combination);
            }
        }
    }
}