using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config {
    private Config() {// Prevent outside instantiation
    }

    private static readonly Config config = new Config();

    //examples of reading a json
    //the idea is to have a config for each individual item/placeable in the game which should have stats.
    //in this way we can easily change values, create new items without the need of more code.
    // we can use the cfg to literarely configure hundreds of items and then use them as template to generate items ingame. 


    // Item item = JsonFileReader.Read<Item>(@"C:\myFile.json");

    public static Config instance() {
        return config;
    }

    // using (StreamReader r = new StreamReader("file.json")){
    //     string json = r.ReadToEnd();
    //     List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
    // }

}